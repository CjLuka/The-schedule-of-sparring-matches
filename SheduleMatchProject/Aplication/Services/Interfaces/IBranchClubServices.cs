using Domain.Models.Domain;
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
        Task<ServiceResponse<BranchClub>> GetBranchClubByCoach(int coachId);
        Task<ServiceResponse<List<BranchClub>>> GetAllBranchClubs();
        Task<ServiceResponse<List<BranchClub>>> GetAllBranchClubsForPlanMatch(int clubId);//serwis pobierający wszystkie zespoły, poza zespolami z klubu, który składa prośbę o mecz
    }
}
