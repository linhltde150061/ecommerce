namespace FashionShops.ViewModels.News
{
    public class NewsInPageVM
    {
        public IEnumerable<NewsVM> NewsList { get; set; }
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
    }
}
