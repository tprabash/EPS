using API.DTOs;
using API.Entities;
using API.Interfaces;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace API.Controllers.CCSystem.Transaction
{
    public class StockAdjuesmentController : BaseApiController
    {
        private readonly IApplicationCartonDbContext _context;
        private readonly IStockAdjuestmentRepository _stockAdjuestmentRepository;
        public StockAdjuesmentController(IApplicationCartonDbContext context, IStockAdjuestmentRepository stockAdjuestmentRepository)
        {
            _stockAdjuestmentRepository = stockAdjuestmentRepository;
            _context = context;
        }

        [HttpPost("SaveAdj")]
        public async Task<IActionResult> SaveStockAdjuestment(List<TransStockAdjuestment> stockAdjuestment)
        {
            var result = await _stockAdjuestmentRepository.SaveStockAdjuestmentAsync(stockAdjuestment);
            return Ok(result);
        }


        [HttpGet("StkDt/{stockId}")]
        public async Task<IActionResult> GetAdjuestmentGetStock(int stockId)
        {
            var result = await _stockAdjuestmentRepository.GetAdjuestmentGetStockAsync(stockId);
            return Ok(result);
        }

    }
}
