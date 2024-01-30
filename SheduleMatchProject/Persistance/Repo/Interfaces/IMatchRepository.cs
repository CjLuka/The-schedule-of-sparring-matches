using Domain.Models.Domain;
using Domain.Models.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repo.Interfaces
{
    public interface IMatchRepository
    {
        Task<List<Match>> GetAllAsync();
        Task<ListPaginated<Match>> GetAllAsync(ModelPagination modelPagination);
        Task<List<Match>> GetAllByClubAsync(int clubId);
        Task<List<Match>> GetAllByBranchClubAsync(int branchClubId);
        Task<Match> GetByIdAsync(int matchId);
        Task AddAsync(Match match);
        Task DeleteAsync(Match match);
        Task UpdateAsync(Match match);
    }
}
