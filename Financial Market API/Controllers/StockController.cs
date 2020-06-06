
using AutoMapper;
using Contracts.V1;
using Contracts.V1.Requests;
using Contracts.V1.Responses;
using Financial_Market_API.Domain;
using Financial_Market_API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Threading.Tasks;

namespace Financial_Market_API.Controllers
{
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;
        private readonly IMapper _mapper;
        public StockController(IStockService stockService, IMapper mapper)
        {
            _stockService = stockService;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns a list of Stocks
        /// </summary>
        /// <response code="200">Creates a stock in the system</response>
        /// <response code="401">Unauthorized</response>
        [HttpGet(ApiRoutes.Stock.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockService.GetStocksAsync();
            return Ok(_mapper.Map<List<StockResponse>>(stocks));
        }

        [HttpPost(ApiRoutes.Stock.Add)]
        public async Task<IActionResult> Add([FromRoute] string stockSymbol)
        {
            // Save to DB
            await _stockService.AddStockAsync(stockSymbol);

            return Ok();
        }

        [HttpGet(ApiRoutes.Stock.Get)]
        public async Task<IActionResult> Get([FromRoute] string stockSymbol)
        {
            var stock = await _stockService.GetStockBySymbolAsync(stockSymbol);

            if (stock == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<StockResponse>(stock);

            return Ok(response);
        }
    }
}
