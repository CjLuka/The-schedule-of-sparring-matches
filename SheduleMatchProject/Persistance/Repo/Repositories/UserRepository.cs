using Domain.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Persistance.Data;
using Persistance.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repo.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public UserRepository(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context= context;
            _userManager= userManager;
        }
        public async Task<List<User>> GetAllAsync()
        {
            List<User> allUsers = await _userManager.Users.ToListAsync();
            //var Users = await _context.Users.ToListAsync();
            return allUsers;
        }

        public async Task<User> GetByEmailAsync(string Email)
        {
            return await _context.Users.FirstOrDefaultAsync(c => c.Email == Email);
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<IdentityUser> GetUserByIdAsync(string id)
        {
            IdentityUser user = await _userManager.FindByIdAsync(id);
            return user;
        }
        public async Task<List<User>> GetAllUsersAsync()
        {
            List<User> allUsers = await _userManager.Users.ToListAsync();
            return allUsers;
        }

        //public async Task<string> GetPasswordByEmailAsync(string Email)
        //{
        //    var user = await _context.Users.FirstOrDefaultAsync(c => c.Email == Email);
        //    if (user != null)
        //    {
        //        return user.Password.ToString();
        //    }
        //    return null;
        //}
        public async Task<string> GetEmailAsync(string Email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(c => c.Email == Email);
            if (user != null){
                return user.Email.ToString();
            }
            return null;
        }

        public async Task<string> GetRoleByEmailAsync(string Email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(c => c.Email == Email);
            if (user != null)
            {
                return user.Role.ToString();
            }
            return null;
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<string> GetUserIdByEmailAsync(string Email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(c => c.Email == Email);
            if (user != null)
            {
                return user.Id;
            }
            return string.Empty;
        }

        public async Task<List<User>> GetCoaches()
        {
            var coaches = _context.Users
                //.Where(coach=>!coach.BranchClubs.Any())
            //.Where(coach => coach.Role == "Coach" && !coach.BranchClubs.Any())//pobranie tylko takich, ktorzy są trenerami i nie mają innych klubow
            .ToList();
            return coaches;
        }

        public async Task<List<User>> GetAllCoaches()
        {
            var allCoaches = _context.Users
            .Where(coach => coach.Role == "Coach")
            .ToList();
            return allCoaches;
        }

		public Task<string> GetPasswordByEmailAsync(string Email)
		{
			throw new NotImplementedException();
		}

        public async Task<string> GetEmailByUserIdAsync(string userId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);
            return user.Email;
        }
	}
}
