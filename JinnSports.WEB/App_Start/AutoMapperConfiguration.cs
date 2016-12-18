using System.Collections.Generic;
using AutoMapper;
using JinnSports.BLL.Dtos;
using JinnSports.Entities.Entities;

namespace JinnSports.WEB
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<Result, KeyValuePair<string, int>>()
                .ConstructUsing(x => new KeyValuePair<string, int>(x.Team.Name, x.Score));

                config.CreateMap<SportEvent, SportEventDto>();
            });
        }
    }
}