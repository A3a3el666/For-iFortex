using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;
namespace TestTask.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetOrder()
        {
            Order orderWithLargestSum = null;
            decimal maxSum = decimal.MinValue;

            foreach (var order in await _context.Orders.ToListAsync())
            {
                decimal sum = order.Price * order.Quantity;

                if (sum > maxSum)
                {
                    maxSum = sum;
                    orderWithLargestSum = order;
                }
            }

            return orderWithLargestSum;
        }


        public async Task<List<Order>> GetOrders()
        {
            var ordersWithQuantityGreaterThanTen = await _context.Orders
                .Where(order => order.Quantity > 10)
                .ToListAsync();

            return ordersWithQuantityGreaterThanTen;
        }

    }
}

