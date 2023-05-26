using Domain.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;
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
        private readonly ApplicationDbContext _context;
        public BranchClubRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BranchClub>> GetAllByClubAsync(int clubId)
        {
            var allBranchClubs = await _context.BranchesClubs.ToListAsync();
            List<BranchClub> branchClubsById = new List<BranchClub>();
            foreach (var branchClub in allBranchClubs)
            {
                if(branchClub.ClubId== clubId)
                {
                    branchClubsById.Add(branchClub);
                }
            }
            return branchClubsById;
        }
    }
}
