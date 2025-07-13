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
            var deals = await GetRequest(request.Query);

            return Ok(deals); // Serialize list of deals to JSON
        }

        static async Task<List<GameDeal>> GetRequest(string game)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = $"https://www.cheapshark.com/api/1.0/games?title={game}&exact=0"; 
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode(); // Throws an exception for 4xx/5xx responses

                    string type = response.Content.GetType().ToString();
                    Console.WriteLine($"Response type: {type}");

                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody);

                    // Deserialize the JSON response into a dynamic object
                    JsonNode jsonResponse = JsonNode.Parse(responseBody);

                    var deals = System.Text.Json.JsonSerializer.Deserialize<List<GameDeal>>(responseBody);

                    foreach (var entry in jsonResponse.AsArray())
                    {
                        //Access properties
                        Console.WriteLine(entry);
                        Console.WriteLine();

                        // Check for null before accessing 
                        if (entry?["gameID"] != null)
                        {
                            Console.WriteLine(entry["gameID"]);
                            //Console.WriteLine(entry["gameID"].GetType());
                            
                            // Extract game id as string from the Json Object
                            string id = entry["gameID"].GetValue<string>();
                            GetRequestId(id);
                        }
                        
                        Console.WriteLine();
                    }

                    return deals ?? new List<GameDeal>();

                    

                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Request error: {e.Message}");
                    return new List<GameDeal>(); // Return an empty list on error
                }
            }
        }


        static async Task GetRequestId(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = $"https://www.cheapshark.com/api/1.0/games?id={id}"; // Replace with your API URL
                    HttpResponseMessage response = await client.GetAsync(url);

                    response.EnsureSuccessStatusCode(); // Throws an exception for 4xx/5xx responses

                    Console.WriteLine("Response received");

                    string type = response.Content.GetType().ToString();

                    string responseBody = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine(responseBody);

                    // Deserialize the JSON response into a dynamic object
                    JsonNode jsonResponse = JsonNode.Parse(responseBody);
                    //Console.WriteLine(jsonResponse);
                    Console.WriteLine(jsonResponse["cheapestPriceEver"]);

                    JsonNode cheapestPriceEver = jsonResponse["cheapestPriceEver"];
                    string Price = cheapestPriceEver["price"]?.GetValue<string>();
                    Console.WriteLine(Price);

                    if (cheapestPriceEver["date"] != null)
                    {
                        long unixTimestamp = cheapestPriceEver["date"].GetValue<long>();

                        //Set Unix timestamp value to Human date 
                        DateTimeOffset dateTime = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp);

                        string Date = dateTime.Date.ToString("MMMM dd, yyyy");
                        Console.WriteLine(Date); 
                    }
                    


                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Request error: {e.Message}");
                }
            }
        }

    }
}
