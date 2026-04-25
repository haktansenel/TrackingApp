using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackingApp.API.Controllers;

namespace TrackingApp.API.Menu
{
    [Authorize]
    [Route("api/[controller]")]

    public class MenuController : BaseController
    {

        [HttpGet]
        public IActionResult Index()
        {


            return Ok(new
            {
                Message = "Welcome to the Menu API!"
            });
        }
    }
}
