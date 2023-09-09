using E_Commerce_App.Models.DTO;
using E_Commerce_App.Models.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace E_Commerce_App.Models.Service
{
    public class IdentityUserService : IUserService
    {
        private UserManager<AuthUser> userManager;
        private SignInManager<AuthUser> signInManager;

        public IdentityUserService(UserManager<AuthUser> manager, SignInManager<AuthUser> sim)
        {
            userManager = manager;
            signInManager = sim;
        }

        public async Task<UserDTO> Register(RegisterDTO data, ModelStateDictionary modelState)
        {
            var user = new AuthUser
            {
                UserName = data.Username,
                Email = data.Email,
                PhoneNumber = data.PhoneNumber
            };

            var result = await userManager.CreateAsync(user, data.Password);

            if (result.Succeeded)
            {
                // Because we have a "Good" user, let's add them to their proper role
                await userManager.AddToRolesAsync(user, data.Roles);
                return new UserDTO
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Roles = await userManager.GetRolesAsync(user)
                };
            }

            // What about our errors?
            // We can feed modelState with our errors, and then use a tag helper to show them:
            // <div asp-validation-summary="All"></div>

            foreach (var error in result.Errors)
            {
                var errorKey =
                    error.Code.Contains("Password") ? nameof(data.Password) :
                    error.Code.Contains("Email") ? nameof(data.Email) :
                    error.Code.Contains("UserName") ? nameof(data.Username) :
                    "";
                modelState.AddModelError(errorKey, error.Description);
            }
            return null;
        }

        private TimeSpan TimeSpan(object p)
        {
            throw new NotImplementedException();
        }


        public async Task<UserDTO> Authenticate(string username, string password)
        {

            var result = await signInManager.PasswordSignInAsync(username, password, true, false);

            if (result.Succeeded)
            {
                var user = await userManager.FindByNameAsync(username);
                return new UserDTO
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Roles = await userManager.GetRolesAsync(user)
                };
            }

            return null;
        }

        // Use a "claim" to get a user
        public async Task<UserDTO> GetUser(ClaimsPrincipal principal)
        {
            var user = await userManager.GetUserAsync(principal);
            return new UserDTO
            {
                Id = user.Id,
                Username = user.UserName,
                Roles = await userManager.GetRolesAsync(user)
            };
        }
    }
}
