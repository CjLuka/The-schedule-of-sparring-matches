using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Services.BranchClubServices
{
    public class GetAllTest : BaseTest
    {
        [Fact]
        public async Task BranchClub_GetAll_IsOk()
        {
            var _branchClubServices = new Aplication.Services.Services.BranchClubServices(_branchClubRepository.Object);
            //var _clubServices = new Aplication.Services.Services.ClubServices(_clubRepository.Object, _mapper);

            var response = await _branchClubServices.GetAllBranchClubsAsync();

            response.Success.ShouldBeTrue();
            response.Data.Count.ShouldBe(3);
            
        }
    }
}
