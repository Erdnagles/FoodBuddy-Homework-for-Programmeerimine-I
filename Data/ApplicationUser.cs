using Microsoft.AspNetCore.Identity;

namespace FoodBuddy.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
