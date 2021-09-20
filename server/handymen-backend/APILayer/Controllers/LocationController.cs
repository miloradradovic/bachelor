using BusinessLogicLayer.services;
using handymen_backend.jwt;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model.models;

namespace handymen_backend.Controllers
{
    [ApiController]
    [Route("api/locations")]
    [EnableCors("CorsPolicy")]
    public class LocationController: ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet("get-location")]
        [Authorize(Roles.USER)]
        public IActionResult GetLocationByUser()
        {
            ApiResponse response = _locationService.GetByUser((User) HttpContext.Items["LoggedIn"]);
            return Ok(response);
        }
    }
}