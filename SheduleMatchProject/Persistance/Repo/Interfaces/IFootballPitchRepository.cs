using Domain.Models.Domain;
using Domain.Models.Pagination;
using Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repo.Interfaces
{
    public interface IFootballPitchRepository
    {
        Task<List<FootballPitch>> GetAllAsync();
        Task<ListPaginated<FootballPitch>> GetAllAsync(ModelPagination pagination);
        Task<List<FootballPitch>> GetAvailableFootballPitchesForMatchRequest(DateTime dateTime);
        Task AddAsync(FootballPitch footballPitch);
    }
}
