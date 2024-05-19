using EcommerceMVC.Models;
using EcommerceMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceMVC.ViewComponents
{
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly EcommerceMvcContext db;

        public CategoriesViewComponent(EcommerceMvcContext context) => db = context;
        public IViewComponentResult Invoke()
        {
            var data = db.Loais.Select(categories => new CategoriesVM
            {
                categoryCode = categories.MaLoai,
                categoryName = categories.TenLoai
                
            });
            return View(data);
        }
    }
}
