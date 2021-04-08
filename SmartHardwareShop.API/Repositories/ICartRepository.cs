using SmartHardwareShop.API.Models;
using System.Collections.Generic;

namespace SmartHardwareShop.API.Repositories
{
    public interface ICartRepository
    {
        int AddCartItem(CartItem item);
        bool ClearCart(int userId);
        bool DeleteCartItem(int cartItem);
        CartItem GetCartItem(int id);
        List<CartItem> GetCartItems(int userId);
    }
}