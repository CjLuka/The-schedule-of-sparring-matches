using Domain.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;
using Persistance.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repo.Repositories
{
    public class MatchRequestRepository : IMatchRequestRepository
    {
        private readonly ApplicationDbContext _context;
        public MatchRequestRepository(ApplicationDbContext context)
        {
            _context= context;
        }

        public async Task<MatchRequest> GetMatchRequestByIdAsync(int id)
        {
            var matchReq = await _context.MatchRequests.FirstOrDefaultAsync(x => x.Id == id);
            return matchReq;
        }

        public async Task<List<MatchRequest>> GetPlannedMatchByBranchAsync(BranchClub branchClub)
        {
            var allMatchRequest = await _context.MatchRequests.ToListAsync();//pobranie wszystkich zapytan o mecz
            List<MatchRequest> allMatchRequestByBranchClub = new List<MatchRequest>();//utworzenie listy, którą będziemy zwracać
            foreach (var item in allMatchRequest)
            {
                if (item.ReceiverId == branchClub.Id && item.SenderId == branchClub.Id && item.IsAccepted == true) 
                {
                    allMatchRequestByBranchClub.Add(item);//Dodawanie do listy tylko zapytan o mecz dla danego odzdziału w klubie i tych ktore sa zaakeptowane
                }
            }
            return allMatchRequestByBranchClub;
        }

        //Funkcja zwracająca wszystkie mecze danego klubu
        public async Task<List<MatchRequest>> GetPlannedMatchByClubAsync(Club club)
        {
            var matches = await _context.MatchRequests
                .Where(mr => mr.Sender.ClubId == club.Id || mr.Receiver.ClubId == club.Id)
                .ToListAsync();
            return matches;
        }

        //Funkcja zwracająca wszystkie mecze danego trenera
        public async Task<List<MatchRequest>> GetPlannedMatchByCoachAsync(string userId)
        {
            var allMatches = await _context.MatchRequests
                .Where(mr => mr.Sender.UserId == userId || mr.Receiver.UserId == userId)  // Sprawdzamy, czy użytkownik jest trenerem w Sender lub Receiver
                //.Where(mr => mr.IsAccepted)
                .Include(s => s.Sender.Club)
                .Include(r => r.Receiver.Club)
                .ToListAsync();

            return allMatches;
        }

        //Funkcja zwracająca zapytania o mecz otrzymane od innych klubów. Zwracane per trener
        public async Task<List<MatchRequest>> GetPropositionsByCoachAsync(string userId)
        {
            var matchPropositions = await _context.MatchRequests
                .Where(mr => mr.Receiver.UserId == userId)
                .Where(mr => !mr.IsAccepted.HasValue)
                .Include(c => c.Receiver.Club)
                .Include(c => c.Sender.Club)
                .ToListAsync();
            return matchPropositions;
        }

        //Zaplanuj nowy mecz
        public async Task PlanNewMatchAsync(MatchRequest matchRequest)
        {
            _context.MatchRequests.Add(matchRequest);
            _context.SaveChanges();
        }

        public async Task UpdateAsync(MatchRequest matchRequest)
        {
            _context.MatchRequests.Update(matchRequest);
            await _context.SaveChangesAsync();
        }
    }
}
