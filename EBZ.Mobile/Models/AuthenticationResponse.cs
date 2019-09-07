using System;
using System.Collections.Generic;

namespace EBZ.Mobile.Models
{
    public class AuthenticationResponse
    {
        public bool IsAuthenticated { get; set; }
        public User User { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public DateTime ValidTo { get; set; }
        public string Role { get; set; }
    }
}
