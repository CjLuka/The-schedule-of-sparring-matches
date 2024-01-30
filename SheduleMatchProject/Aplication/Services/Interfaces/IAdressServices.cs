using Domain.Models.Domain;
using Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IAdressServices
    {
        Task<ServiceResponse<List<Addresses>>> GetAllAsync();
        Task<ServiceResponse> AddAsync(Addresses addresses);
        Task<ServiceResponse> DeleteAsync(Addresses addresses);
        Task<ServiceResponse> UpdateAsync(Addresses addresses, int id);
        Task<ServiceResponse<Addresses>> GetByIdAsync(int id);
    }
}
