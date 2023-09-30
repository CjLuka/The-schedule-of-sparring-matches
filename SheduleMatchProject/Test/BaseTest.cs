using Aplication.Mapper;
using AutoMapper;
using Moq;
using Persistance.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Repository;

namespace Test
{
    public class BaseTest
    {
        protected readonly IMapper _mapper;
        protected readonly Mock<IClubRepository> _clubRepository;
        protected readonly Mock<IBranchClubRepository> _branchClubRepository;
        public BaseTest()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
            _clubRepository = ClubRepositoryMoq.getClubRepository();
            _branchClubRepository = BranchRepositoryMoq.getBranchClubRepository();
        }


    }
}
