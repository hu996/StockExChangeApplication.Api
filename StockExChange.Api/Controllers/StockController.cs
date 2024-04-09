using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StockExChange.Api.Hubs;
using StockExChange.Core.Interfaces;
using StockExChange.Core.Models;
using StockExChange.EF.Dtos.ProductDtos;
using System.Threading.Tasks;

namespace StockExChange.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 
    public class StockController : ControllerBase
    {
        private readonly IBaseRepository<Product> _productRep;
        private readonly IHubContext<PriceHub> _hubContext;
        public StockController(IBaseRepository<Product> productRep, IHubContext<PriceHub>huncontext)
        {
            _productRep = productRep;   
            _hubContext=huncontext;
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateProduct(CreateProductDto productdto)
        {
            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);  
            }

            var newproduct = new Product
            {
                Name= productdto.Name,
                Price= productdto.Price,
                Description= productdto.Description,
                Symbole= productdto.Symbole,
            };
            return Ok();
        }


        [HttpGet("stocks")]

        public IActionResult DisplayProducts()
        {
            return Ok(_productRep.FindAll());
        }

        [HttpGet("Stocks/{symbole:alpha}/history")]

        public IActionResult DisplayProductHistory([FromRoute] string symbole)
        {
            if (symbole!=null)
            {
                return Ok(_productRep.FindByName(a => a.Symbole == symbole));
            }

           

            return BadRequest(new { status = "Failed", message = "symbole can't be null",symbole= symbole });
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdatePrice([FromBody] PriceUpdateDto model)
        {
            
            await _hubContext.Clients.All.SendAsync("ReceivePriceUpdate", model.Symbole, model.Price);
            return Ok();
        }


    }
}
