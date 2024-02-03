using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;
using Socialty.Contexts;
using Socialty.Models;
using Socialty.Requests;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Socialty.Services
{
    public class AuthService:IAuthService
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;

        private readonly AppDbContext _context;

        public AuthService(Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager,IConfiguration config,AppDbContext context)
        {
            _userManager = userManager;
            _config = config;
            _context = context;
        }

        public async Task<bool> Login(LoginModel request)
        {

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user != null)
            {

                return await _userManager.CheckPasswordAsync(user, request.Password);
            }


            return false;

        }



        public async Task<dynamic> Register(LoginModel request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user !=null )
            {
                return "unique";

            }
            var User = new ApplicationUser() {
                Email = request.Email,
                UserName = request.Email,
                Profile_Url= "Static/Profiles/guest-icon-png-6.jpg"
            };

            var result = await _userManager.CreateAsync(User, request.Password);

            if (result.Succeeded)
            {
                return "true";
            }
            return "false";




        }
         public async Task<String> GenerateTokenString(LoginModel request)
        {

            var user = _context.users.Where(item => item.Email == request.Email).FirstOrDefault();




            IEnumerable<Claim> claims = new List<Claim> {


                new Claim(ClaimTypes.Email,request.Email),
                new Claim(ClaimTypes.Role,"Admin"),
                new Claim(ClaimTypes.NameIdentifier,user.Id),


            };
            SecurityKey securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("jwt:key").Value));
            Microsoft.IdentityModel.Tokens.SigningCredentials signingCred = new SigningCredentials(securitykey,
                SecurityAlgorithms.HmacSha256Signature);
            var securityToken = new JwtSecurityToken(
                claims: claims,

                expires: DateTime.Now.AddMinutes(60),

                issuer: _config.GetSection("Jwt:Issuer").Value,
                audience: _config.GetSection("Jwt:Audience").Value,
                signingCredentials: signingCred

                );
            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return tokenString;




        }


    }
}
