using System;
using System.Collections.Generic;
using System.Text;

namespace TrackingApp.Core.Dtos
{
    public class UserLoginDto
    {

        public string? UserNameOrEmail { get; set; }
        public string? Password { get; set; }

    }
}
