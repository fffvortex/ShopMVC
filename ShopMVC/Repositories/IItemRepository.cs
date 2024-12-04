namespace ShopMVC
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetItems(string sTerm = "", int typeId = 0);

        Task<IEnumerable<TypeItem>> TypesItem();
        Task<IEnumerable<Item>> GetLatestDropItems();
    }
}