using Microsoft.AspNetCore.Mvc;
using ReactApp1.Server.Controllers;
using ReactApp1.Server.Models; 
using System;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace EasyDeal.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DealSearchController : ControllerBase
    {
        private readonly ILogger<DealSearchController> _logger;

        public DealSearchController(ILogger<DealSearchController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DealSearchRequest request)
        {
            Console.WriteLine("DealSearchController Post method called.");
            Console.WriteLine($"Received query: {request?.Query}");

            var result = new { message = "Deals fetched successfully" };
            Console.WriteLine(result.GetType);
            return Ok(result);
        }
    }
}
