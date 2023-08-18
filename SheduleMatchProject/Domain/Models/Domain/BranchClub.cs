using Domain.Models.Auditable;
using Domain.Models.Enum;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Domain
{
    [JsonObject(IsReference = true)]
    public class BranchClub : AuditableEntity
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public int Id { get; set; }
        public BranchType Type { get; set; }
        //public Guid UserId { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int ClubId { get; set; }
        public Club Club { get; set; }
    }
}
