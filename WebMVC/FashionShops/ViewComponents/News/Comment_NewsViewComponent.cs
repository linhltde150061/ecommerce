using FashionShops.Models;
using FashionShops.ViewModels.News;
using Microsoft.AspNetCore.Mvc;

namespace FashionShops.ViewComponents.News
{
    public class Comment_NewsViewComponent : ViewComponent
    {
        private readonly FashionShopContext _context;
        public Comment_NewsViewComponent(FashionShopContext context) => _context = context;
        public IViewComponentResult Invoke(int? NewsId)
        {
            var data = _context.NewsComments
                .Where(cmt => cmt.NewsId == NewsId)
                .Select(cmt => new CommentVM
                {
                    NameMember = _context.Members.SingleOrDefault(m => m.Id == cmt.CommentBy).Name,
                    Avatar = _context.Members.SingleOrDefault(m => m.Id == cmt.CommentBy).Avatar,
                    DateComment = cmt.CreateDate,
                    Comment = cmt.Content,
                });
            var CountComment = _context.NewsComments.Where(c => c.NewsId == NewsId).Count();
            ViewBag.CountComment = CountComment;
            return View(data);
        }
    }
}
