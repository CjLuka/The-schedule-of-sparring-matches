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
                            .Include(m => m.ClubHome) // Nadanie dostępu do tabeli Club, aby mozna bylo odowlywac sie do wartosci z tej tabeli
                            .Include(mecz => mecz.ClubAway) // Załaduj powiązaną drużynę wyjazdową
                            .ToListAsync();
            return Matches;
        }


        public Task<Match> GetByIdAsync(int matchId)
        {
            throw new NotImplementedException();
        }
        public Task AddAsync(Match match)
        {
            throw new NotImplementedException();
        }
        public Task DeleteAsync(Match match)
        {
            throw new NotImplementedException();
        }
        public Task UpdateAsync(Match match)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Match>> GetAllByClubAsync(int clubId)
        {
            var Matches = await _context.Matches
                            .Include(m => m.ClubHome) // Nadanie dostępu do tabeli Club, aby mozna bylo odowlywac sie do wartosci z tej tabeli                                                      //.ThenInclude(mc => mc.Name)
                            .Include(m => m.ClubAway) // Załaduj powiązaną drużynę wyjazdową                                                           //.ThenInclude(ClubAway => ClubAway.Name)
                            .ToListAsync();

            List<Match> matches = new List<Match>();//Lista meczow do ktorej będą dodawane mecze i ta, która będzie zwracana

            foreach (var match in Matches)
            {
                if (match.ClubAwayId == clubId || match.ClubHomeId == clubId)
                {
                    matches.Add(match);
                }
            }
            return matches;
        }
    }
}
