using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StockExChange.Core.Models
{
    public class Order
    {
        public int Id { get; set; }

        public decimal ItemPrice { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime CreatedAt { get; set; }
        public int CustomerID { get; set; }

        [JsonIgnore]
        public Customer Customer { get; set; }

        public string OrderType { get; set; }
        public string Symbol { get; set; }


        //[ForeignKey("SymbolId")]
        //public StocKSymbol StocKSymbol { get; set; }
    }
}
