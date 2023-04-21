using Domain.Models.Auditable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Domain
{
    public class FootballPitchRequest : AuditableEntity
    {
        public int Id { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateStartEnd { get; set; }
        public Club Club { get; set; }
        public FootballPitch FootballPitch { get; set; }
    }
}
