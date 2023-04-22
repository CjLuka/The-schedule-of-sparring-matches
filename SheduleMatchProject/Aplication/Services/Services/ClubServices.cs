using Aplication.Services.Interfaces;
using Domain.Models.Domain;
using Domain.Response;
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
        
        public Task<ServiceResponse<List<Club>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
