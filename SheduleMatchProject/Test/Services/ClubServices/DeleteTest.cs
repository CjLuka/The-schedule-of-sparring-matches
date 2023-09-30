using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Services.ClubServices
{
    public class DeleteTest : BaseTest
    {

        [Fact]
        public async Task Club_Delete_IsOk()
        {
            var _clubServices = new Aplication.Services.Services.ClubServices(_clubRepository.Object, _mapper);
            var response = await _clubServices.DeleteClubAsync(2);

            response.Success.ShouldBeTrue();
            response.Data.ShouldBeNull();
            response.Message.ShouldBe("Usunięto klub");
        }
    }
}
