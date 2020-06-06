using Financial_Market_API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Financial_Market_API.Services
{
    public interface IStockService
    {
        Task<List<Stock>> GetStocksAsync();
        
        Task<Stock> GetStockBySymbolAsync(string stockSymbol);

        Task<bool> AddStockAsync(string stockSymbol);
    }
}
