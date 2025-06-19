using Microsoft.AspNetCore.Mvc;
using ReactApp1.Server.Controllers;
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
        public async Task<IActionResult> Post()
        {
            // Example: return a simple JSON object
            Console.WriteLine("DealSearchController Post method called.");
            Console.WriteLine(Post().ToString);

            var result = new { message = "Deals fetched successfully" };
            return Ok(result);
        }
    }
}
