using E_Commerce_App.Models.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace E_Commerce_App.Models.Interface
{
    public interface IUser
    {
        public Task<UserDTO> Register(RegisterDTO data, ModelStateDictionary modelState);

        public Task<UserDTO> Authenticate(string username, string password);

        public Task<UserDTO> GetUser(ClaimsPrincipal user);
    }
}
