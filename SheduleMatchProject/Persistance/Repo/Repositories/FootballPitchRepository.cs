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
    public class FootballPitchRepository : IFootballPitchRepository
    {
        private readonly ApplicationDbContext _context;
        public FootballPitchRepository(ApplicationDbContext context)
        {
            _context= context;
        }
        public async Task<List<FootballPitch>> GetAllAsync()
        {
            var allFootballPitches = await _context.FootballPitches.ToListAsync();
            return allFootballPitches;
        }
        public async Task<List<FootballPitch>> GetAvailableFootballPitchesForMatchRequest(DateTime dateTime)
        {
            var reservedFootballPitchIds = await _context.MatchRequests
                .Where(mr => mr.Date == dateTime && mr.IsAccepted)
                .Select(mr => mr.FootballPitch.Id)
                .ToListAsync();

            var availableFootballPitches = await _context.FootballPitches
                .Where(fp => !reservedFootballPitchIds.Contains(fp.Id))
                .ToListAsync();

            return availableFootballPitches;
        }
    }

}
