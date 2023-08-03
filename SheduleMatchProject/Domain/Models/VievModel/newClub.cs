using Domain.Models.Auditable;
using Domain.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.VievModel
{
    public class newClub : AuditableEntity
    {
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public GameClass GameClass { get; set; }
        public string FeaturedImageUrl { get; set; }
        public int GameClassId { get; set; }
        public string UserId { get; set; }

    }
}
