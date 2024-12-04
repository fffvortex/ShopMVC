
using Microsoft.EntityFrameworkCore;

namespace ShopMVC.Repositories
{
    public class ItemTypesRepository : IItemTypesRepository
    {
        private readonly ApplicationDbContext _context;

        public ItemTypesRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddType(TypeItem type)
        {
            _context.ItemTypes.Add(type);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteType(TypeItem type)
        {
            _context.ItemTypes.Remove(type);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TypeItem>> GetTypes()
        {
            return await _context.ItemTypes.ToListAsync();
        }

        public async Task<TypeItem?> GetTypeById(int id)
        {
            return await _context.ItemTypes.FindAsync(id);
        }

        public async Task UpdateType(TypeItem type)
        {
            _context.ItemTypes.Update(type);
            await _context.SaveChangesAsync();
        }
    }
}
