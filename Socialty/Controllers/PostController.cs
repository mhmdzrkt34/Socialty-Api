using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Socialty.Services;

namespace Socialty.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;



        }

        [HttpGet]

        public async Task<IActionResult> Get() {

            return await _postService.Get();
                
                
                }
    }
}
