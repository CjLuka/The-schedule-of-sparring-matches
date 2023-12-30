using Domain.Models.Domain;
using Domain.Models.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repo.Interfaces
{
    public interface IClubRepository
    {
        Task<List<Club>> GetAllAsync();
        Task<ListPaginated<Club>> GetAllAsync(ModelPagination modelPagination);
        Task<Club> GetByIdAsync(int clubId);
        Task<Club> GetByNameAsync(string name);
        Task AddAsync(Club club);
        Task DeleteAsync(Club club);
        Task UpdateAsync(Club club);
        Task<Club> GetClubByPresidentIdAsync(string userId);
    }
}
