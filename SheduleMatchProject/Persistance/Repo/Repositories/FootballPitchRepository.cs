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
    public class FootballPitchRepository : IFootballPitchRepository
    {
        private readonly ApplicationDbContext _context;
        public FootballPitchRepository(ApplicationDbContext context)
        {
            _context= context;
        }

        public async Task AddAsync(FootballPitch footballPitch)
        {
            await _context.AddAsync(footballPitch);
            await _context.SaveChangesAsync();
        }

        public async Task<List<FootballPitch>> GetAllAsync()
        {
            var allFootballPitches = await _context.FootballPitches
                .Include(x => x.Addresses)
                .ToListAsync();
            return allFootballPitches;
        }
        public async Task<ListPaginated<FootballPitch>> GetAllAsync(ModelPagination pagination)
        {
            var allFootballPitches = await _context.FootballPitches
                .Include(x => x.Addresses)
                .AddPagination(pagination);
            return allFootballPitches;
        }
        public async Task<List<FootballPitch>> GetAvailableFootballPitchesForMatchRequest(DateTime dateTime)
        {
            DateTime startTime = dateTime.AddHours(-2);
            DateTime endTime = dateTime.AddHours(2);

            // Lista boisk które są w podanym terminie zarezerwowane
            var reservedFootballPitchIds = await _context.MatchRequests
                .Where(mr => mr.Date > startTime && mr.Date < endTime)
                .Where(mr => mr.IsAccepted == true)
                .Where(mr => mr.FootballPitchId != null)
                .Select(mr => mr.FootballPitch.Id)
                .ToListAsync();

            // Lista dostępnych boisk (niezajętych 2 godziny przed meczem i 2 godziny po meczu)
            var availableFootballPitches = await _context.FootballPitches
                .Include(a => a.Addresses)
                .Where(fp => !reservedFootballPitchIds.Contains(fp.Id))
                .ToListAsync();

            return availableFootballPitches;
        }
    }

}
