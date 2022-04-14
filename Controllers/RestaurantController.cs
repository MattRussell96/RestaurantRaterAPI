using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantRaterAPI.Models;

namespace RestaurantRaterAPI.Controllers
{
    [ApiController]
    [Route("controller")]
    public class RestaurantController : Controller
    {
        public double AverageRating 
        { 
            get 
            {
                if (Ratings.Count == 0)
                {
                    return 0;
                }
                double total = 0.0;
                foreach (Rating rating in Ratings)
                {
                    total += rating.Score;
                }
                return total / Ratings.Count;
            }
        }
        public virtual List<Rating> Ratings { get; set; } = new List<Rating>();
        private RestaurantDbContext _context;
        public RestaurantController(RestaurantDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostRestaurant([FromForm] RestaurantEdit model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Restaurants.Add(new Restaurant()
            {
                Name = model.Name,
                Location = model.Location,
            });

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> PostRestaurant([FromForm] RatingEdit model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Ratings.Add(new Rating()
            {
                Score = model.Score,
                RestaurantId = model.RestaurantId,
            });

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRestaurants()
        {
            var restaurants = await _context.Restaurants.Include(r => r.Ratings).ToListAsync();
            return Ok(restaurants);
        }

        
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetRestaurantById(int id)
        {
            var restaurant = await _context.Restaurants.Include(r=> r.Ratings).FirstOrDefaultAsync(r => r.Id == id);

            if (restaurant == null)
            {
                return NotFound();
            }
            return Ok(restaurant);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateRestaurant([FromForm] RestaurantEdit model, [FromRoute] int id)
        {
        var oldRestaurant = await _context.Restaurants.FindAsync(id);

        if (oldRestaurant == null)
        {
            return NotFound();
        }
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        if (!string.IsNullOrEmpty(model.Name))
        {
            oldRestaurant.Name = model.Name;
        }
        if (!string.IsNullOrEmpty(model.Location))
        {
            oldRestaurant.Location = model.Location;
        }

        await _context.SaveChangesAsync();
        return Ok();
        
        }

        [HttpDelete]
        [Route("{id}")]

        public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}