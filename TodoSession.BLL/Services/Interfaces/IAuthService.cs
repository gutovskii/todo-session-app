using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoSession.BLL.DTO.Auth;
using TodoSession.DataAccess.Models;

namespace TodoSession.BLL.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<User> Login(AuthUserDto dto);
        public Task<User> Register(AuthUserDto dto);
    }
}
