using ProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.ViewModels
{
    public class TransactionViewModel
    {
        public int TransactionId { get; set; }
        public int ProductId { get; set; }
        public string TransactionType { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }

        public Product Product { get; set; }
    }
}