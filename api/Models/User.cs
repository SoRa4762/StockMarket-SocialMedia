using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Models
{

    public class User : IdentityUser
    {
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
        //? I don't need this?
        // public int Id { get; set; }
        // public string FirstName { get; set; } = string.Empty;
        // public string LastName { get; set; } = string.Empty;
        // public string Email { get; set; } = string.Empty;
        // public string Password { get; set; } = string.Empty;
        // public string ProfilePicture { get; set; } = string.Empty;
        // public string CoverPhoto { get; set; } = string.Empty;
    }
}