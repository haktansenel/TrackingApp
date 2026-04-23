using System;
using System.Collections.Generic;
using System.Text;
using TrackingApp.Core.Enums;

namespace TrackingApp.Core.Models
{
    public class SessionModel
    {

        public int Id { get; set; } 

        public string NameSurname { get; set; }

        public string Email { get; set; }
        public UserType UserType { get; set; }

    }
}
