﻿using ReactApp1.Server.Models;
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
using EasyDeal.Server.Models;

namespace EasyDeal.Server.Controllers
{
    public class CheapSharkApiRequests
    {
        public static async Task<List<GameDeal>> GetGameList(string game, ILogger<DealSearchController> logger)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = $"https://www.cheapshark.com/api/1.0/games?title={game}&exact=0";
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode(); // Throws an exception for 4xx/5xx responses

                    string type = response.Content.GetType().ToString();
                    logger.LogInformation($"Response type: {type}");

                    string responseBody = await response.Content.ReadAsStringAsync();
                    logger.LogInformation(responseBody);

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
                    logger.LogWarning($"Request error: {e.Message}");
                    return new List<GameDeal>(); // Return an empty list on error
                }
            }
        }

        public static async Task<BestGameDeal>GameInfoById(string id, ILogger<BestDealInfoController> logger)
        {
            var bestDeal = new BestGameDeal();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = $"https://www.cheapshark.com/api/1.0/games?id={id}";
                    HttpResponseMessage response = await client.GetAsync(url);

                    response.EnsureSuccessStatusCode(); // Throws an exception for 4xx/5xx responses

                    logger.LogInformation("Response received for Individual game id");

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
                    bestDeal.cheapestPriceEver = Price != null ? Price : "No price available";

                    if (cheapestPriceEver["date"] != null)
                    {
                        long unixTimestamp = cheapestPriceEver["date"].GetValue<long>();

                        //Set Unix timestamp value to Human date 
                        DateTimeOffset dateTime = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp);

                        string Date = dateTime.Date.ToString("MMMM dd, yyyy");
                        Console.WriteLine(Date);
                        bestDeal.date = Date != null ? Date : "No date available";
                    }

                    return bestDeal ?? new BestGameDeal();



                }
                catch (HttpRequestException e)
                {
                    logger.LogWarning($"Request error: {e.Message}");
                    return new BestGameDeal(); // Return an empty BestGameDeal on error
                }
            }
        }

        
        public static async Task GetDealInfo(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = $"https://www.cheapshark.com/api/1.0/deals?id={id}";
                    HttpResponseMessage response = await client.GetAsync(url);

                    response.EnsureSuccessStatusCode(); // Throws an exception for 4xx/5xx responses

                    Console.WriteLine("Response received");

                    string type = response.Content.GetType().ToString();

                    string responseBody = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine(responseBody);

                    // Deserialize the JSON response into a dynamic object
                    JsonNode jsonResponse = JsonNode.Parse(responseBody);
                    //Console.WriteLine(jsonResponse);
                    Console.WriteLine(jsonResponse["gameInfo"]);
                    Console.WriteLine(jsonResponse["cheapestPrice"]);

                    JsonNode gameInfo = jsonResponse["gameInfo"];
                    JsonNode cheapestPrice = jsonResponse["cheapestPrice"];

                    string retailPrice = gameInfo["retailPrice"]?.GetValue<string>();
                    Console.WriteLine($"retailPrice: {retailPrice}");

                    string steamRatingText = gameInfo["steamRatingText"]?.GetValue<string>();
                    Console.WriteLine($"steamRatingText: {steamRatingText}");

                    string steamRatingCount = gameInfo["steamRatingCount"]?.GetValue<string>();
                    Console.WriteLine($"steamRatingCount: {steamRatingCount}");

                    string metacriticScore = gameInfo["metacriticScore"]?.GetValue<string>();
                    Console.WriteLine($"metacriticScore: {metacriticScore}");

                    string dealLink = $"https://www.cheapshark.com/redirect?dealID={id}";
                    Console.WriteLine($"dealLink: {dealLink}");


                    string chepestPriceEver = cheapestPrice["price"]?.GetValue<string>();
                    Console.WriteLine($"Cheapest Price ever: {chepestPriceEver}");

                    //Check for cheapest price date
                    if (cheapestPrice["date"] != null)
                    {
                        long unixTimestamp = cheapestPrice["date"].GetValue<long>();

                        //Set Unix timestamp value to Human date 
                        DateTimeOffset dateTime = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp);

                        string Date = dateTime.Date.ToString("MMMM dd, yyyy");
                        Console.WriteLine($"chepest price date: {Date}");
                    }

                    //Check for release date
                    if (gameInfo["releaseDate"] != null)
                    {
                        long unixTimestamp = gameInfo["releaseDate"].GetValue<long>();

                        //Set Unix timestamp value to Human date 
                        DateTimeOffset dateTime = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp);

                        string releaseDate = dateTime.Date.ToString("MMMM dd, yyyy");
                        Console.WriteLine($"Release date: {releaseDate}");
                    }



                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Request error: {e.Message}");
                }
            }
        }

        public static async Task GetRequestIdOld(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = $"https://www.cheapshark.com/api/1.0/games?id={id}";
                    HttpResponseMessage response = await client.GetAsync(url);

                    response.EnsureSuccessStatusCode(); // Throws an exception for 4xx/5xx responses

                    Console.WriteLine("Response received for Individual game id");

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
