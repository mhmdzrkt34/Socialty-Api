using Microsoft.AspNetCore.Identity.Data;
using Socialty.Requests;

namespace Socialty.Services
{
    public interface IAuthService
    {

        Task<bool> Login(LoginModel request);
        Task<dynamic> Register(LoginModel request);
        Task<String> GenerateTokenString(LoginModel request);
    }
}
