using System;
using System.Collections.Generic;
using System.Text;
using TrackingApp.Core.Entites;

namespace TrackingApp.Core.Abstract.Services
{
    public interface IAuthorizationService
    {
         Task<string> GenerataToken(User user);
    }
}
