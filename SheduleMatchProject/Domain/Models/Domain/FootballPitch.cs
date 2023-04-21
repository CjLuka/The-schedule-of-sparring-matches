using Domain.Models.Auditable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Domain
{
    public class FootballPitch : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
