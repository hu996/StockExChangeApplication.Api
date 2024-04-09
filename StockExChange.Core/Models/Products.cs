using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StockExChange.Core.Models
{
    public class Product
    {
        public int Id { get; set; } 
        public string Name { get; set; }

        public string Description { get; set; }

        private decimal _price;

        public decimal Price
        {
            set {
                if (Price < 0)
                {
                    Price = 0;
                }
                    
                _price = value;
            }
            get
            {
                return _price;
            }
        }
        //public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }

        public string Symbole { get; set; }

        //[JsonIgnore]
        //[ForeignKey("SymboleId")]
        //public StocKSymbol Symbol { get; set; }
    }
}
