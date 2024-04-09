using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExChange.EF.Dtos.OrderDtos
{
    public class CreateOrderDto
    {
        

        public decimal ItemPrice { get; set; }
        public string OrderType { get; set; }
        public int Quantity { get; set; }
        public int CustomerID { get; set; }

        public string symbole { get;set; }
    }
}
