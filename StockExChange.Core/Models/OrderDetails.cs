using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExChange.Core.Models
{
    [NotMapped]
    public class OrderDetails
    {
        
        public int Id { get; set; }

        public decimal ItemPrice { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }

        public int productId { get; set; }

        public Product Product { get; set; }    
    }
}
