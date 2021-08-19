using BusinessLogicLayer.services;
using handymen_backend.jwt;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model.dto;
using Model.models;

namespace handymen_backend.Controllers
{
    [ApiController]
    [Route("api/administrators")]
    [EnableCors("CorsPolicy")]
    public class AdministratorController : ControllerBase
    {

        private readonly IPersonService _personService;

        public AdministratorController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPost("create-new-administrator")]
        [Authorize(Roles.ADMINISTRATOR)]
        public IActionResult CreateAdministrator([FromBody] AdministratorDTO administratorDto)
        {
            ApiResponse response = _personService.CreateAdministrator(administratorDto.ToAdministratorWithoutId());

            if (response.Status == 400)
            {
                return BadRequest(response);
            }

            return Created(response.Message, response);
        }

    }
}