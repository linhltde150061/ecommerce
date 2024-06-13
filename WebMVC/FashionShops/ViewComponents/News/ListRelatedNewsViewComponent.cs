using FashionShops.Models;
using FashionShops.ViewModels.News;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FashionShops.ViewComponents.News
{
    public class ListRelatedNewsViewComponent : ViewComponent
    {
        private readonly FashionShopContext _context;
        public ListRelatedNewsViewComponent(FashionShopContext context) => _context = context;

        public IViewComponentResult Invoke(int? Id_NewsCategory, int? IdNews)
        {
            var listNews = _context.News
                .OrderByDescending(news => news.CreateDate)
                .Include(news => news.NewsCategory)
                .Where(news => news.NewsCategoryId == Id_NewsCategory && news.Id != IdNews)
                .Select(news => new NewsVM
                {
                    Id = news.Id,
                    Picture = news.Picture,
                    CreateDate = news.CreateDate,
                    Title = news.Title,
                    NewsCategoryName = news.NewsCategory.Name,
                    CountComment = _context.NewsComments.Count(cmt => cmt.NewsId == news.Id)
                }).Take(5).ToList();
            if (listNews.Count() < 5)
            {
                var moreNews = _context.News
                .Include(news => news.NewsCategory)
                .Where(news => news.Id != IdNews)
                .Select(news => new NewsVM
                {
                    Id = news.Id,
                    Picture = news.Picture,
                    CreateDate = news.CreateDate,
                    Title = news.Title,
                    NewsCategoryName = news.NewsCategory.Name,
                    CountComment = _context.NewsComments.Count(cmt => cmt.NewsId == news.Id)
                }).Take(5 - listNews.Count()).ToList();
                listNews.AddRange(moreNews);
            }
            return View(listNews);
        }
    }
}
