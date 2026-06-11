using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLBaoDienTu.Dtos.Apis;
using QLBaoDienTu.Dtos;
using QLBaoDienTu.ViewModels._NewsViewModels;
using QLBaoDienTu.ViewModels._CategoryViewModels;
using QLBaoDienTu.Services;
using System.Security.Claims;

namespace QLBaoDienTu.Controllers
{
    [Authorize]
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        public NewsController(INewsService newsService)
        {
            this._newsService = newsService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateNews(CreateNews createNews)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (Guid.TryParse(userIdString, out Guid userId))
            {
                return Unauthorized(new ApiErrorResponse<string>
                (
                    "User not authenticated",
                    StatusCodes.Status401Unauthorized)
                {

                });
            }

            var result = await _newsService.CreateNews(createNews, userId);
            if (!result.IsSuccess)
            {
                return BadRequest(new ApiErrorResponse<string>
                        (
                             result.Message,
                             StatusCodes.Status400BadRequest
                        ));
            }

            return StatusCode(StatusCodes.Status201Created, new ApiCommandResponse(result.Message, StatusCodes.Status201Created));
        }
    }
}
