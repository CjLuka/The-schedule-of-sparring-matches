using Domain.Models.Domain;
using Domain.Models.VievModel;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Services.ClubServices
{
    public class AddTest : BaseTest
    {
        [Fact]
        public async Task Club_Add_IsOk()
        {
            var clubServices = new Aplication.Services.Services.ClubServices(_clubRepository.Object, _mapper);

            newClub club = new newClub()
            {
                Name= "Test",
                DateCreated= DateTime.Now,
                GameClass = new GameClass(),
                FeaturedImageUrl = "Test",
                GameClassId=1,
                UserId= "051122f7-225f-42a5-ba39-9bd535610c6e"
            };

            var response = await clubServices.AddClubAsync(club);

            response.Success.ShouldBeTrue();
            response.Data.ShouldBe(club);
        }
    }
}
