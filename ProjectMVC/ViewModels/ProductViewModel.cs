namespace ProjectMVC.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        
        public string TransactionType { get; set; }
        public int Quantity { get; set; }

    }
}
