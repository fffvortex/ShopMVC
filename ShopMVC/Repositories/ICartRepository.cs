namespace ShopMVC.Repositories
{
    public interface ICartRepository
    {
        Task<int> AddItem(int itemId, int qty);
        Task<int> RemoveItem(int itemId);
        Task<ShopCart> GetUserCart();
        Task<ShopCart> GetCart(string userId);
        Task<int> GetCartItemsCount(string userId = "");

        Task<bool> DoCheckout();
    }
}