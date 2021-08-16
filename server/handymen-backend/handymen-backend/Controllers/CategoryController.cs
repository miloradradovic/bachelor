using BusinessLogicLayer.services;
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
    }
}