using Domain.Models.Auditable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Domain
{
    public class Match : AuditableEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        public Club ClubHome { get; set; }
        public Club ClubAway { get; set; }

        public int GoalsHome { get; set; }
        public int GoalsAway { get; set; }

        public FootballPitch? FootballPitch { get; set; }
    }
}
