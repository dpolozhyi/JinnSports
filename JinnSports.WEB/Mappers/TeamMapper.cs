using System.Linq;
using JinnSports.Entities.Entities;
using JinnSports.WEB.Models;

namespace JinnSports.WEB.Mappers
{
    internal static class TeamMapper
    {
        public static TeamViewModel MapToTeamViewModel(this Team team)
        {
            return new TeamViewModel
            {
                Id = team.Id,
                Name = team.Name,
                //Result = team.Results.Select(result => result.MapToResultViewModel()).ToList()
            };
        }
    }
}