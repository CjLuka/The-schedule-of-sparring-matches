using Domain.Models.Auditable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Domain
{
    public class MatchRequest : AuditableEntity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Club Sender { get; set; }
        public Club Receiver { get; set; }
        public FootballPitch FootballPitch { get; set; }
    }
}
