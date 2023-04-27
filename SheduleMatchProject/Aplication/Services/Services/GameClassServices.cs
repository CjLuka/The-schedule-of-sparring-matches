using Aplication.Services.Interfaces;
using Domain.Models.Domain;
using Domain.Response;
using Microsoft.IdentityModel.Tokens;
using Persistance.Repo.Interfaces;
using Persistance.Repo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services.Services
{
    public class GameClassServices : IGameClassServices
    {
        private readonly IGameClassRepository _gameClassRepository;
        public GameClassServices(IGameClassRepository gameClassRepository)
        {
            _gameClassRepository = gameClassRepository;
        }

        public async Task<List<GameClass>> GetAllAsync()
        {
            var GameClassess = await _gameClassRepository.GetAllAsync();
            if (GameClassess.IsNullOrEmpty())
            {
                
            }
            return GameClassess;
        }
    }
}
