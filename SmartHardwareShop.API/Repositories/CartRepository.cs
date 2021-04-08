using Microsoft.EntityFrameworkCore;
using SmartHardwareShop.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartHardwareShop.API.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly IDbContextFactory<SmartHardwareShopContext> contextFactory;

        public CartRepository(IDbContextFactory<SmartHardwareShopContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public List<CartItem> GetCartItems(int userId)
        {
            using (var context = contextFactory.CreateDbContext())
            {
                return context.CartItem.Where(x => x.UserId == userId).ToList();
            }
        }

        public CartItem GetCartItem(int id)
        {
            using (var context = contextFactory.CreateDbContext())
            {
                return context.CartItem.FirstOrDefault(x => x.Id == id);
            }
        }

        public bool ClearCart(int userId)
        {
            var items = GetCartItems(userId);
            using (var context = contextFactory.CreateDbContext())
            {
                foreach (var item in items)
                {
                    context.CartItem.Remove(item);
                }

                context.SaveChanges();
                return true;
            }
        }


        public int AddCartItem(CartItem item)
        {
            using (var context = contextFactory.CreateDbContext())
            {
                var entity = context.CartItem.Add(item);
                var ret = context.SaveChanges();
                return entity.Entity.Id;
            }
        }


        public bool DeleteCartItem(int cartItem)
        {
            var found = GetCartItem(cartItem);
            if (found == null)
            {
                throw new ApplicationException("Cart Item not found for ID = " + cartItem);
            }

            using (var context = contextFactory.CreateDbContext())
            {
                var entity = context.CartItem.Remove(found);
                var ret = context.SaveChanges();
                return true;
            }
        }
    }
}
