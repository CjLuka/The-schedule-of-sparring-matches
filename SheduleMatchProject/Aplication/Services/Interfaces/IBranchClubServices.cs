﻿using Domain.Models.Domain;
using Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services.Interfaces
{
    public interface IBranchClubServices
    {
        Task<ServiceResponse<BranchClub>> GetBranchClubByCoachAsync(int coachId);
        Task<ServiceResponse<List<BranchClub>>> GetBranchesByClubAsync(int clubId);
        Task<ServiceResponse<List<BranchClub>>> GetAllBranchClubsAsync();
        Task<ServiceResponse<BranchClub>> GetDetailBranchByIdAsync(int branchId);
        Task<ServiceResponse<List<BranchClub>>> GetAllBranchClubsForPlanMatchAsync(int clubId);//serwis pobierający wszystkie zespoły, poza zespolami z klubu, który składa prośbę o mecz
        Task<ServiceResponse<BranchClub>> AddBranchAsync(BranchClub branchClub);
        Task<ServiceResponse<BranchClub>> UpdateBranchAsync(BranchClub branchClub, int id, string lastModifiedBy);
        Task<ServiceResponse<BranchClub>> DeleteBranchAsync(BranchClub branchClub);


    }
}
