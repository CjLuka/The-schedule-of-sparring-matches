using Domain.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Domain
{
    public class BranchClub
    {
        public int Id { get; set; }
        public BranchType Type { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ClubId { get; set; }
        public Club Club { get; set; }
    }
}
