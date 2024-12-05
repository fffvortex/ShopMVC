namespace ShopMVC.Repositories
{
    public interface IServiceItemsRepository
    {
        Task AddItem(Item item);

        Task DeleteItem(Item item);

        Task<Item?> GetItemById(int id);

        Task<IEnumerable<Item>> GetItems();

        Task UpdateItem(Item item);
    }
}