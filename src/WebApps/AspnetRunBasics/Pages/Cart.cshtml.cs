using AspnetRunBasics.Models;
using AspnetRunBasics.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetRunBasics
{
    public class CartModel : PageModel
    {
        private readonly IBasketService _basketService;
        private readonly string _username;

        public CartModel(IBasketService basketService)
        {
            _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
            _username = "ms";
        }

        public BasketModel Cart { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Cart = await _basketService.GetByUsername(_username);

            return Page();
        }

        public async Task<IActionResult> OnPostRemoveToCartAsync(string productId)
        {
            var basket = await _basketService.GetByUsername(_username);
            var itemToRemove = basket.Items.Single(i => i.ProductId == productId);
            basket.Items.Remove(itemToRemove);
            await _basketService.Update(basket);

            return RedirectToPage();
        }
    }
}