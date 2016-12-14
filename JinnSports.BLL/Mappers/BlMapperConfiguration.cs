using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using JinnSports.BLL.Dtos;
using JinnSports.Entities.Entities;

namespace JinnSports.BLL.Mappers
{
    public class BlMapperConfiguration : Profile
    {
        public override string ProfileName => "BlMapperConfiguration";

        protected override void Configure()
        {
            CreateMap<Result, KeyValuePair<string, int>>()
                .ConstructUsing(x => new KeyValuePair<string, int>(x.Team.Name, x.Score));

            CreateMap<SportEvent, SportEventDto>();

            //CreateMap<ICollection<Result>, Dictionary<string, int>>();



//            CreateMap<SportEvent, SportEventDto>()
//                .ForMember(dest => dest.SportType, opt => opt.MapFrom(src => src.SportType.Name))
//                .ForMember(dest => dest.Results, opt => opt.MapFrom(src => src.Results.ToList()))
//                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date));
        }
    }
}
