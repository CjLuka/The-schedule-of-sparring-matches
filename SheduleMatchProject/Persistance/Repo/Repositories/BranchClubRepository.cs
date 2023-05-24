using Domain.Models.Domain;
using Persistance.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repo.Repositories
{
    public class BranchClubRepository : IBranchClubRepository
    {
        public Task<List<BranchClub>> GetAllByClubAsync(int clubId)
        {
            throw new NotImplementedException();
        }
    }
}
