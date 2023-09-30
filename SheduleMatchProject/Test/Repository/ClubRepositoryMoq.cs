using Domain.Models.Domain;
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
    public class ClubRepositoryMoq
    {
       
        public static Mock<IClubRepository> getClubRepository()
        { 
            var _context = new MoqContext();
            var _clubRepository = new Mock<IClubRepository>();

            //Test pobrania wszystkich klubów
            _clubRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(() =>
            {
                return _context.Clubs;
            });

            //Test usunięcia klubu
            _clubRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Club>())).Callback((Club club) =>
            {
                var clubForDelete = _context.Clubs.FirstOrDefault(c => c.Id == club.Id);
                if (clubForDelete != null)
                {
                    _context.Clubs.Remove(club);
                }
            }).Returns(Task.CompletedTask);

            //Test dodania klubu
            _clubRepository.Setup(repo => repo.AddAsync(It.IsAny<Club>())).Returns(Task.CompletedTask);

            //_clubRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Club>())).Returns(Task.CompletedTask);


            return _clubRepository;
        }
    }
}
