
using Microsoft.EntityFrameworkCore;

namespace ShopMVC.Repositories
{
    public class ServiceItemsRepository : IServiceItemsRepository
    {
        private readonly ApplicationDbContext _context;

        public ServiceItemsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddItem(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateItem(Item item)
        {
            _context.Items.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteItem(Item item)
        {
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<Item?> GetItemById(int id) =>
            await _context.Items.FindAsync(id);

        public async Task<IEnumerable<Item>> GetItems() => 
            await _context.Items.Include(a => a.ItemType).ToListAsync();
    }
}
