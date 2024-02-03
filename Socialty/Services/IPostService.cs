using Microsoft.AspNetCore.Mvc;

namespace Socialty.Services
{
    public interface IPostService
    {
        Task<IActionResult> Get();
    }
}