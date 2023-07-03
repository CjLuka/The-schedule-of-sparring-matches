﻿using Domain.Models.Domain;
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
        Task<ServiceResponse<List<Match>>> GetAllAsync();
        Task<ServiceResponse<List<Match>>> GetAllByClubAsync(int clubId);

    }
}
