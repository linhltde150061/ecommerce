using EcommerceMVC.Helpers;
using EcommerceMVC.Models;
using EcommerceMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceMVC.Controllers
{
    public class CartController : Controller
    {
        private readonly EcommerceMvcContext db;

        public CartController(EcommerceMvcContext _context)
        {
            db = _context;
        }

        public List<CartVM> Cart => HttpContext.Session.Get<List<CartVM>>(MyConfig.Cart_Key) ?? new List<CartVM>();
        public IActionResult Index()
        {
            return View(Cart);
        }

        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var cart = Cart;
            var item = cart.SingleOrDefault(p => p.ProductId == id);
            if (item == null)
            {
                var product = db.HangHoas.SingleOrDefault(p => p.MaHh == id);
                if (product == null)
                {
                    return Redirect("/404");
                }
                item = new CartVM
                {
                    ProductId = id,
                    ProductName = product.TenHh,
                    Price = product.DonGia ?? 0,
                    Img = product.Hinh ?? String.Empty,
                    Amount = quantity
                };
                cart.Add(item);
            }
            else
            {
                item.Amount += quantity;
            }
            HttpContext.Session.Set(MyConfig.Cart_Key, cart);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveCart(int id)
        {
            var cart = Cart;
            var item = cart.SingleOrDefault(p => p.ProductId == id);
            if (item != null)
            {
                cart.Remove(item);
                HttpContext.Session.Set(MyConfig.Cart_Key, cart);
            }
            return RedirectToAction("Index");
        }
    }
}
