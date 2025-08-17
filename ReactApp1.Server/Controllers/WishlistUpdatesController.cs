using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReactApp1.Server.Models;
using EasyDeal.Server.Models;

namespace EasyDeal.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WishlistUpdatesController : ControllerBase
    {
        private readonly ILogger<WishlistUpdatesController> _logger;

        public WishlistUpdatesController(ILogger<WishlistUpdatesController> logger)
        {
            _logger = logger;
        }



        // Handles Post request from front end for WishlistUpdates endpoint
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GameDeal request)
        {
            _logger.LogInformation("WishlistUpdatesController Post method called.");
            AddToWishlist(request);

            return Ok(new { message = "Wishlist updated successfully." });
        }

        public bool AddToWishlist(GameDeal gameDeal)
        {
            _logger.LogInformation($"Game deal to add: {gameDeal}");
            // Implement logic to add the game deal to the wishlist
            
            // Return true if the operation was successful, otherwise false
            return true;
        }
    }
}
