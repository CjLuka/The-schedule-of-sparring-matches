using Domain.Models.Domain;
using Domain.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Data
{
    public class MoqContext
    {
        public List<Club> Clubs { get; set; }
        public List<BranchClub> BranchClubs { get; set; }
        public MoqContext()
        {
            Clubs = new List<Club>()
            {
                new Club()
                {
                    Id = 1,
                    Name= "Jaśliska",
                    DateCreated = new DateTime(2000, 10, 10),
                    GameClassId= 1,
                    FeaturedImageUrl = "test",
                    UserId = "571dbbce-483a-41b7-aae3-04b190326e2a",
                    User = new User(),
                    GameClass = new GameClass(),
                    Branches = new List<BranchClub>()
                },
                new Club()
                {
                    Id = 2,
                    Name= "Krosno",
                    DateCreated = new DateTime(2001, 10, 10),
                    GameClassId= 1,
                    FeaturedImageUrl = "test2",
                    UserId = "3dede767-5dda-4593-ba4b-73ff525955be",
                    User = new User(),
                    GameClass = new GameClass(),
                    Branches = new List<BranchClub>()
                },
                new Club()
                {
                    Id = 3,
                    Name= "Jaśliska",
                    DateCreated = new DateTime(1999, 10, 10),
                    GameClassId= 2,
                    FeaturedImageUrl = "test3",
                    UserId = "c85f7cf1-46f1-4123-a77f-2a71eeff01d4",
                    User = new User(),
                    GameClass = new GameClass(),
                    Branches = new List<BranchClub>()
                }
            };
            BranchClubs = new List<BranchClub>()
            {
                new BranchClub()
                {
                    Id = 1,
                    Type = BranchType.Senior,
                    UserId = "051122f7-225f-42a5-ba39-9bd535610c6e",
                    User = new User(),
                    ClubId= 1,
                    Club = new Club()
                },
                new BranchClub()
                {
                    Id = 2,
                    Type = BranchType.Senior,
                    UserId = "3dede767-5dda-4593-ba4b-73ff525955be",
                    User = new User(),
                    ClubId= 2,
                    Club = new Club()
                },
                new BranchClub()
                {
                    Id = 3,
                    Type = BranchType.Junior,
                    UserId = "464ecc05-4eb0-4794-b312-55fba38ac052",
                    User = new User(),
                    ClubId= 3,
                    Club = new Club()
                }
            };
            

        }
        

    }
}
//public int Id { get; set; }
//public BranchType Type { get; set; }
//public string UserId { get; set; }
//public User User { get; set; }
//public int ClubId { get; set; }
//public Club Club { get; set; }