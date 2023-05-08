using Domain.Models.Domain;
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
        Task <User> GetByIdAsync(int id);
        Task<User> GetByEmailAsync(string Email);
        Task<string> GetPasswordByEmailAsync(string Email);
        Task<string> GetEmailAsync(string Email);
        Task<string> GetRoleByEmailAsync(string Email);
    }
}
