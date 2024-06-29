using System;
using System.Collections.Generic;

namespace SoloProject.InventoryApi.Models;


public partial class Product
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int Stock { get; set; }

    public decimal Price { get; set; }


    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}

    [InverseProperty("Product")]
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}

