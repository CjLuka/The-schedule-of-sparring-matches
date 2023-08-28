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
    public class MatchRequestServices : IMatchRequestServices
    {
        private readonly IMatchRequestRepository _matchRequestRepository;
        public MatchRequestServices(IMatchRequestRepository matchRequestRepository)
        {
            _matchRequestRepository = matchRequestRepository;
        }

        public async Task<ServiceResponse<List<MatchRequest>>> GetPlannedMatchByBranchAsync(BranchClub branchClub)
        {
            var PlannedMatch = await _matchRequestRepository.GetPlannedMatchByBranchAsync(branchClub);
            if (PlannedMatch == null)
            {
                return new ServiceResponse<List<MatchRequest>>
                {
                    Data = null, 
                    Message = "Brak zaplanowanych meczów dla tego zespolu",
                    Success= false
                };
            }
            return new ServiceResponse<List<MatchRequest>> 
            { 
               Data = PlannedMatch,
               Message = "Zaplanowane spotkania",
               Success= true
            };
        }

        //Funkcja wyciągająca mecze danego trenera. 1 użytkownik może trenować kilka drużyn, dlatego wyciągamy per userId
        public async Task<ServiceResponse<List<MatchRequest>>> GetPlannedMatchByCoachAsync(string userId)
        {
            var allPlannedMatch = _matchRequestRepository.GetPlannedMatchByCoachAsync(userId);
            if(allPlannedMatch == null)
            {
                return new ServiceResponse<List<MatchRequest>>
                {
                    Success = false,
                    Message = "Brak danych"
                };
            }
            return new ServiceResponse<List<MatchRequest>>
            {
                Data = allPlannedMatch.Result,
                Success = true,
                Message = "Twoje mecze"
            };
        }

        //Funkcja zwracająca zapytania o mecz otrzymane od innych klubów. Zwracane per trener
        public async Task<ServiceResponse<List<MatchRequest>>> GetPropositionsByCoachAsync(string userId)
        {
            var matchPropositions = await _matchRequestRepository.GetPropositionsByCoachAsync(userId);
            if(matchPropositions == null)
            {
                return new ServiceResponse<List<MatchRequest>>
                {
                    Success= false,
                    Message="Brak zapytań o mecz dla Twojego zespołu"
                };
            }
            return new ServiceResponse<List<MatchRequest>>
            {
                Success = true,
                Message = "Oto propozycję dla Twojego zespołu",
                Data = matchPropositions
            };
        }

        public async Task<ServiceResponse<MatchRequest>> PlanNewMatchAsync(MatchRequest matchRequest)
        {
            var planMatch = _matchRequestRepository.PlanNewMatchAsync(matchRequest);
            if (planMatch.IsCompletedSuccessfully)
            {
                return new ServiceResponse<MatchRequest>
                {
                    Message = "Poprawnie dodano nowe zapytanie o mecz",
                    Success = true
                };
            }
            return new ServiceResponse<MatchRequest>
            {
                Success = false,
                Message = "Coś poszło nie tak.."
            };
        }
    }
}
