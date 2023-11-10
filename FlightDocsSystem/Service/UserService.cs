using FlightDocsSystem.Data;
using FlightDocsSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace FlightDocsSystem.Service
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly FlightDocsSystemWebAPIDbContext _dbContext;
        public UserService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, FlightDocsSystemWebAPIDbContext dbContext)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;

        }
        public User GetUserById(int userId)
        {
            return _dbContext.Users.FirstOrDefault(u => u.UserId == userId);
        }
        public async Task<User> Register(string username, string password, string email, string phoneNumber)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phoneNumber))
            {
                throw new Exception("Thiếu thông tin");
            }

            var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username || u.Email == email);
            if (existingUser != null)
            {
                throw new Exception("Tài khoản này đã được đăng ký.");
            }

            // Tạo hash mật khẩu
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            // Tạo đối tượng User mới
            var newUser = new User
            {
                Username = username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Email = email,
                PhoneNumber = phoneNumber,
            };
            // Lưu người dùng vào cơ sở dữ liệu
            await _dbContext.Users.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();
            return newUser;
        }

        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new Exception("Mật khẩu không đúng.");
            }

            if (string.IsNullOrEmpty(username))
            {
                throw new Exception("Tên đăng nhập hoặc Email không tồn tại.");
            }

            // Tìm kiếm người dùng trong cơ sở dữ liệu
            var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (existingUser == null)
            {
                throw new ArgumentException("Invalid username or password.");
            }

            // Kiểm tra mật khẩu
            if (!VerifyPasswordHash(password, existingUser.PasswordHash, existingUser.PasswordSalt))
            {
                throw new ArgumentException("Invalid username or password.");
            }

            string token = CreateToken(existingUser);
            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken, existingUser);
            return new ObjectResult(token) { StatusCode = 200 };
        }
        public async Task<bool> UpdateUser(int userId, UserDto userDto)
        {
            // Tìm kiếm người dùng cần cập nhật
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                throw new Exception("Người dùng không tồn tại");
            }

            // Cập nhật thông tin người dùng từ UserDto
            user.Username = userDto.Username;
            user.Email = userDto.Email;
            user.PhoneNumber = userDto.PhoneNumber;

            // Lưu thay đổi vào cơ sở dữ liệu
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }


        // Khu vực xử lý token 
        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        private void SetRefreshToken(RefreshToken newRefreshToken, User user)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            _httpContextAccessor.HttpContext.Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
