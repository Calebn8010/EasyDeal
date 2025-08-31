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

        public WishlistUpdatesController(ILogger<WishlistUpdatesController> logger)
        {
            _logger = logger;
        }


        private readonly ApplicationDbContext _context;

        public WishlistUpdatesController(ApplicationDbContext context)
        {
            _context = context;
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
            _logger.LogInformation($"Game deal to add: {gameDeal.external}");
            // Implement logic to add the game deal to the wishlist
            var tableNames = _context.Model.GetEntityTypes()
            .Select(t => t.GetTableName())
            .Distinct()
            .ToList();

            _logger.LogInformation("Tables in DbContext: {Tables}", string.Join(", ", tableNames));

            // Return true if the operation was successful, otherwise false
            return true;
        }
    }
}
