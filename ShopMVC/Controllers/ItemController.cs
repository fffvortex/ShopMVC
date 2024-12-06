using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopMVC.Models;
using ShopMVC.Shared;

namespace ShopMVC.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class ItemController : Controller
    {
        private readonly IServiceItemsRepository _itemsRepository;
        private readonly IItemTypesRepository _typesRepository;
        private readonly IFileService _fileService;

        public ItemController(IServiceItemsRepository itemRepository,
            IItemTypesRepository itemTypesRepository, IFileService fileService)
        {
            _itemsRepository = itemRepository;
            _typesRepository = itemTypesRepository;
            _fileService = fileService;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _itemsRepository.GetItems();
            return View(items);
        }

        public async Task<IActionResult> AddItem()
        {
            var typesSelectedList = (await _typesRepository
                .GetTypes())
                .Select(type => new SelectListItem
                {
                    Text = type.TypeTitle,
                    Value = type.Id.ToString()
                });
            ItemDTO itemToAdd = new()
            {
                TypeList = typesSelectedList
            };
            return View(itemToAdd);
        }
        [HttpPost]
        public async Task<IActionResult> AddItem(ItemDTO itemToAdd)
        {
            var typesSelectList = (await _typesRepository
                .GetTypes())
                .Select(type => new SelectListItem
                {
                    Text = type.TypeTitle,
                    Value = type.Id.ToString()
                });
            itemToAdd.TypeList = typesSelectList;
            if (!ModelState.IsValid)
            {
                return View(itemToAdd);
            }
            try
            {
                if (itemToAdd.ImageFile != null)
                {
                    if (itemToAdd.ImageFile.Length > 1 * 1024 * 1024)
                    {
                        throw new InvalidOperationException
                            ("Image file can not exceed 1 MB");
                    }
                    string[] allowedExtentions = [".png"];
                    string imageName = await _fileService
                        .SaveFile(itemToAdd.ImageFile, allowedExtentions);
                    itemToAdd.Image = imageName;
                }
                Item item = new()
                {
                    Id = itemToAdd.Id,
                    ItemTitle = itemToAdd.ItemTitle,
                    Stats = itemToAdd.Stats,
                    Description = itemToAdd.Description,
                    Price = itemToAdd.Price,
                    TypeId = itemToAdd.TypeId,
                    Image = itemToAdd.Image

                };
                await _itemsRepository.AddItem(item);
                TempData["succesMessage"] = "Item is added!";
                return RedirectToAction(nameof(AddItem));
            }
            catch (InvalidOperationException ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(itemToAdd);
            }
            catch (FileNotFoundException ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(itemToAdd);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Error saving data";
                return View(itemToAdd);
            }
        }

        public async Task<IActionResult> UpdateItem(int id)
        {
            var item = await _itemsRepository.GetItemById(id);
            if (item == null)
            {
                TempData["errorMessage"] = $"Item with id: {id} does not found";
                return RedirectToAction(nameof(Index));
            }
            var typeSelectedList = (await _typesRepository.GetTypes())
                .Select(type => new SelectListItem
                {
                    Text = type.TypeTitle,
                    Value = type.Id.ToString(),
                    Selected = type.Id == item.TypeId
                });
            ItemDTO itemToUpdate = new()
            {
                TypeList = typeSelectedList,
                ItemTitle = item.ItemTitle,
                Description = item.Description,
                Stats = item.Stats,
                Price = item.Price,
                TypeId = item.TypeId,
                Image = item.Image
            };
            return View(itemToUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateItem(ItemDTO itemToUpdate)
        {
            var typeSelectedList = (await _typesRepository.GetTypes())
                .Select(type => new SelectListItem
                {
                    Text = type.TypeTitle,
                    Value = type.Id.ToString(),
                    Selected = type.Id == itemToUpdate.TypeId
                });
            itemToUpdate.TypeList = typeSelectedList;

            if (!ModelState.IsValid)
            {
                return View(itemToUpdate);
            }
            try
            {
                string oldImage = "";
                if (itemToUpdate.ImageFile != null)
                {
                    if (itemToUpdate.ImageFile.Length > 1 * 1024 * 1024)
                    {
                        throw new InvalidOperationException
                            ("Image file can not exceed 1MB");
                    }
                    string[] allowedExtentions = [".png"];
                    string imageName = await _fileService.SaveFile(itemToUpdate
                        .ImageFile, allowedExtentions);
                    oldImage = itemToUpdate.Image;
                    itemToUpdate.Image = imageName;
                }

                Item item = new()
                {
                    Id = itemToUpdate.Id,
                    ItemTitle = itemToUpdate.ItemTitle,
                    Stats = itemToUpdate.Stats,
                    Description = itemToUpdate.Description,
                    Price = itemToUpdate.Price,
                    TypeId = itemToUpdate.TypeId,
                    Image = itemToUpdate.Image
                };

                await _itemsRepository.UpdateItem(item);

                if (!string.IsNullOrWhiteSpace(oldImage))
                {
                    _fileService.DeleteFile(oldImage);
                }
                TempData["successMessage"] = "Item is updated";
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(itemToUpdate);
            }
            catch (FileNotFoundException ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(itemToUpdate);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Error saving data";
                return View(itemToUpdate);
            }
        }

        public async Task<IActionResult> DeleteItem(int id)
        {
            try
            {
                var item = await _itemsRepository.GetItemById(id);
                if (item == null)
                {
                    TempData["errorMessage"]= $"Item with id: {id} does not found";
                }
                else
                {
                    await _itemsRepository.DeleteItem(item);
                    if (!string.IsNullOrWhiteSpace(item.Image))
                    {
                        _fileService.DeleteFile(item.Image);
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            catch (FileNotFoundException ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Error on deleting the data";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
