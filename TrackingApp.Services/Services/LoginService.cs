using AutoMapper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using TrackingApp.Core.Abstract.Repositories;
using TrackingApp.Core.Dtos;
using TrackingApp.Core.HelperFunctions;
using TrackingApp.Core.Models;
using TrackingApp.Repository.Abstract.Repositories;
using TrackingApp.Repository.Abstract.Services;

namespace TrackingApp.Services.Services
{
    public class LoginService : ILoginService
    {
       private readonly IUserRepository _userRepository;

        private readonly IMapper _mapper;


        public LoginService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<BaseResponseDto<NoContentDto>> Login(UserLoginDto userLoginDto)
        {

            BaseResponseDto<NoContentDto> baseResponseDto = new BaseResponseDto<NoContentDto>();

            #region Business
            
            
            if (string.IsNullOrWhiteSpace(userLoginDto.UserNameOrEmail))
                baseResponseDto.Errors.Add("Username or email is required.");
            //baseResponseDto.StatusCode = HttpStatusCode.BadRequest;

            if (string.IsNullOrWhiteSpace(userLoginDto.Password))
                baseResponseDto.Errors.Add("Password is required");


            if (string.IsNullOrWhiteSpace(userLoginDto.UserNameOrEmail) || userLoginDto.UserNameOrEmail.ToString().Contains(" "))
                baseResponseDto.Errors.Add("Username or email contains spaces.");


            //if (string.IsNullOrWhiteSpace(userLoginDto.UserNameOrEmail) || !HelperGeneral.IsSpecialCharsContains(userLoginDto.UserNameOrEmail))
            //    baseResponseDto.Errors.Add("Username or email must contain at least one special character.");

            if (!baseResponseDto.IsSuccess)
                return baseResponseDto;
            #endregion


            var user = (await _userRepository.GetUserByUserName(userLoginDto.UserNameOrEmail));


            string decryptedPassword = HelperGeneral.Decrypt(user.EncryptedPassword);    


            if (decryptedPassword != userLoginDto.Password || user == null)
                baseResponseDto.Errors.Add("Invalid username/email or password.");


             if (!baseResponseDto.IsSuccess)
                return baseResponseDto;
            


            return baseResponseDto;

        }
    }
}
