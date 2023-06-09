using Domain.Models.Domain;
using Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services.Interfaces
{
    public interface IUserServices
    {
        Task<string> GetEmailAsync(string email);
        Task<string> GetPasswordByEmailAsync(string email);
        Task<string> GetRoleByEmailAsync(string email);
        Task<int> GetUserIdByEmailAsync(string email);
        public void Login(string email);
        Task<ServiceResponse<User>> AddAsync(User user);
        Task<ServiceResponse<List<User>>> GetAllUsersAsync();
        Task<ServiceResponse<List<User>>> GetUsersWithoutClub();
    }
}
