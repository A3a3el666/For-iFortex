using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUser()
        {
            var userWithMostOrders = await _context.Users
                .Include(u => u.Orders)
                .Where(u => u.Status == UserStatus.Active)
                .OrderByDescending(u => u.Orders.Count)
                .FirstOrDefaultAsync();

            return userWithMostOrders;
        }


        public async Task<List<User>> GetUsers()
        {
            var inactiveUsers = _context.Users
                .Where(user => user.Status == UserStatus.Inactive)
                .ToList();

            return inactiveUsers;
        }
    }
}

//dotnet ef migrations add testMigration --project D:\TestTask\TestTask\TestTask.csproj
//dotnet ef database update --project D:\TestTask\TestTask\TestTask.csproj