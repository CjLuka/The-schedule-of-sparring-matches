using AutoMapper;
using Domain.Models.Domain;
using Domain.Models.VievModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Models.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Club, showAllClubs>().ReverseMap();
            CreateMap<Club, newClub>().ReverseMap();
            CreateMap<BranchClub, newBranch>().ReverseMap();
        }
    }
}
