//----------------------------------------------------------------------------------------------------------------------------------------

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

        // Constructor to initialize UserManager and SignInManager
        public IdentityUserService(UserManager<AuthUser> manager, SignInManager<AuthUser> sim)
        {
            userManager = manager;
            signInManager = sim;
        }

        /// <summary>
        /// Registers a new user based on provided registration data.
        /// </summary>
        /// <param name="data">Registration data including username, email, password, and roles.</param>
        /// <param name="modelState">ModelState for validation errors.</param>
        /// <returns>A UserDTO if registration is successful, otherwise null.</returns>
        public async Task<UserDTO> Register(RegisterDTO data, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                return null; 
            }

            var user = new AuthUser
            {
                UserName = data.Username,
                Email = data.Email,
                PhoneNumber = data.PhoneNumber
            };

            var result = await userManager.CreateAsync(user, data.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRolesAsync(user, data.Roles);
                return new UserDTO
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Roles = await userManager.GetRolesAsync(user)
                };
            }

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



        /// <summary>
        /// Authenticates a user with the provided username and password.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>A UserDTO if authentication is successful, otherwise null.</returns>
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

        /// <summary>
        /// Retrieves user information based on claims in the provided ClaimsPrincipal.
        /// </summary>
        /// <param name="principal">The ClaimsPrincipal associated with the authenticated user.</param>
        /// <returns>A UserDTO representing the authenticated user.</returns>
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