using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ShopMVC.Repositories
{
    public class UserOrderRepository : IUserOrderRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;

        public UserOrderRepository(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task ChangeOrderStatus(UpdateOrderStatusModel data)
        {
            var order = await _dbContext.Orders.FindAsync(data.OrderId) ?? throw new InvalidOperationException($"Order with id: {data.OrderId} does not found");
            order.OrderStatusId = data.OrderStatusId;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Order?> GetOrderById(int id)
        {
            return await _dbContext.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<OrderStatus>> GetOrderStatuses()
        {
            return await _dbContext.OrderStatuses.ToListAsync();
        }

        public async Task TogglePaymentStatus(int orderId)
        {
            var order = await _dbContext.Orders.FindAsync(orderId) ?? throw new InvalidOperationException($"Order with id: {orderId} does not found");
            order.IsPaid = !order.IsPaid;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> UserOrders(bool getAll = false)
        {
            var orders = _dbContext.Orders
                .Include(x => x.OrderStatus)
                .Include(x => x.OrderDetail)
                .ThenInclude(x => x.Item)
                .ThenInclude(x => x.ItemType)
                .AsQueryable();

            if (!getAll)
            {
                var userId = GetUserId();
                if (userId == null)
                {
                    throw new Exception("User is not logged-in");
                }
                orders = orders.Where(x => x.UserId == userId);
                return await orders.ToListAsync();
            }
            return await orders.ToListAsync();
        }
        private string GetUserId()
        {
            ClaimsPrincipal principal = _httpContextAccessor.HttpContext.User;

            string userId = _userManager.GetUserId(principal);

            return userId;
        }
    }
}
