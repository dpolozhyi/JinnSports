using AutoMapper;
using JinnSports.BLL.Mappers;

namespace JinnSports.WEB
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(config =>
            {
                config.AddProfile<BlMapperConfiguration>();
            });
        }
    }
}