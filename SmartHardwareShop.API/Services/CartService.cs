using SmartHardwareShop.API.Models;
using SmartHardwareShop.API.Repositories;
using System;
using System.Collections.Generic;

namespace SmartHardwareShop.API.Services
{
    public class CartService : ICartService
    {
        private readonly IProductRepository productRepository;
        private readonly IOrderRepository orderRepository;
        private readonly ICartRepository cartRepository;

        public CartService(IProductRepository productRepository, IOrderRepository orderRepository, ICartRepository cartRepository)
        {
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
            this.cartRepository = cartRepository;
        }
        public int AddToCart(int userId, int productId, int quantity)
        {
            //validate
            var product = productRepository.GetProduct(productId);
            if (product == null)
            {
                throw new ApplicationException("Product not found for ID = " + product.Id);
            }
            if (product.Quantity < quantity)
            {
                throw new ApplicationException("Insufficient quantity for product = " + product.Quantity);
            }

            //create item
            CartItem cartItem = CreateCartItem(userId, productId, quantity);

            //save
            return cartRepository.AddCartItem(cartItem);
        }

        private static CartItem CreateCartItem(int userId, int productId, int quantity)
        {
            var cartItem = new CartItem();
            cartItem.ProductId = productId;
            cartItem.Quantity = quantity;
            cartItem.UserId = userId;
            cartItem.DateAdded = DateTime.Now;
            return cartItem;
        }

        public void ClearCart(int userId)
        {
            //save
            cartRepository.ClearCart(userId);
        }

        public void Checkout(int userId)
        {
            Order order = CreateOrder(userId);
            var cartItems = cartRepository.GetCartItems(userId);

            //create order
            foreach (var cartItem in cartItems)
            {
                var product = productRepository.GetProduct(cartItem.ProductId);
                var orderItem = CreateOrderItem(cartItem, product);

                order.OrderTotal += orderItem.Price;
                order.OrderItems.Add(orderItem);
            }                

            //place order
            orderRepository.AddOrder(order);

            //update available quantities
            foreach (var cartItem in cartItems)
            {
                productRepository.UpdateProductQuantity(cartItem.ProductId, cartItem.Quantity);
            }

            //clear cart
            cartRepository.ClearCart(userId);
        }

        private static OrderItem CreateOrderItem(CartItem cartItem, Product product)
        {
            var orderItem = new OrderItem();

            orderItem.DateAdded = DateTime.Now;
            orderItem.Discount = 0;
            orderItem.Price = product.Price;
            orderItem.ProductId = cartItem.ProductId;
            orderItem.Quantity = cartItem.Quantity;

            return orderItem;
        }

        private static Order CreateOrder(int userId)
        {
            var order = new Order();
            order.DateCreated = DateTime.Now;
            order.DateUpdated = DateTime.Now;
            order.Status = "pending";
            order.UserId = userId;
            order.OrderTotal = 0;
            order.OrderItems = new List<OrderItem>();
            return order;
        }
    }
}
