using System;
using System.Collections.Generic;
using System.Text;
using TrackingApp.Core.Dtos;
using TrackingApp.Core.Entites;

namespace TrackingApp.Core.Abstract.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByUserNameOrEmail(string emailOrUsername);
    }
}
