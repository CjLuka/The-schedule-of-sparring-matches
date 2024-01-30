using Aplication.Services.Interfaces;
using Domain.Models.Domain;
using Domain.Models.Pagination;
using Domain.Response;
using Persistance.Repo.Interfaces;
using Persistance.Repo.Repositories;
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

        public async Task AddAsync(Match match)
        {
            await _matchRepository.AddAsync(match);
        }

        public async Task<ServiceResponse<List<Match>>> GetAllAsync()
        {
            var allMatches = await _matchRepository.GetAllAsync();
            if (allMatches == null)
            {
                return new ServiceResponse<List<Match>>(false, "Brak meczów");
            }

            return new ServiceResponse<List<Match>>(allMatches, true);
        }

        public async Task<ServiceResponse<ListPaginated<Match>>> GetAllAsync(ModelPagination modelPagination)
        {
            var allMatches = await _matchRepository.GetAllAsync(modelPagination);
            if (allMatches == null)
            {
                return new ServiceResponse<ListPaginated<Match>>(false, "Brak rozegranych spotkań");
            }
            return new ServiceResponse<ListPaginated<Match>>(allMatches, true);
        }

        public async Task<ServiceResponse<List<Match>>> GetAllByBranchClubAsync(int branchClubId)
        {
            var allMatches= await _matchRepository.GetAllByBranchClubAsync(branchClubId);
            if (allMatches == null)
            {
                return new ServiceResponse<List<Match>>(false, "Brak spotkań dla danego zespołu");
            }
            
            return new ServiceResponse<List<Match>>(allMatches, true);
        }

        public async Task<ServiceResponse<List<Match>>> GetAllByClubAsync(int clubId)
        {
            var allMatches = await _matchRepository.GetAllByClubAsync(clubId);//pobranie meczow danego uzytkownika
            if (allMatches == null)
            {
                return new ServiceResponse<List<Match>>(false, "Brak meczów");
            }

            return new ServiceResponse<List<Match>>(allMatches, true);
        }

        public async Task<ServiceResponse<Match>> GetByIdAsync(int id)
        {
            var match = await _matchRepository.GetByIdAsync(id);
            if (match == null)
            {
                return new ServiceResponse<Match>(false, "Brak meczu o podanym Id");
            }
            return new ServiceResponse<Match>(match, true);
        }

        public async Task<ServiceResponse<Match>> UpdateAsync(Match match, int id)
        {
            var matchFromBase = await _matchRepository.GetByIdAsync(id);

            if(matchFromBase == null)
            {
                return new ServiceResponse<Match>(false, "Brak meczu o podanym id");
            }
            matchFromBase.GoalsHome = match.GoalsHome;
            matchFromBase.GoalsAway = match.GoalsAway;

            await _matchRepository.UpdateAsync(matchFromBase);
            return new ServiceResponse<Match>(matchFromBase, true);
        }
    }
}
