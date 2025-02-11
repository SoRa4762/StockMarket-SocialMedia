using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Models;
using Microsoft.EntityFrameworkCore;
// using api.Dtos.Stock;

namespace api.Mappers
{
    public static class StockMappers //static because they are going to be extention methods
    {
        //here I have passed Stock - Model as parameters, because I want to manipulate data of the model
        //here StockDto is the return type of the ToStockDto method
        //turns Model to DTO
        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap
            };
        }

        //here I have passed CreateStockRequestDto - DTO as parameters, because I want the data to be manipulated through the DTO
        //here Stock is the return type of method, it turns CreateStockRequestDto to Stock
        //turns DTO to model
        public static Stock ToStockFromCreateDTO(this CreateStockRequestDto stockDto)
        {
            return new Stock
            {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap
            };
        }
    }
}