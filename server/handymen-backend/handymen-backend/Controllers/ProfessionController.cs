using BusinessLogicLayer.services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model.models;

namespace handymen_backend.Controllers
{
    
    [ApiController]
    [Route("api/professions")]
    [EnableCors("CorsPolicy")]
    public class ProfessionController: ControllerBase
    {

        private readonly IProfessionService _professionService;

        public ProfessionController(IProfessionService professionService)
        {
            _professionService = professionService;
        }

        [HttpGet("get-professions-by-category/{categoryId}")]
        public IActionResult GetProfessionsByCategory([FromRoute] int categoryId)
        {
            ApiResponse response = _professionService.GetProfessionsByCategory(categoryId);
            
            if (response.Status == 400)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

    }
}