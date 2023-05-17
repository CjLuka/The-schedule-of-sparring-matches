using Domain.Models.Auditable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Domain
{
    public class FootballPitchRequest : AuditableEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateStartEnd { get; set; }
        public Club Club { get; set; }
        public FootballPitch FootballPitch { get; set; }
        public bool IsAccepted { get; set; }
    }
}
