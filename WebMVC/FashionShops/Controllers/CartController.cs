using FashionShops.Models;
using Microsoft.AspNetCore.Mvc;
using FashionShops.Helper;
using FashionShops.ViewModels.Cart;

namespace FashionShops.Controllers
{
	public class CartController : Controller
	{
		private readonly FashionShopContext _context;
		public CartController(FashionShopContext context) => _context = context;

		const string Cart_Key = "Cart";
		public List<CartVM> myCarts => HttpContext.Session.Get<List<CartVM>>(Cart_Key) ?? new List<CartVM>();

		public IActionResult ViewCart()
		{

			return View(myCarts);
		}

		public IActionResult GetCart()
		{
			var getCart = myCarts;
			return new JsonResult(getCart);
		}

		public IActionResult AddToCart(int productId, int colorId, int sizeId, int amount = 1)
		{
			var cart = myCarts;
			var item = cart.SingleOrDefault(p => p.ProductId == productId && p.ColorId == colorId && p.SizeId == sizeId);
			if (item == null)
			{
				var product = _context.Products.Find(productId);
				var productDetail = _context.ProductDetails.Where(p => p.ProductId == productId && p.ColorId == colorId && p.SizeId == sizeId).FirstOrDefault();
				item = new CartVM
				{
					ProductId = product.Id,
					ProductName = product.ProductName,
					Price = product.Price ?? 0,
					ColorId = productDetail.ColorId,
					SizeId = productDetail.SizeId,
					Picture = productDetail.Picture,
					sizeName = _context.ProductSizes.SingleOrDefault(s => s.Id == productDetail.SizeId).Size,
                    AmountInShop = productDetail.Amount,
                    amount = amount
				};
				cart.Add(item);
			}
			else
			{
				item.amount += amount;
			}
			HttpContext.Session.Set(Cart_Key, cart);
			return new JsonResult("");
		}

		public IActionResult RemoveICart(int productId, int colorId, int sizeId)
		{
			var cart = myCarts;
			var item = cart.SingleOrDefault(p => p.ProductId == productId && p.ColorId == colorId && p.SizeId == sizeId);
			if (item != null)
			{
				cart.Remove(item);
				HttpContext.Session.Set(Cart_Key, cart);
			}
			return new JsonResult("");
		}
	}

}
