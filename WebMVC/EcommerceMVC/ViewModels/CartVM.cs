namespace EcommerceMVC.ViewModels
{
    public class CartVM
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Img { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Amount { get; set;}
        public double TotalPrice => Price * Amount;

    }
}
