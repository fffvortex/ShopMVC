using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ShopMVC.Models;
using ShopMVC.Models.DTOs;

namespace ShopMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IItemRepository _itemRepository;

        public HomeController(ILogger<HomeController> logger, IItemRepository itemRepository)
        {
            _logger = logger;
            _itemRepository = itemRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Item> items = await _itemRepository.GetLatestDropItems();
            return View(items);
        }

        public async Task<IActionResult> Items(string sTerm = "", int typeId = 0)
        {
            
            IEnumerable<Item> items = await _itemRepository.GetItems(sTerm, typeId);
            IEnumerable<TypeItem> types = await _itemRepository.TypesItem();

            ItemDisplayModel itemModel = new()
            {
                Items = items,
                TypeItems = types,
                STerm = sTerm,
                TypeId = typeId
            };

            return View(itemModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
