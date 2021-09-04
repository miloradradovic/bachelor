using BusinessLogicLayer.services;
using handymen_backend.jwt;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model.models;

namespace handymen_backend.Controllers
{
    [ApiController]
    [Route("api/categories")]
    [EnableCors("CorsPolicy")]
    public class CategoryController: ControllerBase
    {

        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        
        [HttpGet]
        public IActionResult GetCategories()
        {
            ApiResponse response = _categoryService.GetCategories();
            return Ok(response);
        }

        [HttpGet("get-categories-with-professions")]
        [Authorize(Roles.USER)]
        public IActionResult GetCategoriesWithProfessions()
        {
            ApiResponse response = _categoryService.GetCategoriesWithProfessions();
            return Ok(response);
        }
    }
}