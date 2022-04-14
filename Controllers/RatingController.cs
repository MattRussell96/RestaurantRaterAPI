using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantRaterAPI.Controllers
{
    [ApiController]
    [Route("controller")]
    public class RatingController
    {
        private RestaurantDbContext _context;
        public RatingController(RestaurantDbContext context)
        {
            _context = context;
        }
        
    }
}