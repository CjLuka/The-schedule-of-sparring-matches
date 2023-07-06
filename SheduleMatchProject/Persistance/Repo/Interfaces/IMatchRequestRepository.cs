﻿using Domain.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repo.Interfaces
{
    public interface IMatchRequestRepository
    {
        Task<List<MatchRequest>> GetAllByClubBranchAsync(int reciverId);
    }
}
