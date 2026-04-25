using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TrackingApp.Core.Abstract.Services;
using TrackingApp.Core.Dtos;
using TrackingApp.Core.Entites;
using TrackingApp.Core.HelperFunctions;
using TrackingApp.Core.Options;

namespace TrackingApp.Services.Services
{
    public class AuthorizationService:IAuthorizationService 
    {
        private readonly JwtSettingsOptions _jwtSettings;
        public AuthorizationService(IOptions<JwtSettingsOptions> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }


        public async Task<string>  GenerataToken(User user)
        {


            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),  
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.UserType.ToString())
            };


            var jwtToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(_jwtSettings.ExpiryInDay),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(HelperGeneral.Decrypt(_jwtSettings.SecretKey))), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }


    }
}
