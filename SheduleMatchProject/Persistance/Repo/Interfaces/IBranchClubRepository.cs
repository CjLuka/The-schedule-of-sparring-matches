using Domain.Models.Domain;
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
        //Task<BranchClub> GetByIdAsync(int branchId);
        //Task AddAsync(BranchClub branchClub);
        //Task DeleteAsync(BranchClub branchClub);
        //Task UpdateAsync(BranchClub branchClub);
    }
}
