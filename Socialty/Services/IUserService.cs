using Microsoft.AspNetCore.Mvc;
using Socialty.Requests;

namespace Socialty.Services
{
    public interface IUserService
    {
        Task<IActionResult> GetInfo(String user_id);
        Task<IActionResult> ChangeProfile(ImageModel model, String user_id);
   
        Task<IActionResult> AddPost(ImageModel model, String user_id);
    }
}