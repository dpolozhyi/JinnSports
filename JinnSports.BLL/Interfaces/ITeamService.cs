using System.Collections.Generic;
using JinnSports.Entities.Entities;

namespace JinnSports.BLL.Interfaces
{
    public interface ITeamService : IService
    {
        IEnumerable<Team> GetAllTeams();
    }
}
