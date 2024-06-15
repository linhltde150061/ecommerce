namespace FashionShops.ViewModels.Product
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ProductCategory { get; set; }
        public string Picture { get; set; }
        public decimal? Price { get; set; }
        public string Voting { get; set; }
        public int CountVoting { get; set; }
        public List<string> PictureColor { get; set; } = new List<string>();
        public int? ColorId { get; set; } 
        public int? SizeId { get; set; }

    }
}
