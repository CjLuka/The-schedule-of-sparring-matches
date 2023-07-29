using Domain.Models.Auditable;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Models.Domain
{
    public class User : IdentityUser
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? ClubId { get; set; }
        public string Role { get; set; }
        public List<BranchClub> BranchClubs { get; set; }
		public virtual Club Club { get; set; }

	}
}
