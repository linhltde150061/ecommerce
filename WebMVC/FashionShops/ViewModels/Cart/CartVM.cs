using FashionShops.Models;

namespace FashionShops.ViewModels.Cart
{
    public class CartVM
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int? ColorId { get; set; }
        public int? SizeId { get; set; }
        public string sizeName { get; set; }
        public string Picture { get; set; }
        public int? AmountInShop { get; set; }
        public int amount { get; set; }
    }
}
