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
            var Clubs = await _context.Clubs.ToListAsync();
            Console.WriteLine(Clubs);
            return Clubs;
        }
        public Task AddAsync(Club club)
        {
            throw new NotImplementedException();
        }
    }
}
