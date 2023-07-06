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
    public class BranchClubServices : IBranchClubServices
    {
        private readonly IBranchClubRepository _branchClubRepository;
        public BranchClubServices(IBranchClubRepository branchClubRepository)
        {
            _branchClubRepository = branchClubRepository;
        }

        public async Task<ServiceResponse<BranchClub>> GetBranchClubByCoach(int coachId)
        {
            var myBranchClub = await _branchClubRepository.GetClubBranchByCoach(coachId);
            if (myBranchClub == null)
            {
                return new ServiceResponse<BranchClub>
                {
                    Data = null,
                    Message = "Brak zespołu z takim trenerem",
                    Success= false
                };
            }
            return new ServiceResponse<BranchClub>
            {
                Data = myBranchClub,
                Message = "Oto twój zespół",
                Success = true
            };
        }
    }
}
