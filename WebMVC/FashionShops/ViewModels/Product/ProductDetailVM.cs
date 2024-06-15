namespace FashionShops.ViewModels.Product
{
    public class ProductDetailVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public string Voting { get; set; }
        public int CountVoting { get; set; }
        public string ProductCategory { get; set; }
        public List<dynamic> PictureColors { get; set; } = new List<dynamic>();
        public List<int?> listSizeId { get; set; } = new List<int?>();
        public int countProduct {  get; set; }
    }
}
