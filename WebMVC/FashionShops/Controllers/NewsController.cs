using FashionShops.Models;
using FashionShops.ViewModels.News;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Diagnostics;

namespace FashionShops.Controllers
{
    public class NewsController : Controller
    {
        private readonly FashionShopContext _context;
        public NewsController(FashionShopContext context) => _context = context;
        public IActionResult Index(int? News_CategoryId, int Page = 1)
        {
            int PageSize = 9;
            var news = _context.News.AsQueryable();
            if (News_CategoryId.HasValue)
            {
                news = news.Where(n => n.NewsCategoryId == News_CategoryId.Value);
            }
            int TotalPage = (int)Math.Ceiling((double)news.Count() / PageSize);
            news = news.OrderByDescending(n => n.CreateDate).Skip((Page - 1) * PageSize).Take(PageSize);
            var data = news.Select(n => new NewsVM
            {
                Id = n.Id,
                Title = n.Title,
                Description = n.Description,
                Picture = n.Picture,
                CreateDate = n.CreateDate,
                CreateByName = _context.Members.FirstOrDefault(m => m.Id == n.CreateBy).Name,
                NewsCategoryName = _context.NewsCategories.FirstOrDefault(cate => cate.Id == n.NewsCategoryId).Name,
                CountComment = _context.NewsComments.Where(cmt => cmt.NewsId == n.Id).Count(),
            }).ToList();
            var dataInPage = new NewsInPageVM
            {
                NewsList = data,
                TotalPage = TotalPage,
                CurrentPage = Page
            };
            ViewBag.News_CategoryId = News_CategoryId;
            return View(dataInPage);
        }

        public IActionResult Detail(int? NewsId)
        {
            var news = _context.News.SingleOrDefault(n => n.Id == NewsId);
            if (news == null)
            {
                return Redirect("/404");
            }
            var data = new NewsVM
            {
                Id = news.Id,
                Title = news.Title,
                Description = news.Description,
                Picture = news.Picture,
                CreateDate = news.CreateDate,
                CreateByName = _context.Members.FirstOrDefault(m => m.Id == news.CreateBy).Name,
                NewsCategoryName = _context.NewsCategories.FirstOrDefault(cate => cate.Id == news.NewsCategoryId).Name,
                Id_NewsCategory = _context.NewsCategories.FirstOrDefault(cate => cate.Id == news.NewsCategoryId).Id,
                CountComment = _context.NewsComments.Where(cmt => cmt.NewsId == news.Id).Count(),
            };

            return View(data);
        }

        
    }
}
