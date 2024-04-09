using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockExChange.Core.Interfaces;
using StockExChange.Core.Models;
using StockExChange.EF.Dtos.OrderDtos;
using System;

namespace StockExChange.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class OrdersController : ControllerBase
    {
        private readonly IBaseRepository<Order> _orderRepo;


        public OrdersController(IBaseRepository<Order>  orderRepo)
        {
            _orderRepo = orderRepo;
        }


        [HttpPost("AddOrder")]

        public IActionResult CreateOrder([FromBody]CreateOrderDto orderdto)
        {
            var NewOrder = new Order
            {
                CustomerID=1,
                ItemPrice=orderdto.ItemPrice,
                Quantity=orderdto.Quantity,
                TotalPrice= orderdto.ItemPrice * orderdto.Quantity,
                CreatedAt=DateTime.Now,
                OrderType=orderdto.OrderType,
                Symbol=orderdto.symbole
            };
            var x = _orderRepo.Create(NewOrder);


            return Ok(new { message = "Sucsess",  CreatedAt = x.CreatedAt, Quantity = x.Quantity, ItemPrice = x.ItemPrice, TotalPrice = x.TotalPrice });
        }



        [HttpGet("DisplayOrders")]
        public IActionResult DisplayAllOrders()
        {
            return Ok(_orderRepo.FindAll());
        }


        [HttpPost("GetOneOrder/{id:int}")]
        [Authorize]
        public IActionResult FindByID([FromRoute] int id)
        {
            return Ok(_orderRepo.FindById(id));
        }

        [HttpGet("stocks/{symbole:alpha}/history")]
       

        public IActionResult FindByName([FromRoute] string symbole)
        {
            if(string.IsNullOrEmpty(symbole))
            {
                var x = _orderRepo.FindByName(b => b.Symbol == symbole);
                return Ok(x);
            }
            return BadRequest(new {Message= "Failed",error= "symbole can't be null", symbole });
            
        }
    }
}
