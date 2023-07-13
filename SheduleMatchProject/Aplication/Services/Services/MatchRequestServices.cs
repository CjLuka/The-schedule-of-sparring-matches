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

        public async Task<ServiceResponse<List<MatchRequest>>> GetPlannedMatchAsync(BranchClub branchClub)
        {
            var PlannedMatch = await _matchRequestRepository.GetPlannedMatchAsync(branchClub);
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
    }
}
