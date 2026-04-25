using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using TrackingApp.Core.Abstract.Repositories;
using TrackingApp.Core.Abstract.Services;
using TrackingApp.Core.Dtos;
using TrackingApp.Core.HelperFunctions;
using TrackingApp.Core.Models;
using TrackingApp.Core.Options;
using TrackingApp.Repository.Abstract.Repositories;
using TrackingApp.Repository.Abstract.Services;
using Microsoft.AspNetCore.Http.Abstractions;

namespace TrackingApp.Services.Services
{
    public class LoginService : ILoginService
    {
       private readonly IUserRepository _userRepository;

        private readonly IMapper _mapper;

        private readonly IAuthorizationService _authorizationService;

        private readonly JwtSettingsOptions _jwtSettings;

        private readonly IHttpContextAccessor _httpContextAccessor;
        public LoginService(IUserRepository userRepository, IMapper mapper, IOptions<JwtSettingsOptions> jwtSettings, IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtSettings = jwtSettings.Value;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
        }
        public async Task<BaseResponseDto<UserLoginResponseDto>> Login(UserLoginDto userLoginDto)
        {

            BaseResponseDto<UserLoginResponseDto> baseResponseDto = new BaseResponseDto<UserLoginResponseDto>();

            #region Business
            
            
            if (string.IsNullOrWhiteSpace(userLoginDto.UserNameOrEmail))
                baseResponseDto.Errors.Add("Username or email is required.");
            //baseResponseDto.StatusCode = HttpStatusCode.BadRequest;

            if (string.IsNullOrWhiteSpace(userLoginDto.Password))
                baseResponseDto.Errors.Add("Password is required");


            if (!string.IsNullOrWhiteSpace(userLoginDto.UserNameOrEmail) && userLoginDto.UserNameOrEmail.ToString().Contains(" "))
                baseResponseDto.Errors.Add("Username or email contains spaces.");
          
            if (!baseResponseDto.IsSuccess)
                return baseResponseDto;
            #endregion

            var user = (await _userRepository.GetUserByUserNameOrEmail(userLoginDto.UserNameOrEmail));

           

            string decryptedPassword = HelperGeneral.Decrypt(user.EncryptedPassword);    

            if (user == null || decryptedPassword != userLoginDto.Password)
                baseResponseDto.Errors.Add("Invalid username/email or password.");

             if (!baseResponseDto.IsSuccess)
                return baseResponseDto;
            


           string token = await  _authorizationService.GenerataToken(user);


            if (string.IsNullOrWhiteSpace(token))
              baseResponseDto.Errors.Add("Failed to generate authentication token.");   


            if (!baseResponseDto.IsSuccess)
                return baseResponseDto;

            baseResponseDto.Data = new UserLoginResponseDto
            {
                Token = token,
                UserName = user.UserName,
                ExpireDate = DateTime.UtcNow.AddDays(_jwtSettings.ExpiryInDay),
            };

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = baseResponseDto.Data.ExpireDate
            };

            baseResponseDto.StatusCode = HttpStatusCode.Accepted;
            baseResponseDto.Message = "Login successful.";
            _httpContextAccessor.HttpContext?.Response.Cookies.Append(HelperGeneral.GetCookieName(), token, cookieOptions);

            return baseResponseDto;
        }
    }
}
