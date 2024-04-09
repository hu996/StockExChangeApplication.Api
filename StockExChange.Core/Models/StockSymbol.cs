using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExChange.Core.Models
{
    public class StocKSymbol
    {


        [Key]
        public string SymboleName { get; set; }

        public string Description { get; set; }

        public IEnumerable<Order>orders { get; set; }

        
    }
}
