using Domain.Models.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repo.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<List<User>> GetAllAsync();
        Task<List<User>> GetAllUsersAsync();
        Task <User> GetByIdAsync(string id);
        Task<User> GetByEmailAsync(string Email);
        Task<List<User>> GetCoaches();
        Task<List<User>> GetAllCoaches();
        Task<string> GetPasswordByEmailAsync(string Email);
        Task<string> GetEmailAsync(string Email);
        Task<string> GetRoleByEmailAsync(string Email);
        Task<string> GetUserIdByEmailAsync(string Email);
        Task<IdentityUser> GetUserByIdAsync(string id);
    }
}
