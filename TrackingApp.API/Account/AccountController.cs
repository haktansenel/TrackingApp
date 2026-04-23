using Microsoft.AspNetCore.Mvc;
using TrackingApp.API.Controllers;
using TrackingApp.Core.Dtos;
using TrackingApp.Repository.Abstract.Services;

namespace TrackingApp.API.Account
{
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        public ILoginService _loginService { get; set; }
        public AccountController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]UserLoginDto userLoginDto)
        {
            var response = await _loginService.Login(userLoginDto);
            return await SendResponse(response);
        }
    }
}
