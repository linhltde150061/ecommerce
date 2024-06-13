using FashionShops.Models;
using FashionShops.ViewModels.News;
using Microsoft.AspNetCore.Mvc;

namespace FashionShops.ViewComponents.News
{
    public class Category_NewsViewComponent : ViewComponent
    {
        private readonly FashionShopContext db;
        public Category_NewsViewComponent(FashionShopContext context) => db = context;
        public IViewComponentResult Invoke()
        {
            var data = db.NewsCategories.Select(categories_new => new NewsCategoryVM
            {
                Id = categories_new.Id,
                Name = categories_new.Name,
                CountNews = db.News.Where(n => n.NewsCategoryId == categories_new.Id).Count(),

            });
            return View(data);
        }
    }
}
