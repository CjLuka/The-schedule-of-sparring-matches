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
                return new ServiceResponse<List<BranchClub>>(false, "Brak zespołów");
            }

            return new ServiceResponse<List<BranchClub>>(branchClubs, true);
        }

        public async Task<ServiceResponse<List<BranchClub>>> GetAllBranchClubsForPlanMatchAsync(int clubId)
        {
            var branchClubs = await _branchClubRepository.GetAllBranchClubsForPlanMatch(clubId);
            if (branchClubs == null)
            {
                return new ServiceResponse<List<BranchClub>>(false, "Brak zespołów");
            }

            return new ServiceResponse<List<BranchClub>>(branchClubs, true);
        }

        public async Task<ServiceResponse<List<BranchClub>>> GetBranchesByClubAsync(int clubId)
        {
            var myBranches = await _branchClubRepository.GetAllByClubAsync(clubId);
            if (myBranches == null)
            {
                return new ServiceResponse<List<BranchClub>>(false, "Brak zespołów");
            }

            return new ServiceResponse<List<BranchClub>>(myBranches, true);
        }

        public async Task<ServiceResponse<BranchClub>> GetBranchClubByCoachAsync(string coachId)
        {
            var myBranchClub = await _branchClubRepository.GetClubBranchByCoach(coachId);
            if (myBranchClub == null)
            {
                return new ServiceResponse<BranchClub>(false, "Brak zespołu z takim trenerem");
            }

            return new ServiceResponse<BranchClub>(myBranchClub, true);
        }

        public async Task<ServiceResponse<BranchClub>> GetDetailBranchByIdAsync(int branchId)
        {
            var branchClub = await _branchClubRepository.GetBranchByIdAsync(branchId);
            if (branchClub == null)
            {
                return new ServiceResponse<BranchClub>(false, "Brak zespołu");
            }

            return new ServiceResponse<BranchClub>(branchClub, true);
        }

        public async Task<ServiceResponse<BranchClub>> AddBranchAsync(BranchClub branchClub)
        {
            await _branchClubRepository.AddAsync(branchClub);

            return new ServiceResponse<BranchClub>(true, "Dodano nowy oddział");
        }

        public async Task<ServiceResponse<BranchClub>> UpdateBranchAsync(BranchClub branchClub, int id)
        {
            var branchFromBase = await _branchClubRepository.GetBranchByIdAsync(id);
            if (branchFromBase == null)
            {
                return new ServiceResponse<BranchClub>(false, "Brak klubu o takim id");
            }
            //branchClub.Type= branchFromBase.Type;
            //branchClub.UserId = branchFromBase.UserId;
            //branchClub.LastModifiedDate = DateTime.Now;
            //branchClub.ClubId= branchFromBase.ClubId;

            branchFromBase.Type= branchClub.Type;
            branchFromBase.UserId= branchClub.UserId;
            //branchFromBase.ClubId= branchClub.ClubId;
            branchFromBase.ClubId = branchFromBase.ClubId;
            branchFromBase.LastModifiedDate= DateTime.Now;


            try
            {
                await _branchClubRepository.UpdateAsync(branchFromBase);
            }
            catch (Exception)
            {

                throw;
            }


            return new ServiceResponse<BranchClub>(branchClub, true);
        }

        public async Task<ServiceResponse<BranchClub>> DeleteBranchAsync(int id)
        {
            var branch = await _branchClubRepository.GetBranchByIdAsync(id);
            if (branch == null)
            {
                return new ServiceResponse<BranchClub>(false, "Oddział o podanym Id nie istnieje");
            }
            await _branchClubRepository.DeleteAsync(branch);

            return new ServiceResponse<BranchClub>(true, "Usunięto oddział");
        }

        public async Task<ServiceResponse<int>> CountBranchesForCoach(string userId)
        {
            var allBranches = await _branchClubRepository.GetAllBranchClubAsync();
            int count = 0;
            foreach (var item in allBranches)
            {
                if(item.UserId == userId)
                {
                    count++;
                }
            }
            return new ServiceResponse<int>(count, true);
        }

        public async Task<ServiceResponse<List<BranchClub>>> GetAllBranchesForCoach(string userId)
        {
            var branches = await _branchClubRepository.GetAllBranchesForCoach(userId);
            if(branches == null)
            {
                return new ServiceResponse<List<BranchClub>>(false, "Brak klubów");
            }

            return new ServiceResponse<List<BranchClub>>(branches, true);
        }

        public async Task<ServiceResponse<BranchClub>> GetBranchClubById(int selectedClubId)
        {
            var branch = await _branchClubRepository.GetBranchByIdAsync(selectedClubId);
            if (branch == null)
            {
                return new ServiceResponse<BranchClub>(false, "Brak klubu o tym Id");
            }

            return new ServiceResponse<BranchClub>(branch, true);
        }
    }
}
