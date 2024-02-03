using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Socialty.Requests;
using Socialty.Services;


namespace Socialty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authservice;

        public AuthController(IAuthService authService)
        {
            _authservice = authService;


        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm ]LoginModel request)
        {

            if (!ModelState.IsValid)
            {

                return  BadRequest(ModelState);
            }

            if(await _authservice.Login(request))
            {


                return new JsonResult(new {
                token=_authservice.GenerateTokenString(request)});
            }

            return new JsonResult(new
            {
                error = "Invalid email or password"
            });



            


        }

        [HttpPost("register")]

        public async Task<IActionResult> Register([FromForm] LoginModel request)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }

            dynamic result = await _authservice.Register(request);


            if (result == "unique")
            {
                return new JsonResult(new
                {
                    error = "there is an already account with this email"
                }); ;

            }
            if (result=="true")
            {
                return new JsonResult(new
                {
                    register = "success"
                });
            }
            return new JsonResult(new
            {
                error = "problem while creating the account"
            });









        }


    }
}
