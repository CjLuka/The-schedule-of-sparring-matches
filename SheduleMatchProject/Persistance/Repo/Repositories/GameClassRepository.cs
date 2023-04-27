using Domain.Models.Domain;
using Persistance.Data;
using Persistance.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repo.Repositories
{
    public class GameClassRepository : IGameClassRepository
    {
        private readonly ApplicationDbContext _context;
        public GameClassRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<GameClass>> GetAllAsync()
        {
            var gameClassList =  _context.GameClasses.ToList();
            return gameClassList;
        }
    }
}
