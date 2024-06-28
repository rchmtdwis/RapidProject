namespace SoloProject.InventoryApi.DTOs
{
    public class CreateTransactionDTO
    {
        public int ProductId { get; set; }
        public string TransactionType { get; set; }
        public int Quantity { get; set; }
    }
}
