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

        public async Task<ServiceResponse<List<BranchClub>>> GetAllBranchClubsAsync()
        {
            var branchClubs = await _branchClubRepository.GetAllBranchClubAsync();
            if (branchClubs == null)
            {
                return new ServiceResponse<List<BranchClub>>
                {
                    Data = null,
                    Message = "Brak zespołów",
                    Success = false
                };
            }
            return new ServiceResponse<List<BranchClub>>
            {
                Data = branchClubs,
                Message = "Oto wszystkie zespoły",
                Success = true
            };
        }

        public async Task<ServiceResponse<List<BranchClub>>> GetAllBranchClubsForPlanMatchAsync(int clubId)
        {
            var branchClubs = await _branchClubRepository.GetAllBranchClubsForPlanMatch(clubId);
            if (branchClubs == null)
            {
                return new ServiceResponse<List<BranchClub>>
                {
                    Data = null,
                    Message = "Brak zespołów",
                    Success = false
                };
            }
            return new ServiceResponse<List<BranchClub>>
            {
                Data = branchClubs,
                Message = "Oto wszystkie zespoły",
                Success = true
            };
        }

        public async Task<ServiceResponse<List<BranchClub>>> GetBranchesByClubAsync(int clubId)
        {
            var myBranches = await _branchClubRepository.GetAllByClubAsync(clubId);
            if (myBranches == null)
            {
                return new ServiceResponse<List<BranchClub>>
                {
                    Data = null,
                    Message = "Brak zespołów",
                    Success = false
                };
            }
            return new ServiceResponse<List<BranchClub>>
            {
                Data = myBranches,
                Message = "Oto wszystkie zespoły",
                Success = true
            };
        }

        public async Task<ServiceResponse<BranchClub>> GetBranchClubByCoachAsync(int coachId)
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

        public async Task<ServiceResponse<BranchClub>> GetDetailBranchByIdAsync(int branchId)
        {
            var branchClub = await _branchClubRepository.GetDetailBranchByIdAsync(branchId);
            if (branchClub == null)
            {
                return new ServiceResponse<BranchClub>
                {
                    Data = null,
                    Message = "Brak zespołu",
                    Success = false
                };
            }
            return new ServiceResponse<BranchClub>
            {
                Data = branchClub,
                Message = "Zespół",
                Success = true
            };
        }
    }
}
