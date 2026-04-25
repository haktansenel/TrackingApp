using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TrackingApp.Core.Abstract.Repositories;
using TrackingApp.Core.Dtos;
using TrackingApp.Core.Entites;
using TrackingApp.Repository.Abstract.Repositories;

namespace TrackingApp.Repository.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppContext context) : base(context)
        {
        }


        public async Task<User> GetUserByUserNameOrEmail(string emailOrUsername)
        {
            var user = await _context.Users
         .Where(u => (u.Email == emailOrUsername || u.UserName == emailOrUsername))
         .Select(u => new User
         {
             Id = u.Id,
             UserType = u.UserType,
             Email = u.Email,
             UserName = u.UserName,
            EncryptedPassword=u.EncryptedPassword

         })
         .FirstOrDefaultAsync();

            return user;
        }
    }
}
