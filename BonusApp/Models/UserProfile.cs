using System;
using System.Collections.Generic;
using System.Text;
using BonusApp.Models;

namespace BonusApp.Models
{
    public class UserProfile
    {
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

    }
}
