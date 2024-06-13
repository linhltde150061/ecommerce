using FashionShops.Models;
using FashionShops.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;

namespace FashionShops.ViewComponents.Product
{
    public class BrandViewComponent : ViewComponent
    {
        private readonly FashionShopContext _context;
        public BrandViewComponent(FashionShopContext context) => _context = context;
        public IViewComponentResult Invoke()
        {
            var listBrand = _context.ProductBrands.Select(brand => new ProductBrandVM
            {
                Id = brand.Id,
                BrandName = brand.BrandName,
                CountProduct = _context.Products.Where(c => c.BrandId == brand.Id).Count()
            });
            return View(listBrand);
        }
    }
}
