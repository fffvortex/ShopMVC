using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShopMVC.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class ItemTypesController : Controller
    {
        private readonly IItemTypesRepository _itemTypes;

        public ItemTypesController(IItemTypesRepository itemTypes)
        {
            _itemTypes = itemTypes;
        }
        public async Task<IActionResult> Index()
        {
            var types = await _itemTypes.GetTypes();
            return View(types);
        }

        public IActionResult AddType()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> AddType(TypeDTO type)
        {
            if (!ModelState.IsValid)
            {
                return View(type);
            }
            try
            {
                var typesToAdd = new TypeItem { TypeTitle = type.TypeTitle, Id = type.Id };
                await _itemTypes.AddType(typesToAdd);
                TempData["successMessage"] = "Type added";
                return RedirectToAction(nameof(AddType));
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Type not added";
                return View(type);
            }
        }

        public async Task<IActionResult> UpdateType(int id)
        {
            var type = await _itemTypes.GetTypeById(id);
            if (type == null)
            {
                throw new InvalidOperationException($"Type with id: {id} does not found");

            }
            var typeToUpdate = new TypeDTO { Id = id, TypeTitle = type.TypeTitle };
            return View(typeToUpdate);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateType(TypeDTO typeToUpdate)
        {
            if (!ModelState.IsValid)
            {
                return View(typeToUpdate);
            }
            try
            {
                var type = new TypeItem
                {
                    TypeTitle = typeToUpdate.TypeTitle,
                    Id = typeToUpdate.Id
                };
                await _itemTypes.UpdateType(type);
                TempData["successMessage"] = "Type updated";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Type could not updated";
                return View(typeToUpdate);
            }
        }

        public async Task<IActionResult> DeleteType(int id)
        {
            var type = await _itemTypes.GetTypeById(id);
            if (type == null)
            {
                throw new InvalidOperationException($"Type with id: {id} does not found");
            }
            await _itemTypes.DeleteType(type);
            return RedirectToAction(nameof(Index));
        }
    }
}
