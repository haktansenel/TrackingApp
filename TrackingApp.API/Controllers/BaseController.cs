using Microsoft.AspNetCore.Mvc;
using System.Net;
using TrackingApp.Core.Dtos;
using TrackingApp.Core.Models;
namespace TrackingApp.API.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        public async Task<IActionResult> SendResponse<T>(BaseResponseDto<T> baseResponseDto)
        {


            if (baseResponseDto.StatusCode == null || baseResponseDto.StatusCode==0) 
            {
                if (baseResponseDto.IsSuccess)
                    baseResponseDto.StatusCode = HttpStatusCode.OK;

                else 
                    baseResponseDto.StatusCode = HttpStatusCode.BadRequest;
            }


            return new ObjectResult(baseResponseDto)
            {
                StatusCode= (int)baseResponseDto.StatusCode,    
                Value = baseResponseDto
            };
        }
    }
}
