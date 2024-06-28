

namespace SoloProject.InventoryApi.DTOs
{
    public class TransactionDTO
    {
        public int TransactionId { get; set; }
        public int ProductId { get; set; }
        public string TransactionType { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public ProductDTO Product { get; set; }
    }
}
