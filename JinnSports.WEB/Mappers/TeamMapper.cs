using System.Linq;
using JinnSports.BLL.DTO;
using JinnSports.Entities.Entities;
using JinnSports.WEB.Models;

namespace JinnSports.WEB.Mappers
{
    internal static class TeamMapper
    {
        public static TeamViewModel MapToTeamViewModel(this TeamDTO team)
        {
            return new TeamViewModel
            {
                Id = team.Id,
                Name = team.Name
            };
        }
    }
}