﻿using Domain.Models.Domain;
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
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context= context;
        }
        public async Task<List<User>> GetAllAsync()
        {
            var Users = await _context.Users.ToListAsync();
            return Users;
        }

        public async Task<User> GetByEmailAsync(string Email)
        {
            return await _context.Users.FirstOrDefaultAsync(c => c.Email == Email);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<string> GetPasswordByEmailAsync(string Email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(c => c.Email == Email);
            if (user != null)
            {
                return user.Password.ToString();
            }
            return null;
        }
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
    }
}