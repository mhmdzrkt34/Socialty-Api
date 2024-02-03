using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Socialty.Contexts;
using Socialty.Models;
using Socialty.Requests;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;

namespace Socialty.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserService(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;

        }



        public async Task<IActionResult> GetInfo(String user_id)
        {
            var user = await _context.users.FindAsync(user_id);

            return new JsonResult(user);




        }

        public async Task<IActionResult> ChangeProfile(ImageModel model, String user_id)
        {
            var user = await _context.users.FindAsync(user_id);
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Image.FileName);
            var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Static/profiles", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.Image.CopyToAsync(stream);
            }

            String path = "Static/profiles/" + fileName;
            user.Profile_Url = path;
            await _context.SaveChangesAsync();


            return new JsonResult(
                
                new {
                profile_path=path});






        }



        public async Task<IActionResult> AddPost(ImageModel model, String user_id)
        {
            var user = await _context.users.FindAsync(user_id);
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Image.FileName);
            var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Static/posts", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.Image.CopyToAsync(stream);
            }
            String path = "Static/posts/" + fileName;

            Post post = new Post()
            {
                Post_Url = path,
                User = user
            };

            await _context.posts.AddAsync(post
              );

            await _context.SaveChangesAsync();

            var posts = await _context.posts.ToListAsync();

            return new JsonResult(new
            {

                userposts=user.Posts,
                posts=posts
                


            }) ;






        }




    }
}
