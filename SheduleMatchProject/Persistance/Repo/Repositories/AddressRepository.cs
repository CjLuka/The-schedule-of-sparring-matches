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
    public class AddressRepository : IAdressRepository
    {
        private readonly ApplicationDbContext _context;
        public AddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Addresses addresses)
        {
            await _context.Addresses.AddAsync(addresses);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Addresses addresses)
        {
            _context.Addresses.Remove(addresses);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Addresses>> GetAllAsync()
        {
            return await _context.Addresses.ToListAsync();
        }

        public async Task<Addresses> GetByIdAsync(int id)
        {
            return await _context.Addresses.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Addresses addresses)
        {
            _context.Addresses.Update(addresses);
            await _context.SaveChangesAsync();
        }
    }
}
