using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShopMVC.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class StockController : Controller
    {

        private readonly IStockRepository _stockRepository;

        public StockController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }
        public async Task<IActionResult> Index(string sTerm = "")
        {
            var stocks = await _stockRepository.GetStocks(sTerm);
            return View(stocks);
        }

        public async Task<IActionResult> ManageStock(int itemId)
        {
            var existingStock = await _stockRepository.GetStockByItemId(itemId);

            var stock = new StockDTO
            {
                ItemId = itemId,
                Quantity = existingStock != null ? existingStock.Quantity : 0
            };

            return View(stock);
        }

        [HttpPost]
        public async Task<IActionResult> ManageStock(StockDTO stock)
        {
            if (!ModelState.IsValid)
            {
                return View(stock);
            }
            try
            {
                await _stockRepository.ManageStock(stock);
                TempData["successMessage"] = "stock is updated";
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "stock update failed";
            }

            return RedirectToAction(nameof(Index));
            
        }
    }
}
