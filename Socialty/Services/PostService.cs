using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Socialty.Contexts;

namespace Socialty.Services
{
    public class PostService : IPostService
    {
        private readonly AppDbContext _context;

        public PostService(AppDbContext context) {
            _context = context;
        }


        public async Task<IActionResult> Get()
        {


            return new JsonResult( await _context.posts.ToListAsync());





        }



    }
}
