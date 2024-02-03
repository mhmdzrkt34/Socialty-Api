using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Socialty.Requests;
using Socialty.Services;
using System.Security.Claims;

namespace Socialty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;

        }


        [HttpGet("info")]
        public async Task<IActionResult> Info()
        {
            var userId=User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;

            return await _userService.GetInfo(userId);


            


        }

        [HttpPost("changeprofile")]
        public async Task<IActionResult> ChangeProfile([FromForm] ImageModel imageModel)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            if (!ModelState.IsValid)
            {

                return new JsonResult(ModelState);
            }


            var result = await _userService.ChangeProfile(imageModel, userId);
            return result;


        }


        [HttpPost("addpost")]
        public async Task<IActionResult> AddPost([FromForm] ImageModel imageModel) {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            return await _userService.AddPost(imageModel, userId);



        }



    }
}
