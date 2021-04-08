using Microsoft.EntityFrameworkCore;
using SmartHardwareShop.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartHardwareShop.API.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IDbContextFactory<SmartHardwareShopContext> contextFactory;

        public OrderRepository(IDbContextFactory<SmartHardwareShopContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public List<Order> GetOrders(int userId)
        {
            using (var context = contextFactory.CreateDbContext())
            {
                return context.Order.Where(x => x.UserId == userId).ToList();
            }
        }

        public Order GetOrder(int id)
        {
            using (var context = contextFactory.CreateDbContext())
            {
                return context.Order.FirstOrDefault(x => x.Id == id);
            }
        }
        public int AddOrder(Order order)
        {
            using (var context = contextFactory.CreateDbContext())
            {
                var entity = context.Order.Add(order);
                var ret = context.SaveChanges();
                return entity.Entity.Id;
            }
        }

        public Order UpdateOrderStatus(int orderId, string status)
        {
            var found = GetOrder(orderId);
            if (found == null)
            {
                throw new ApplicationException("Order not found for ID = " + found);
            }

            found.Status = status;
            found.DateUpdated = DateTime.Now;

            using (var context = contextFactory.CreateDbContext())
            {
                var entity = context.Order.Update(found);
                var ret = context.SaveChanges();
                return found;
            }
        }
    }
}
