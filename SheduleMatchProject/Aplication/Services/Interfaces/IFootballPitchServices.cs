using Domain.Models.Domain;
using Domain.Models.Pagination;
using Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services.Interfaces
{
    public interface IFootballPitchServices
    {
        Task<ServiceResponse<List<FootballPitch>>> GetAllFootballPitchesAsync();
        Task<ServiceResponse<ListPaginated<FootballPitch>>> GetAllFootballPitchesAsync(ModelPagination pagination);
        Task<ServiceResponse<List<FootballPitch>>> GetAvailableFootballPitchesForMatchRequest(DateTime dateTime);

    }
}
