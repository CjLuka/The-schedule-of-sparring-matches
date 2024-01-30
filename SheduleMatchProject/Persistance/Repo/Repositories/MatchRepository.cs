using Domain.Models.Domain;
using Domain.Models.Pagination;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;
using Persistance.Helpers;
using Persistance.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repo.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        private readonly ApplicationDbContext _context;
        public MatchRepository(ApplicationDbContext context)
        {
            _context= context;
        }

        //public async Task<List<Match>> GetAllAsync()
        //{
        //    var Matches = await _context.Matches.ToListAsync();
        //    return Matches;
        //}
        public async Task<List<Match>> GetAllAsync()
        {
            var Matches = await _context.Matches
                            .Include(m => m.BranchClubHome)//Nadanie dostępu do tabeli BranchClub, aby mozna bylo odowlywac sie do wartosci z tej tabeli                       
                            .ThenInclude(mb => mb.Club)//Nadanie wartości jeszcze dalej, do tabeli Club 
                            .Include(m => m.BranchClubAway)// Załaduj powiązaną drużynę wyjazdową 
                            .ThenInclude(mb => mb.Club)// Załaduj powiązaną drużynę wyjazdową 
                            .ToListAsync();
            return Matches;
        }


        public async Task<Match> GetByIdAsync(int matchId)
        {
            var match = await _context.Matches
                .Include(x => x.BranchClubHome.Club)
                .Include(x => x.BranchClubAway.Club)
                .Include(x => x.FootballPitch)
                .FirstOrDefaultAsync(x => x.Id == matchId);
            return match;
        }
        public async Task AddAsync(Match match)
        {
            await _context.AddAsync(match);
            await _context.SaveChangesAsync();
        }
        public Task DeleteAsync(Match match)
        {
            throw new NotImplementedException();
        }
        public async Task UpdateAsync(Match match)
        {
            _context.Update(match);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Match>> GetAllByClubAsync(int clubId)
        {
            var Matches = await _context.Matches
                            .Include(m=>m.BranchClubHome)//Nadanie dostępu do tabeli BranchClub, aby mozna bylo odowlywac sie do wartosci z tej tabeli                     
                            .ThenInclude(mb => mb.Club)//Nadanie wartości jeszcze dalej, do tabeli Club 
                            .Include(m => m.BranchClubAway)// Załaduj powiązaną drużynę wyjazdową 
                            .ThenInclude(mb => mb.Club)// Załaduj powiązaną drużynę wyjazdową 
                            .Include(x => x.FootballPitch)
                            .ToListAsync();

            List<Match> ListOfMatches = new List<Match>();//Lista meczow do ktorej będą dodawane mecze i ta, która będzie zwracana

            foreach (var match in Matches)
            {
                if (match.BranchClubHome.ClubId == clubId || match.BranchClubAway.ClubId == clubId)
                {
                    ListOfMatches.Add(match);
                }
            }
            return ListOfMatches;
        }

        public async Task<List<Match>> GetAllByBranchClubAsync(int branchClubId)
        {
            var matches = await _context.Matches
                .Include(x => x.BranchClubHome.Club)
                .Include(x => x.BranchClubAway.Club)
                .Include(x => x.FootballPitch)
                .Where(x => x.BranchClubHomeId == branchClubId|| x.BranchClubAwayId== branchClubId)
                .ToListAsync();

            return matches;
        }

        public async Task<ListPaginated<Match>> GetAllAsync(ModelPagination modelPagination)
        {
            var matches = await _context.Matches
                .Include(x => x.BranchClubHome.Club)
                .Include(x => x.BranchClubAway.Club)
                .Include(x => x.FootballPitch)
                .AddPagination(modelPagination);
            return matches;
        }
    }
}
