using ReactApp1.Server.Models;
using System.Text.Json.Nodes;
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
    public class CheapSharkApiRequests
    {
        public static async Task<List<GameDeal>> GetGameList(string game)
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

                        /*
                        // Check for null before accessing 
                        if (entry?["gameID"] != null)
                        {
                            Console.WriteLine(entry["gameID"]);
                            //Console.WriteLine(entry["gameID"].GetType());
                            
                            // Extract game id as string from the Json Object
                            string id = entry["gameID"].GetValue<string>();
                            GetRequestId(id);
                        }
                        */
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
    }
}
