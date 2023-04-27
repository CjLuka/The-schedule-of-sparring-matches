using Aplication.Services.Interfaces;
using AutoMapper;
using Domain.Models.Domain;
using Domain.Response;
using Microsoft.IdentityModel.Tokens;
using Persistance.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services.Services
{
    public class ClubServices : IClubServices
    {
        private readonly IClubRepository _clubRepository;
        private readonly IMapper _mapper;
        public ClubServices(IClubRepository clubRepository, IMapper mapper)
        {
            _clubRepository= clubRepository;
            _mapper= mapper;
        }


        public async Task<ServiceResponse<List<Club>>> GetAllAsync()
        {
            var Clubs = await _clubRepository.GetAllAsync();
            if (Clubs.IsNullOrEmpty())
            {
                return new ServiceResponse<List<Club>>
                {
                    Message = "Brak klubów w bazie danych!",
                    Success= false
                };
            }
            return new ServiceResponse<List<Club>>
            {
                Data = Clubs,
                Message = "Oto wszystkie kluby",
                Success = true

            };
        }
        public async Task<ServiceResponse<Club>> AddClubAsync(Club club)
        {
            var newClubIsExist = await _clubRepository.GetByNameAsync(club.Name);
            if (newClubIsExist != null)
            {
                new ServiceResponse<Club>
                {
                    Message = "Klub o podanej nazwie istnieje w bazie danych",
                    Success = false
                };
            }
            await _clubRepository.AddAsync(club);

            return new ServiceResponse<Club>
            {
                Data = club,
                Message = "Dodano nowy klub",
                Success = true
            };
        }
    }
}
