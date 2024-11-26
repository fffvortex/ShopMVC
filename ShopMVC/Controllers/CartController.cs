﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShopMVC.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<IActionResult> AddItem(int itemId, int qty=1, int redirect = 0)
        {
            var cartCount = await _cartRepository.AddItem(itemId, qty);
            if (redirect == 0)
                return Ok(cartCount);
            return RedirectToAction("GetUserCart");
        }

        public async Task<IActionResult> RemoveItem(int itemId)
        {
            var cartCount = await _cartRepository.RemoveItem(itemId);

            return RedirectToAction("GetUserCart");
        }

        public async Task<IActionResult> GetUserCart()
        {
            var cart = await _cartRepository.GetUserCart();
            return View(cart);
        }

        public async Task<IActionResult> GetTotalItemInCart()
        {
            int cartItemCount = await _cartRepository.GetCartItemsCount();
            return Ok(cartItemCount);
        }

        public async Task<IActionResult> Checkout()
        {
            bool isCheckOut = await _cartRepository.DoCheckout();
            if (!isCheckOut)
            {
                throw new Exception("Something happend in server side");
            }
            return RedirectToAction("Items", "Home");
        }
    }
}
