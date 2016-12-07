using System.Collections.Generic;
using JinnSports.BLL.DTO;
using JinnSports.Entities.Entities;

namespace JinnSports.BLL.Mappers
{
    internal static class TeamDtoMapper
    {
        public static TeamDTO MapToTeamDto(this Team team)
        {
            return new TeamDTO
            {
                Id = team.Id,
                Name = team.Name
            };
        }
    }
}
