using FashionShops.Models;
using FashionShops.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;

namespace FashionShops.Controllers
{
    public class ProductController : Controller
    {
        private readonly FashionShopContext _context;
        public ProductController(FashionShopContext context) => _context = context;
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ListProduct(List<int?> Categories, List<int?> Brand, string? OrderBy = "Date", int? Page = 1)
        {
            var productInPage = 12;
            var listProduct = _context.Products.AsQueryable();
            if (Categories.Count > 0)
            {
                listProduct = listProduct.Where(p => Categories.Contains(p.CategoryId));
            }
            if (Brand.Count > 0)
            {
                listProduct = listProduct.Where(p => Brand.Contains(p.BrandId));
            }
            switch (OrderBy)
            {
                case "Date":
                    listProduct = listProduct.OrderByDescending(p => p.CreateDate);
                    break;
                case "Rated":
                    var voting = _context.ProductVotings.GroupBy(r => r.ProductId).Select(g => new
                    {
                        ProductId = g.Key,
                        AverageStar = g.Average(r => r.Star)
                    });
                    listProduct = (from v in voting
                                   join listP in listProduct on v.ProductId equals listP.Id
                                   orderby v.AverageStar descending
                                   select listP);
                    break;

            }
            int TotalPage = (int)Math.Ceiling((double)listProduct.Count() / productInPage);
            listProduct = listProduct.Skip((int)((Page - 1) * productInPage)).Take(productInPage);
            var data = listProduct.Select(product => new ProductVM
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Description = product.Description,
                ProductCategory = _context.ProductCategories.FirstOrDefault(p => p.Id == product.CategoryId).CategoryName,
                Picture = product.Picture,
                Price = product.Price,
                Voting = product.ProductVotings.Where(v => v.ProductId == product.Id).Average(s => s.Star).ToString(),
                CountVoting = product.ProductVotings.Where(v => v.ProductId == product.Id).Count(),
                PictureColor = _context.ProductDetails.Where(p => p.ProductId == product.Id).Select(p => p.Picture).Distinct().ToList(),
            });
            return new JsonResult(data);
        }
        public IActionResult Details(int Id)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == Id);
            var data = new ProductDetailVM
            {
                Id = product.Id,
                Name = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                Voting = (_context.ProductVotings.Where(v => v.ProductId == product.Id).Average(s => s.Star) * 20).ToString(),
                CountVoting = _context.ProductVotings.Where(v => v.ProductId == product.Id).Count(),
                ProductCategory = _context.ProductCategories.FirstOrDefault(p => p.Id == product.CategoryId).CategoryName,
                PictureColors = _context.ProductDetails.Where(p => p.ProductId == product.Id).Select(p => new 
                {
                    Picture = p.Picture,
                    ColorId = p.ColorId
                })
                .Distinct().Cast<dynamic>().ToList(),
                //Size = _context.ProductSizes.Where(s => _context.ProductDetails.Where(p => p.ProductId == product.Id).GroupBy(c => c.ColorId)).Select(p => p.Size).ToList();
            };
            return View(data);
        }

        public IActionResult LoadSize(int ProductId, int ColorId)
        {
            var listSize = _context.ProductDetails.AsQueryable();
            var listSizeId = listSize
                .Where(s => s.ProductId == ProductId && s.ColorId == ColorId)
                .Select(ls => ls.SizeId)
                .ToList();
            var sizeNames = _context.ProductSizes
                .Where(ps => listSizeId.Contains(ps.Id))
                .Select(ps => ps.Size)
                .ToList();
            var data = new ProductDetailVM
            {
                listSizeName = sizeNames
            };
            return new JsonResult(sizeNames);
        }
    }
}
