using EasyDeal.Server.Data;
using EasyDeal.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReactApp1.Server.Models;

namespace EasyDeal.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WishlistUpdatesController : ControllerBase
    {
        private readonly ILogger<WishlistUpdatesController> _logger;
        private readonly ApplicationDbContext _context;

        public WishlistUpdatesController(ILogger<WishlistUpdatesController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Handles Post request from front end for WishlistUpdates endpoint
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GameDeal request)
        {
            _logger.LogInformation("WishlistUpdatesController Post method called.");

            // Get logged in user id 
            string? userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("No user is logged in.");
                return Unauthorized(new { message = "User is not logged in." });
            }

            _logger.LogInformation($"User id: {userId}");

            AddToWishlist(request, userId);

            return Ok(new { message = "Wishlist updated successfully." });
        }

        public bool AddToWishlist(GameDeal gameDeal, string userid)
        {
            _logger.LogInformation($"Game deal to add: {gameDeal.external}");
            // Implement logic to add the game deal to the wishlist

            // Map GameDeal to Wishlist
            Wishlist wishlistEntry = new Wishlist
            {
                GameName = gameDeal.external, // or use another property if more appropriate
                GameId = gameDeal.gameID,
                DateAdded = DateTime.UtcNow,
                UserId = userid
            };

            _context.Wishlists.Add(wishlistEntry);
            int result = _context.SaveChanges();

            return result > 0; // returns true if at least one row was affected

        }
    }
}
