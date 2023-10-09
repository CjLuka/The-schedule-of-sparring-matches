using Domain.Models.Domain;
using Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services.Interfaces
{
    public interface IMatchRequestServices
    {
        Task<ServiceResponse<List<MatchRequest>>> GetPlannedMatchByBranchAsync(BranchClub branchClub);
        Task<ServiceResponse<List<MatchRequest>>> GetPlannedMatchByCoachAsync(string userId);
        Task<ServiceResponse<MatchRequest>> PlanNewMatchAsync(MatchRequest matchRequest);
        Task<ServiceResponse<List<MatchRequest>>> GetPropositionsByCoachAsync(string userId);
        Task<ServiceResponse<MatchRequest>> GetMatchRequestByIdAsync(int id);
        Task<ServiceResponse<MatchRequest>> UpdateAsync(MatchRequest matchRequest);
        Task<ServiceResponse<List<MatchRequest>>> GetPlannedMatchByClubAsync(Club club);

    }
}
