using Microsoft.AspNetCore.Identity;

namespace Web_Giam_Sat_Massan.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Role { get; set; } // Operator or Admin
    }
}
