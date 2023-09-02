using Microsoft.AspNetCore.Identity;

namespace ReadSavvy.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
