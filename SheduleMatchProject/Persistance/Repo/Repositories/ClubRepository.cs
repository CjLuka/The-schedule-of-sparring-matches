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
    public class ClubRepository : IClubRepository
    {
        private readonly ApplicationDbContext _context;
        public ClubRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Club>> GetAllAsync()
        {
            var Clubs = await _context.Clubs
                .Include(C => C.GameClass)
                .ToListAsync();
            return Clubs;
        }
        public async Task<Club> GetByIdAsync(int clubId)
        {
            return await _context.Clubs.FirstOrDefaultAsync(c => c.Id == clubId);
        }

        public async Task<Club> GetByNameAsync(string name)
        {
            return await _context.Clubs.FirstOrDefaultAsync(c => c.Name == name);
        }
        public async Task AddAsync(Club club)
        {
            await _context.Clubs.AddAsync(club);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Club club)
        {
            _context.Clubs.Remove(club);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Club club)
        {
            _context.Clubs.Update(club);
            await _context.SaveChangesAsync();
        }

        public async Task<Club> GetClubByPresidentIdAsync(Guid userId)
        {
            return await _context.Clubs.FirstOrDefaultAsync(c => c.UserId== userId);
        }
    }
}
