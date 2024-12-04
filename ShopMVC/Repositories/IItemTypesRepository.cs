namespace ShopMVC.Repositories
{
    public interface IItemTypesRepository
    {
        Task AddType(TypeItem type);

        Task UpdateType(TypeItem type);

        Task<TypeItem?> GetTypeById(int id);

        Task DeleteType(TypeItem type);

        Task<IEnumerable<TypeItem>> GetTypes();
    }
}