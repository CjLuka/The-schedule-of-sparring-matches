using Domain.Models.Domain;
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
    }
}
