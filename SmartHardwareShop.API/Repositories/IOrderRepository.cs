using SmartHardwareShop.API.Models;
using System.Collections.Generic;

namespace SmartHardwareShop.API.Repositories
{
    public interface IOrderRepository
    {
        int AddOrder(Order order);
        Order GetOrder(int id);
        List<Order> GetOrders(int userId);
        Order UpdateOrderStatus(int orderId, string status);
    }
}