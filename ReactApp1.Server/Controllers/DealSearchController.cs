using Microsoft.AspNetCore.Mvc;
using ReactApp1.Server.Controllers;
using ReactApp1.Server.Models; 
using System;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

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


        // Handles Post request from front end for DealSearch endpoint
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DealSearchRequest request)
        {
            Console.WriteLine("DealSearchController Post method called.");
            Console.WriteLine($"Received query: {request?.Query}");
            //var deals = await GetGameListRequest(request.Query);
            var deals = await CheapSharkApiRequests.GetGameList(request.Query);

            //Testing new request function
            Console.WriteLine(deals[0].cheapestDealID);
            CheapSharkApiRequests.GetDealInfo(deals[0].cheapestDealID);
            CheapSharkApiRequests.GetRequestId(deals[0].gameID);


            return Ok(deals); // Serialize list of deals to JSON
        }
        

    }
}
