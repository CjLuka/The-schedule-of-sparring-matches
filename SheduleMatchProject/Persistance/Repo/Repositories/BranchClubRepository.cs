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

        public async Task<List<BranchClub>> GetAllBranchClubAsync()
        {
            var allBranchClubs = await _context.BranchesClubs
                .Include(c => c.Club)
                .ToListAsync();
            return allBranchClubs;
        }

        public async Task<List<BranchClub>> GetAllBranchClubsForPlanMatch(int clubId)
        {
            var allBranchClubs = await _context.BranchesClubs
                .Include(c => c.Club)
                .ToListAsync();
            List<BranchClub> branchClubsWithoutSender= new List<BranchClub>();
            foreach (var branchClub in allBranchClubs)
            {
                if (branchClub.ClubId != clubId)
                {
                    branchClubsWithoutSender.Add(branchClub);//dodawanie do listy tych zespołow, ktore nie będą wysyłały zapytania o mecz
                }
            }
            return branchClubsWithoutSender;
        }

        public async Task<List<BranchClub>> GetAllByClubAsync(int clubId)
        {
            var allBranchClubs = await _context.BranchesClubs
                .Include(c => c.Club)
                .ToListAsync();
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

        public async Task<BranchClub> GetClubBranchByCoach(int userId)//funkcja pobierająca klub dla danego trenera
        {
            var myBranchClub = await _context.BranchesClubs
                .Include(c => c.Club)
                .FirstOrDefaultAsync(u => u.UserId == userId);
            return myBranchClub;
        }
    }
}
