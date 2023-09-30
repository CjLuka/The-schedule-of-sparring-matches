using Moq;
using Persistance.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Data;

namespace Test.Repository
{
    public class BranchRepositoryMoq
    {
        public static Mock<IBranchClubRepository> getBranchClubRepository()
        {
            var _context = new MoqContext();
            var _branchClubRepository = new Mock<IBranchClubRepository>();

            _branchClubRepository.Setup(repo => repo.GetAllBranchClubAsync()).ReturnsAsync(()=>
            {
                return _context.BranchClubs;
            });




            return _branchClubRepository;
        }
    }
}
