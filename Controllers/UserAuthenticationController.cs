using FoodBuddy.Models;
using FoodBuddy.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FoodBuddy.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private IUserAuthenticationService authService;
        public UserAuthenticationController(IUserAuthenticationService authService)
        {
            this.authService = authService;
        }

/*        public async Task<IActionResult> Register()
        {
            var model = new RegistrationModel
            {
                Email = "admin@gmail.com",
                Username = "Admin",
                Name = "Andre",
                Password = "Admin!1",
                PasswordConfirm = "Admin!1",
                Role = "Admin",
            };

            var result = await authService.RegisterAsync(model);
            return Ok(result.Message);
        }*/

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            
            var result = await authService.LoginAsync(model);
            if(result.StatusCode == 1)
                return RedirectToAction("Index", "Home");
            else
            {
                TempData["msg"] = "Could not log in...";
                return RedirectToAction(nameof(Login));
            }
        }

        public async Task<IActionResult> Logout()
        {
            await authService.LogoutAsync();
                return RedirectToAction(nameof(Login));
        }
    }
}
