using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QLBaoDienTu.Dtos;
using QLBaoDienTu.Dtos.Apis;
using QLBaoDienTu.Services;
using QLBaoDienTu.ViewModels._CategoryViewModels;
using System.Security.Claims;

namespace QLBaoDienTu.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;
        public CategoryController(ICategoryService categoryService, IUserService userService)
        {
            this._categoryService = categoryService;
            this._userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategory createCategory)
        {
            if (createCategory == null)
            {
                return BadRequest(new ApiErrorResponse<string>
                        (
                            "Request for creating new category is not found",
                             StatusCodes.Status400BadRequest
                        ));
            }

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

            var result = await _categoryService.CreateCategory(createCategory, userId);
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

        [HttpPut("id")]
        public async Task<IActionResult> UpdateAsync([FromRoute] string id, [FromBody] UpdateCategory updateCategory)
        {
            if (updateCategory == null)
            {
                return BadRequest(new ApiErrorResponse<string>
                        (
                            "Request for creating new category is not found",
                             StatusCodes.Status400BadRequest
                        ));
            }

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

            updateCategory.Id = Guid.Parse(id);
            var result = await _categoryService.UpdateCategory(updateCategory, userId);
            if (!result.IsSuccess)
            {
                return BadRequest(new ApiErrorResponse<string>
                        (
                             result.Message,
                             StatusCodes.Status400BadRequest
                        ));
            }

            return StatusCode(StatusCodes.Status200OK, new ApiCommandResponse(result.Message, StatusCodes.Status200OK));
        }

    }
}
