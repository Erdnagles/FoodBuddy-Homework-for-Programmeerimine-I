using FoodBuddy.Data;
using FoodBuddy.Models;

namespace FoodBuddy.Services
{
    public interface IUserAuthenticationService
    {
        Task<Status> LoginAsync(LoginModel model);
        Task LogoutAsync();
        Task<Status> RegisterAsync(RegistrationModel model);
    }
}
