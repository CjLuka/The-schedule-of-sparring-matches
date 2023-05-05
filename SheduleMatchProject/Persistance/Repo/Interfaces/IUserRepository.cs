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
        Task<List<User>> GetAllAsync();
        Task <User> GetByIdAsync(int id);
        Task<User> GetByEmailAsync(string Email);

    }
}
