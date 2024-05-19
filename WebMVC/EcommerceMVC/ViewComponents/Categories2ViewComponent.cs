using EcommerceMVC.Models;
using EcommerceMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceMVC.ViewComponents
{
    public class Categories2ViewComponent : ViewComponent
    {
        private readonly EcommerceMvcContext db;

        public Categories2ViewComponent(EcommerceMvcContext context) => db = context;
        public IViewComponentResult Invoke()
        {
            var data = db.Loais.Select(categories => new CategoriesVM
            {
                categoryCode = categories.MaLoai,
                categoryName = categories.TenLoai,
                amount = categories.HangHoas.Count()

            });
            return View(data);
        }
    }
}
