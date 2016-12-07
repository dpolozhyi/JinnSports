using System.Collections.Generic;
using JinnSports.BLL.DTO;
using JinnSports.Entities.Entities;

namespace JinnSports.BLL.Interfaces
{
    public interface ITeamService : IService
    {
        IEnumerable<TeamDTO> GetAllTeams();
        TeamDetailsDTO GetTeamDetailsById(int id);
    }
}
