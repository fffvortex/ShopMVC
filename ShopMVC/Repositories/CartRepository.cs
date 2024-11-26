using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ShopMVC.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartRepository(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<int> AddItem(int itemId, int qty)
        {
            string userId = GetUserId();
            using var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("user is not logged-in");
                }
                var cart = await GetCart(userId);

                if (cart == null)
                {
                    cart = new ShopCart
                    {
                        UserId = userId,

                    };
                    _dbContext.ShopCarts.Add(cart);
                }
                _dbContext.SaveChanges();
                var cartItem = _dbContext.CartDetails.FirstOrDefault(c => c.ShopCartId == cart.Id && c.ItemId == itemId);
                if (cartItem != null)
                {
                    cartItem.Quantity += qty;
                }
                else
                {
                    var item = _dbContext.Items.Find(itemId);
                    cartItem = new CartDetail
                    {
                        ItemId = itemId,
                        ShopCartId = cart.Id,
                        Quantity = qty,
                        UnitPrice = item.Price
                    };
                    _dbContext.CartDetails.Add(cartItem);

                }
                _dbContext.SaveChanges();
                transaction.Commit();

            }
            catch (Exception ex)
            {

            }
            var cartItemCount = await GetCartItemsCount(userId);
            return cartItemCount;
        }

        public async Task<int> RemoveItem(int itemId)
        {
            string userId = GetUserId();
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("user is not logged-in");
                }
                var cart = await GetCart(userId);

                if (cart == null)
                {
                    throw new Exception("invalid cart");
                }
                var cartItem = _dbContext.CartDetails.FirstOrDefault(c => c.ShopCartId == cart.Id && c.ItemId == itemId);
                if (cartItem == null)
                {
                    throw new Exception("not finded items in cart");
                }
                else if (cartItem.Quantity == 1)
                {
                    _dbContext.CartDetails.Remove(cartItem);
                }
                else
                {
                    cartItem.Quantity = cartItem.Quantity - 1;
                }
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            var cartItemCount = await GetCartItemsCount(userId);
            return cartItemCount;
        }

        public async Task<ShopCart> GetUserCart()
        {
            var userId = GetUserId();
            if (userId == null)
            {
                throw new Exception("Invalid user Id");
            }
            var shopCart = await _dbContext.ShopCarts
                .Include(a => a.CartDetails)
                .ThenInclude(a => a.Item)
                .ThenInclude(a => a.ItemType)
                .Where(a => a.UserId == userId)
                .FirstOrDefaultAsync();
            return shopCart;
        }

        public async Task<ShopCart> GetCart(string userId)
        {
            var cart = await _dbContext.ShopCarts.FirstOrDefaultAsync(x => x.UserId == userId);

            return cart;
        }

        public async Task<int> GetCartItemsCount(string userId = "")
        {
            if (string.IsNullOrEmpty(userId))
            {
                userId = GetUserId();
            }
            var data = await (from cart in _dbContext.ShopCarts
                              join cartDetail in _dbContext.CartDetails
                              on cart.Id equals cartDetail.ShopCartId
                              where cart.UserId == userId
                              select new { cartDetail.Id })
                              .ToListAsync();
            return data.Count;
        }

        public async Task<bool> DoCheckout()
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                var userId = GetUserId();
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("User is not logged-in");
                }
                var cart = await GetCart(userId) ?? throw new Exception("Invalid cart");
                var cartDetail = _dbContext.CartDetails
                    .Where(a => a.ShopCartId == cart.Id).ToList();

                if (cartDetail.Count == 0)
                {
                    throw new Exception("Cart is empty");
                }
                var order = new Order
                {
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow,
                    OrderStatusId = 1
                };
                _dbContext.Orders.Add(order);
                _dbContext.SaveChanges();
                foreach(var item in cartDetail)
                {
                    var orderDetail = new OrderDetail
                    {
                        ItemId = item.ItemId,
                        OrderId = order.Id,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    };
                    _dbContext.OrderDetails.Add(orderDetail);
                }
                _dbContext.SaveChanges();

                _dbContext.CartDetails.RemoveRange(cartDetail);

                _dbContext.SaveChanges();

                transaction.Commit();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private string GetUserId()
        {
            ClaimsPrincipal principal = _httpContextAccessor.HttpContext.User;

            string userId = _userManager.GetUserId(principal);

            return userId;
        }
    }

}
