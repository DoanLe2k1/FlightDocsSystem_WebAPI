using FlightDocsSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocsSystem.Service
{
    public interface IUserService
    {
        Task<User> Register(string username, string password, string email, string phoneNumber);
        Task<IActionResult> Login(string username, string password);
        Task<bool> UpdateUser(int userId, UserDto userDto);
        User GetUserById(int userId);
    }
}
