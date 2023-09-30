using Aplication.Mapper;
using Aplication.Services.Services;
using AutoMapper;
using Moq;
using Persistance.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Repository;
using Aplication.Services.Services;
using Shouldly;
using Domain.Models.Domain;

namespace Test.Services.ClubServices
{
    public class GetAllAsyncTest : BaseTest
    {
        [Fact]
        public async Task Club_GetAllAsync_IsOk()
        {

            var _clubServices = new Aplication.Services.Services.ClubServices(_clubRepository.Object, _mapper);
            var response = await _clubServices.GetAllAsync();

            response.Success.ShouldBeTrue();
            response.Data.Count.ShouldBe(3);
            response.Data.ShouldBeOfType<List<Club>>();
        }
    }
}
