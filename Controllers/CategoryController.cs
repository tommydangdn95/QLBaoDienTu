using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLBaoDienTu.Services;
using QLBaoDienTu.ViewModels._CategoryViewModels;
using System.Security.Claims;

namespace QLBaoDienTu.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
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
                return BadRequest();
            }
            
            return View();
        }
    }
}
