namespace ProjectMVC.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }

        public int ProductId { get; set; }

        public bool TransactionType { get; set; }

        public int Quantity { get; set; }

        public DateTime Date { get; set; }

        public virtual Product Product { get; set; } = null!;
    }
}
