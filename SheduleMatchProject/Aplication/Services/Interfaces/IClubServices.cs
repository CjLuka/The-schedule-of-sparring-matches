using Domain.Models.Domain;
using Domain.Models.VievModel;
using Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services.Interfaces
{
    public interface IClubServices
    {
        Task<ServiceResponse<List<Club>>> GetAllAsync();
        Task<ServiceResponse<newClub>> AddClubAsync(newClub club);
        Task<ServiceResponse<Club>> UpdateClubAsync(Club club, int id, string lastModifiedBy);//3 parametr po to, aby móc ustawic pole LastModifiedBy
        Task<ServiceResponse<Club>> GetDetailClubAsync(int id);
        Task<ServiceResponse<Club>> UpdateLastModifedBy(Club club,string lastModifedBy);
        Task<ServiceResponse<Club>> DeleteClubAsync(int id);
        Task<ServiceResponse<Club>> GetClubByPresidentIdAsync(string userId);
    }
}
