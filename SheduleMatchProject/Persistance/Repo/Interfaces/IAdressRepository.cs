using Domain.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repo.Interfaces
{
    public interface IAdressRepository
    {
        Task<List<Addresses>> GetAllAsync();
        Task AddAsync(Addresses addresses);
        Task UpdateAsync(Addresses addresses);
        Task DeleteAsync(Addresses addresses);
        Task<Addresses> GetByIdAsync(int id);
    }
}
