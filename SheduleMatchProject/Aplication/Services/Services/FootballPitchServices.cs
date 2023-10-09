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
                return new ServiceResponse<List<FootballPitch>>(false, "Brak stadionów w bazie danych");
            }

            return new ServiceResponse<List<FootballPitch>>(allFootballPitches, true);
        }

        public async Task<ServiceResponse<List<FootballPitch>>> GetAvailableFootballPitchesForMatchRequest(DateTime dateTime)
        {
            var allFootballPitches = await _footballPitchRepository.GetAvailableFootballPitchesForMatchRequest(dateTime);
            if (allFootballPitches.IsNullOrEmpty())
            {
                return new ServiceResponse<List<FootballPitch>>(false, "Brak dostępnych stadionów w tym terminie");
            }

            return new ServiceResponse<List<FootballPitch>>(allFootballPitches, true);
        }
    }
}
