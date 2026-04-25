using System;
using System.Collections.Generic;
using System.Text;

namespace TrackingApp.Core.Dtos
{
    public class UserLoginResponseDto
    {

        public string? Token { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string UserName { get; set; }
    }
}
