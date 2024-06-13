namespace FashionShops.ViewModels.News
{
    public class NewsVM
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Picture { get; set; }

        public DateOnly? CreateDate { get; set; }

        public string? CreateByName { get; set; }
        public string? NewsCategoryName { get; set; }
        public int Id_NewsCategory { get; set; }
        public int CountComment { get; set; }
    }
}
