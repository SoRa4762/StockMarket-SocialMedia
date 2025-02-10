using System;
using System.Collections.Generic;
using System.Linq;
using api.Data;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// using api.Dtos.Stock;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]


    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll() //IActionResult is a wrapper function that saves you from writing whole bunch of code just to return 500 ok message with some data
        {
            var stocks = _context.Stocks.ToList().Select(s => s.ToStockDto()); //deferred execution
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id) //dotnet uses model binding to extract that string and turn it into int and make sure we use it in the code
        {
            var stock = _context.Stocks.Find(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());

        }
    }

}