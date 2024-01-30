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
    public interface IMatchServices
    {
        Task AddAsync(Match match);
        Task<ServiceResponse<List<Match>>> GetAllAsync();
        Task<ServiceResponse<List<Match>>> GetAllByClubAsync(int clubId);
        Task<ServiceResponse<List<Match>>> GetAllByBranchClubAsync(int branchClubId);
        Task<ServiceResponse<ListPaginated<Match>>> GetAllAsync(ModelPagination modelPagination);
        Task<ServiceResponse<Match>> UpdateAsync(Match match, int id);
        Task<ServiceResponse<Match>> GetByIdAsync(int id);

    }
}
