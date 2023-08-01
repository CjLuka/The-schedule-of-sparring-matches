using Aplication.Services.Interfaces;
using AutoMapper;
using Domain.Models.Domain;
using Domain.Models.VievModel;
using Domain.Response;
using Microsoft.IdentityModel.Tokens;
using Persistance.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            _clubRepository = clubRepository;
            _mapper = mapper;
        }


        public async Task<ServiceResponse<List<Club>>> GetAllAsync()
        {
            var Clubs = await _clubRepository.GetAllAsync();
            if (Clubs.IsNullOrEmpty())
            {
                return new ServiceResponse<List<Club>>
                {
                    Message = "Brak klubów w bazie danych!",
                    Success = false
                };
            }
            return new ServiceResponse<List<Club>>
            {
                Data = Clubs,
                Message = "Oto wszystkie kluby",
                Success = true

            };
        }
        public async Task<ServiceResponse<newClub>> AddClubAsync(newClub newClub)
        {
            var newClubIsExist = await _clubRepository.GetByNameAsync(newClub.Name);
            if (newClubIsExist != null)
            {
                new ServiceResponse<newClub>
                {
                    Message = "Klub o podanej nazwie istnieje w bazie danych",
                    Success = false
                };
            }
            await _clubRepository.AddAsync(_mapper.Map<Club>(newClub));
            //await _clubRepository.AddAsync(club);
            return new ServiceResponse<newClub>
            {
                Data = _mapper.Map<newClub>(newClub),
                Message = "Dodano nowy klub",
                Success = true
            };
        }

        public async Task<ServiceResponse<Club>> UpdateClubAsync(Club club, int id, string lastModifiedBy)
        {
            var clubFromBase = await _clubRepository.GetByIdAsync(id);
            if (clubFromBase == null)
            {
                return new ServiceResponse<Club>()
                {
                    Message = "Brak klubu o takim Id",
                    Success = false
                };
            }
            clubFromBase.Name = club.Name;
            clubFromBase.DateCreated = club.DateCreated;
            clubFromBase.GameClassId = club.GameClassId;
            clubFromBase.UserId = clubFromBase.UserId;
            clubFromBase.LastModifiedBy = lastModifiedBy;
            clubFromBase.LastModifiedDate = DateTime.UtcNow;
            clubFromBase.UserId = club.UserId;
            clubFromBase.CreatedBy = clubFromBase.CreatedBy;

            await _clubRepository.UpdateAsync(clubFromBase);
            return new ServiceResponse<Club>()
            {
                Data = clubFromBase,
                Success = true
            };
        }

        public async Task<ServiceResponse<Club>> GetDetailClubAsync(int id)
        {
            var Club = await _clubRepository.GetByIdAsync(id);
            return new ServiceResponse<Club>()
            {
                Data = Club,
                Message = "Twoj klub",
                Success = true
            };
        }

        public async Task<ServiceResponse<Club>> UpdateLastModifedBy(Club club, string lastModifedBy)
        {
            club.LastModifiedBy = lastModifedBy;
            await _clubRepository.UpdateAsync(club);
            return new ServiceResponse<Club>()
            {
                Data = club
            };
        }

        public async Task<ServiceResponse<Club>> DeleteClubAsync(int id)
        {
            var club = await _clubRepository.GetByIdAsync(id);
            if(club == null)
            {
                return new ServiceResponse<Club>()
                {
                    Message = "Podany klub nie istnieje",
                    Success = false
                };
            }
            await _clubRepository.DeleteAsync(club);
            return new ServiceResponse<Club>()
            {
                Message = "Usunięto klub",
                Success = true
            };
        }

        public async Task<ServiceResponse<Club>> GetClubByPresidentIdAsync(Guid userId)
        {
            var club = await _clubRepository.GetClubByPresidentIdAsync(userId);
            if (club == null)
            {
                return new ServiceResponse<Club>()
                {
                    Message = "Coś poszło nie tak.",
                    Success= false
                };
            }
            return new ServiceResponse<Club>()
            {
                Data= club,
                Success = true,
                Message = "Twoj klub"
            };
        }
    }
}
