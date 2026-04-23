using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TrackingApp.Core.Dtos;
using TrackingApp.Core.Entites;
using TrackingApp.Core.Models;

namespace TrackingApp.Services.Profiles
{
    public class UserProfile :Profile
    {


        public UserProfile() 
        {
            CreateMap<User, SessionModel>()
                .ForMember(dest => dest.NameSurname, opt => opt.MapFrom(src => src.NameSurname))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => (int)src.UserType));


        }

    }
}
