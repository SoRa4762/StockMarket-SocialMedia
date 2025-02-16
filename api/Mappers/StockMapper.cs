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
        //works for both update and create so I will use this mapper to turn my update or create model into it's own DTO through this method
        public static StockDto ToStockDto(this Stock stockModel) //this keyword - makes THIS method an "extension" method of the Stock class
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                //cuz it is in list
                Comments = stockModel.Comments.Select(c => c.FromCommentModelToCommentDto()).ToList()
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