using System;
using System.Collections.Generic;
using System.Text;
using TrackingApp.Core.Dtos;
using TrackingApp.Core.Models;

namespace TrackingApp.Repository.Abstract.Services
{
    public  interface ILoginService
    {
         Task<BaseResponseDto<NoContentDto>>  Login(UserLoginDto userLoginDto);
   }
}
