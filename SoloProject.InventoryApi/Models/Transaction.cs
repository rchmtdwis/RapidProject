using System;
using System.Collections.Generic;

namespace SoloProject.InventoryApi.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public int ProductId { get; set; }

    public bool TransactionType { get; set; }

    public int Quantity { get; set; }

    public DateTime Date { get; set; }

    public virtual Product Product { get; set; } = null!;
}
