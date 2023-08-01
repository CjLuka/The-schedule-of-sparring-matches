using Domain.Models.Auditable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Domain
{
    public class Club : AuditableEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public GameClass GameClass { get; set; }
        public string FeaturedImageUrl { get; set; }
        public int GameClassId { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        //public User President { get; set; }
        public List<BranchClub> Branches { get; set; }
        //public GameClass GameClass { get; set; }
    }
}
