using FashionShops.Models;
using FashionShops.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;

namespace FashionShops.ViewComponents.Product
{
    public class Category_ProductViewComponent : ViewComponent
    {
        private readonly FashionShopContext _context;
        public Category_ProductViewComponent(FashionShopContext context) => _context = context;
        public IViewComponentResult Invoke()
        {
            var listCategory = _context.ProductCategories.Select(cate => new ProductCategoryVM
            {
                Id = cate.Id,
                CategoryName = cate.CategoryName,
                CountProduct = _context.Products.Where(c => c.CategoryId == cate.Id).Count()
            });
            return View(listCategory);
        }
    }
}
