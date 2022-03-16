using Microsoft.EntityFrameworkCore;
using RestaurantRaterAPI.Models;
using static RestaurantRaterAPI.Models.Ratings;

namespace RestaurantRaterAPI
{
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext(DbContextOptions <RestaurantDbContext> options) : base (options){}
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}