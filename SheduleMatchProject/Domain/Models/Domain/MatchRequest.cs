using Domain.Models.Auditable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Domain
{
    public class MatchRequest : AuditableEntity
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public BranchClub Sender{ get; set; }
        public BranchClub Receiver { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public FootballPitch FootballPitch { get; set; }
        public int? FootballPitchId { get; set; }
        public bool IsAccepted { get; set; }
        public Match Match { get; set; }

    }
}
