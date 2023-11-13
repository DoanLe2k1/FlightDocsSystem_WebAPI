using FlightDocsSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocsSystem.Service
{
    public interface IUserService
    {
        Task<User> Register(string username, string password, string email, string phoneNumber);
        Task<IActionResult> Login(string username, string password);
        User GetUserById(int userId);
    }
}
