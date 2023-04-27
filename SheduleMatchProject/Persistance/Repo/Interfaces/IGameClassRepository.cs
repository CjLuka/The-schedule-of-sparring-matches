﻿using Domain.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repo.Interfaces
{
    public interface IGameClassRepository
    {
        Task<List<GameClass>> GetAllAsync();
    }
}
