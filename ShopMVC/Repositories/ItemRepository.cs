

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;

namespace ShopMVC.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public ItemRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<TypeItem>> TypesItem()
        {
            return await _applicationDbContext.ItemTypes.ToListAsync();
        }

        public async Task<IEnumerable<Item>> GetLatestDropItems()
        {
            var allItems = await _applicationDbContext.Items.ToListAsync();
            var count = allItems.Count();
            IEnumerable<Item> latestItems = await (from item in _applicationDbContext.Items
                                                  join types in _applicationDbContext.ItemTypes on item.TypeId equals types.Id
                                                  where item.Id > count - 4
                                                   select new Item
                                                   {
                                                       Id = item.Id,
                                                       Image = item.Image,
                                                       ItemTitle = item.ItemTitle,
                                                       TypeId = item.TypeId,
                                                       Price = item.Price,
                                                       Description = item.Description,
                                                       TypeName = types.TypeTitle,
                                                       Stats = item.Stats
                                                   }).ToListAsync();
            return latestItems;
        }

        public async Task<IEnumerable<Item>> GetItems(string sTerm = "", int typeId = 0)
        {
            sTerm = sTerm.ToLower().Replace(" ", "");
            IEnumerable<Item> items = await (from item in _applicationDbContext.Items
                         join types in _applicationDbContext.ItemTypes 
                         on item.TypeId equals types.Id
                         join stock in _applicationDbContext.Stocks
                         on item.Id equals stock.ItemId
                         into items_stock
                         from itemWithstock in items_stock.DefaultIfEmpty()
                         where string.IsNullOrWhiteSpace(sTerm) || (item != null && item.ItemTitle
                         .ToLower()
                         .Replace(" ", "").Contains(sTerm)) 
                         select new Item
                         {
                             Id = item.Id,
                             Image = item.Image,
                             ItemTitle = item.ItemTitle,
                             TypeId = item.TypeId,
                             Price = item.Price,
                             Description = item.Description,
                             TypeName = types.TypeTitle,
                             Stats = item.Stats,
                             Quantity = itemWithstock == null ? 0 : itemWithstock.Quantity
                         }
                         ).ToListAsync();
            if(typeId > 0)
            {
                items = items.Where(a=> a.TypeId == typeId).ToList();
            }
            return items;
        }
    }
}
