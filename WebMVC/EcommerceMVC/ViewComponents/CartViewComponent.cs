using EcommerceMVC.Helpers;
using EcommerceMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceMVC.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<CartVM>>(MyConfig.Cart_Key) ?? new List<CartVM>();
            return View("CartNavbar", new CartVM
            {
                Amount = cart.Sum(x => x.Amount)
            });
        }
    }
}
