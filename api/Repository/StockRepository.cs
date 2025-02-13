using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context; //will throw error if it is not initialized witin a constructor
        //create a constructor
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public Task<Stock?> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Stock>> GetAllAsync()
        {
            return _context.Stocks.ToListAsync();
        }

        public Task<Stock?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            throw new NotImplementedException();
        }
    }
}