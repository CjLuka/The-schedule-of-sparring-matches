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
        [ForeignKey("GameClass")]
        public int GameClassId { get; set; }
        //public GameClass GameClass { get; set; }
        public List<User> Users { get; set; }
    }
}
