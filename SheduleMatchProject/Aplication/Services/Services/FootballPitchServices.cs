using Aplication.Services.Interfaces;
using Domain.Models.Domain;
using Domain.Response;
using Microsoft.IdentityModel.Tokens;
using Persistance.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services.Services
{
    public class FootballPitchServices : IFootballPitchServices
    {
        private readonly IFootballPitchRepository _footballPitchRepository;
        public FootballPitchServices(IFootballPitchRepository footballPitchRepository)
        {
            _footballPitchRepository = footballPitchRepository;
        }

        public async Task<ServiceResponse<List<FootballPitch>>> GetAllFootballPitchesAsync()
        {
            var allFootballPitches = await _footballPitchRepository.GetAllAsync();
            if (allFootballPitches.IsNullOrEmpty())
            {
                return new ServiceResponse<List<FootballPitch>>()
                {
                    Data= null,
                    Message = "Brak stadionów w bazie danych",
                    Success= false
                };
            }
            return new ServiceResponse<List<FootballPitch>>()
            {
                Data = allFootballPitches,
                Message = "Wszystkie stadiony",
                Success = true
            };
        }

        public async Task<ServiceResponse<List<FootballPitch>>> GetAvailableFootballPitchesForMatchRequest(DateTime dateTime)
        {
            var allFootballPitches = await _footballPitchRepository.GetAvailableFootballPitchesForMatchRequest(dateTime);
            if (allFootballPitches.IsNullOrEmpty())
            {
                return new ServiceResponse<List<FootballPitch>>()
                {
                    Data = null,
                    Message = "Brak dostępnych stadionów w tym terminie",
                    Success = false
                };
            }
            return new ServiceResponse<List<FootballPitch>>()
            {
                Data = allFootballPitches,
                Message = "Wszystkie stadiony",
                Success = true
            };
        }
    }
}
