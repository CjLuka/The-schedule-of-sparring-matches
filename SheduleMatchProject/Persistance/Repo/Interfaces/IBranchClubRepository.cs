using Domain.Models.Domain;
using Domain.Models.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repo.Interfaces
{
    public interface IBranchClubRepository
    {
        Task<List<BranchClub>> GetAllByClubAsync(int clubId);
        Task <BranchClub> GetClubBranchByCoach(string userId);
        Task<List<BranchClub>> GetAllBranchClubAsync();
        Task<ListPaginated<BranchClub>> GetAllBranchClubAsync(ModelPagination pagination);
        Task<List<BranchClub>> GetAllBranchClubsForPlanMatch(int clubId);
        Task<BranchClub> GetDetailBranchByIdAsync(int branchClubId);
        Task<BranchClub> GetBranchByIdAsync(int branchClubId);
        Task AddAsync(BranchClub branchClub);
        Task UpdateAsync(BranchClub branchClub);
        Task DeleteAsync(BranchClub branchClub);
        Task<int> CountBranchesForCoach(string userId);
        Task<List<BranchClub>> GetAllBranchesForCoach(string userId);

        //Task<BranchClub> GetByIdAsync(int branchId);
        //Task AddAsync(BranchClub branchClub);
        //Task DeleteAsync(BranchClub branchClub);
        //Task UpdateAsync(BranchClub branchClub);
    }
}
