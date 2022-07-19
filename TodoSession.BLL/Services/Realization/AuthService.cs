using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoSession.BLL.DTO.Auth;
using TodoSession.BLL.Services.Interfaces;
using TodoSession.DataAccess;
using TodoSession.DataAccess.Models;
using BCryptNet = BCrypt.Net.BCrypt;

namespace TodoSession.BLL.Services.Realization
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _db;
        public AuthService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<User> Login(AuthUserDto dto)
        {
            var user = await _db.Users.SingleOrDefaultAsync(u => u.UserName == dto.UserName);
            if (user == null) return null;

            if (!BCryptNet.Verify(dto.Password, user.PasswordHash)) return null;
            
            return user;
        }

        public async Task<User> Register(AuthUserDto dto)
        {
            var user = await _db.Users.SingleOrDefaultAsync(u => u.UserName == dto.UserName);
            if (user != null) return null;

            var passwordHash = BCryptNet.HashPassword(dto.Password);
            var newUser = new User
            {
                UserName = dto.UserName,
                PasswordHash = passwordHash
            };
            await _db.Users.AddAsync(newUser);
            await _db.SaveChangesAsync();
            return newUser;
        }
    }
}
