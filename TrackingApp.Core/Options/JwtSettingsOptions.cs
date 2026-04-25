using System;
using System.Collections.Generic;
using System.Text;

namespace TrackingApp.Core.Options
{
  
    public class JwtSettingsOptions
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public bool ValidateIssuer { get; set; }
        public string? Audience { get; set; }
        public bool ValidateAudience { get; set; }

        public bool ValidateLifetime { get; set; }
        public int ExpiryInDay { get; set; }
    }
}
