﻿using Domain.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repo.Interfaces
{
    public interface IMatchRequestRepository
    {
        Task<List<MatchRequest>> GetPlannedMatchByBranchAsync(BranchClub branchClub);//wyswietlenie zaplanowanych meczów dla trenerów(Dany branchClub)
        Task<List<MatchRequest>> GetPlannedMatchByClubAsync(Club club);
        Task<List<MatchRequest>> GetPlannedMatchByCoachAsync(string userId);
        Task PlanNewMatchAsync(MatchRequest matchRequest);
        Task<List<MatchRequest>> GetPropositionsByCoachAsync(string userId);
    }
}
