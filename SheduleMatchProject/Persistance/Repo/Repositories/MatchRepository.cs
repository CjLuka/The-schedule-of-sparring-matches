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

        public async Task<List<Match>> GetAllAsync()
        {
            var Matches = await _context.Matches.ToListAsync();
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
            var AllMatches = await _context.Matches.ToListAsync();
            List<Match> matches = new List<Match>();
            foreach (var match in AllMatches)
            {
                if (match.ClubAway == clubId || match.ClubHome == clubId)
                {
                    matches.Add(match);
                    match.
                }
            }
            return matches;
        }
    }
}
