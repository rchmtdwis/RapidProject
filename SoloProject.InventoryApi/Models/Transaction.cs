using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


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

    [ForeignKey("ProductId")]
    [InverseProperty("Transactions")]
    public virtual Product Product { get; set; }
}

