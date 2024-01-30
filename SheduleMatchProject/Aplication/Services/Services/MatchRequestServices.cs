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
    public class MatchRequestServices : IMatchRequestServices
    {
        private readonly IMatchRequestRepository _matchRequestRepository;
        public MatchRequestServices(IMatchRequestRepository matchRequestRepository)
        {
            _matchRequestRepository = matchRequestRepository;
        }

        public async Task<ServiceResponse<MatchRequest>> GetMatchRequestByIdAsync(int id)
        {
            var matchReq = await _matchRequestRepository.GetMatchRequestByIdAsync(id);
            if(matchReq == null)
            {
                return new ServiceResponse<MatchRequest>(false, "Brak matchRequest o podanym id");
            }

            return new ServiceResponse<MatchRequest>(matchReq, true);
        }
        public async Task<ServiceResponse<List<MatchRequest>>> GetPlannedMatchByClubAsync(Club club)
        {
            var plannedMatch = await _matchRequestRepository.GetPlannedMatchByClubAsync(club);
            //if (plannedMatch.Count == 0)
            //{
            //    return new ServiceResponse<List<MatchRequest>>(false, "Brak meczów dla danego klubu");
            //}

            if (plannedMatch.Any())
            {
                return new ServiceResponse<List<MatchRequest>>(plannedMatch, true);
            }

            return new ServiceResponse<List<MatchRequest>>(false, "Coś poszło nie tak..");
        }

        public async Task<ServiceResponse<List<MatchRequest>>> GetPlannedMatchByBranchAsync(BranchClub branchClub)
        {
            var PlannedMatch = await _matchRequestRepository.GetPlannedMatchByBranchAsync(branchClub);
            if (PlannedMatch == null)
            {
                return new ServiceResponse<List<MatchRequest>>(false, "Brak zaplanowanych meczów dla tego zespolu");
            }

            return new ServiceResponse<List<MatchRequest>>(PlannedMatch, true);
        }

        //Funkcja wyciągająca mecze danego trenera. 1 użytkownik może trenować kilka drużyn, dlatego wyciągamy per userId
        public async Task<ServiceResponse<List<MatchRequest>>> GetPlannedMatchByCoachAsync(string userId)
        {
            var allPlannedMatch = await _matchRequestRepository.GetPlannedMatchByCoachAsync(userId);
            if(allPlannedMatch == null)
            {
                return new ServiceResponse<List<MatchRequest>>(false, "Brak danych");
            }

            return new ServiceResponse<List<MatchRequest>>(allPlannedMatch, true);
        }

        //Funkcja zwracająca zapytania o mecz otrzymane od innych klubów. Zwracane per trener
        public async Task<ServiceResponse<List<MatchRequest>>> GetPropositionsByCoachAsync(string userId)
        {
            var matchPropositions = await _matchRequestRepository.GetPropositionsByCoachAsync(userId);
            if(matchPropositions == null)
            {
                return new ServiceResponse<List<MatchRequest>>(false, "Brak zapytań o mecz dla Twojego zespołu");
            }

            return new ServiceResponse<List<MatchRequest>>(matchPropositions, true);
        }

        public async Task<ServiceResponse<MatchRequest>> PlanNewMatchAsync(MatchRequest matchRequest)
        {
            var planMatch = _matchRequestRepository.PlanNewMatchAsync(matchRequest);
            if (planMatch.IsCompletedSuccessfully)
            {
                return new ServiceResponse<MatchRequest>(true, "Poprawnie dodano nowe zapytanie o mecz");
            }

            return new ServiceResponse<MatchRequest>(false, "Coś poszlo nie tak..");
        }

        //Funkcja updateująca IsAccepted(potrzebne przy decyzji o zatwierdzeniu lub odrzuceniu meczu)
        public async Task<ServiceResponse<MatchRequest>> UpdateAsync(MatchRequest matchRequest)
        {
            var matchFromBase = await _matchRequestRepository.GetMatchRequestByIdAsync(matchRequest.Id);
            if (matchFromBase == null)
            {
                return new ServiceResponse<MatchRequest>(false, "Brak meczu o takim Id");
            }

            matchFromBase.IsAccepted = matchRequest.IsAccepted;


            await _matchRequestRepository.UpdateAsync(matchFromBase);
            return new ServiceResponse<MatchRequest>(matchFromBase, true);
        }

        public async Task<ServiceResponse<ListPaginated<MatchRequest>>> GetAllMatchRequestsAsync(ModelPagination modelPagination)
        {
            var matchRequests = await _matchRequestRepository.GetAllMatchRequestsAsync(modelPagination);
            
            if (matchRequests == null)
            {
                return new ServiceResponse<ListPaginated<MatchRequest>>(false, "Brak dostępnych meczów");
            }

            return new ServiceResponse<ListPaginated<MatchRequest>>(matchRequests,true);
        }
    }
}
