using Domain.Models.Domain;
using Domain.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.VievModel
{
    public class newBranch
    {
        public BranchType Type { get; set; }
        public int UserId { get; set; }
        public int ClubId { get; set; }
    }
}
