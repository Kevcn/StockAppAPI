using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financial_Market_API.Data;
using Financial_Market_API.Domain;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;

namespace Financial_Market_API.Services
{
    public class StockService : IStockService
    {
        public StockService(ApplicationDbContext dbContext)
        {
            
        }

        public Task<List<Stock>> GetStocksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Stock> GetStockBySymbolAsync(string stockSymbol)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddStockAsync(string stockSymbol)
        {
            var factory = new ConnectionFactory() {HostName = "localhost"};
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("stockExchange", ExchangeType.Topic);

                var routingKey = "get.stock.info";
                var message = $"{stockSymbol}";
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "stockExchange",
                    routingKey: routingKey,
                    basicProperties: null,
                    body: body);
                Console.WriteLine(" [x] Sent '{0}':'{1}'", routingKey, message);
            }
            return true;
        }
    }
}

