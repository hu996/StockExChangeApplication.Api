using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExChange.EF.Dtos.ProductDtos
{
    public class PriceUpdateDto
    {
        public string Symbole { get; set; }

        public decimal Price { get; set; }
    }
}
