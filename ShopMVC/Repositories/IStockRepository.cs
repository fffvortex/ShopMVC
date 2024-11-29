namespace ShopMVC.Repositories
{
    public interface IStockRepository
    {
        Task<Stock?> GetStockByItemId(int itemId);

        Task ManageStock(StockDTO stockDTOtoManage);

        Task<IEnumerable<StockDisplayModel>> GetStocks(string Sterm = "");

    }
}