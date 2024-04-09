using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace StockExChange.Api.Hubs
{
    public class PriceHub:Hub
    {
        public  async Task SendPriceUpdate(string symbol, decimal price)
        {
            await Clients.All.SendAsync("ReceivePriceUpdate", symbol, price);
        }
    }
}
