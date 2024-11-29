using Microsoft.EntityFrameworkCore;

namespace ShopMVC.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Stock?> GetStockByItemId(int itemId) => 
            await _context.Stocks.FirstOrDefaultAsync(x => x.ItemId == itemId);

        public async Task ManageStock(StockDTO stockDTOtoManage)
        {
            var existingStock = await GetStockByItemId(stockDTOtoManage.ItemId);
            if (existingStock == null)
            {
                var stock = new Stock
                {
                    ItemId = stockDTOtoManage.ItemId,
                    Quantity = stockDTOtoManage.Quantity
                };
                _context.Stocks.Add(stock);

            }
            else
            {
                existingStock.Quantity = stockDTOtoManage.Quantity;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<StockDisplayModel>> GetStocks(string sTerm = "")
        {
            var stocks = await (from item in _context.Items
                                join stock in _context.Stocks
                                on item.Id equals stock.ItemId
                                into item_stock
                                from itemStock in item_stock.DefaultIfEmpty()
                                where string.IsNullOrWhiteSpace(sTerm) ||
                                item.ItemTitle.ToLower().Contains(sTerm.ToLower())
                                select new StockDisplayModel
                                {
                                    ItemId = item.Id,
                                    ItemTitle = item.ItemTitle,
                                    Quantity = itemStock == null ? 0 : itemStock.Quantity
                                }).ToListAsync();
            return stocks;
        }
    }
}
