using EcommerceMVC.Models;
using EcommerceMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly EcommerceMvcContext _context;
        public HomeController(EcommerceMvcContext context) => _context = context;

        public IActionResult Index(int? loai)
        {
            var products = _context.HangHoas.AsQueryable();
            if (loai.HasValue)
            {
                products = products.Where(p => p.MaLoai == loai.Value);
            }
            var data = products.Select(p => new ProductsVM
            {
                ProductId = p.MaLoai,
                ProductName = p.TenHh,
                Price = p.DonGia ?? 0,
                Img = p.Hinh ?? "",
                Depscription = p.MoTaDonVi ?? "",
                categoryName = p.MaLoaiNavigation.TenLoai

            });
            return View(data);
        }

        public IActionResult Search(string? query)
        {
            var products = _context.HangHoas.AsQueryable();
            if (query != null)
            {
                products = products.Where(p => p.TenHh.Contains(query));
            }
            var data = products.Select(p => new ProductsVM
            {
                ProductId = p.MaLoai,
                ProductName = p.TenHh,
                Price = p.DonGia ?? 0,
                Img = p.Hinh ?? "",
                Depscription = p.MoTaDonVi ?? "",
                categoryName = p.MaLoaiNavigation.TenLoai

            });
            return View(data);
        }

        public IActionResult Detail(int id)
        {
            var data = _context.HangHoas
                .Include(p => p.MaLoaiNavigation)
                .SingleOrDefault(p => p.MaHh == id);
            if (data == null)
            {
                return Redirect("/404");
            }
            var result = new DetailProductVM
            {
                ProductId = id,
                ProductName = data.TenHh,
                Price = data.DonGia ?? 0,
                Depscription = data.MoTaDonVi ?? string.Empty,
                Img = data.Hinh ?? string.Empty,
                Detail = data.MoTa ?? string.Empty,
                categoryName = data.MaLoaiNavigation.TenLoai,
                Amount = 100,
                StarVote = 5

            };
            return View(result);
        }

        [Route("/404")]
        public IActionResult PageNotFound()
        {
            return View();
        }
    }
}
