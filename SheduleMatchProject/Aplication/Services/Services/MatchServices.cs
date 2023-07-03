using Aplication.Services.Interfaces;
using Domain.Models.Domain;
using Domain.Response;
using Persistance.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services.Services
{
    public class MatchServices : IMatchServices
    {
        private readonly IMatchRepository _matchRepository;
        public MatchServices(IMatchRepository matchRepository)
        {
            _matchRepository= matchRepository;
        }
        public async Task<ServiceResponse<List<Match>>> GetAllAsync()
        {
            var allMatches = await _matchRepository.GetAllAsync();
            if (allMatches == null)
            {
                return new ServiceResponse<List<Match>>
                {
                    Message = "Brak meczow",
                    Success= false
                };
            }
            return new ServiceResponse<List<Match>>
            {
                Data = allMatches,
                Message = "Wszystkie mecze",
                Success = true
            };
        }

        public async Task<ServiceResponse<List<Match>>> GetAllByClubAsync(int clubId)
        {
            var allMatches = await _matchRepository.GetAllByClubAsync(clubId);//pobranie meczow danego uzytkownika
            if (allMatches == null)
            {
                return new ServiceResponse<List<Match>>
                {
                    Message = "Brak meczow",
                    Success = false
                };
            }
            return new ServiceResponse<List<Match>>
            {
                Data = allMatches,
                Message = "Wszystkie mecze",
                Success = true
            };
        }
    }
}
