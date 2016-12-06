using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JinnSports.BLL.Interfaces;
using JinnSports.BLL.DTO;
using JinnSports.Entities;
using JinnSports.DataAccessInterfaces.Interfaces;
using JinnSports.DAL.Repositories;

namespace JinnSports.BLL.Service
{
    public class TeamService : ITeamService
    {
        IUnitOfWork dataUnit = new EFUnitOfWork("SportsContext");

        public IEnumerable<Team> GetAllTeams()
        {
            IEnumerable<Team> teams;

            IRepository<Team> teamsRepository = dataUnit.Set<Team>();
            teams = teamsRepository.GetAll();

            //dataUnit.Dispose();
            return teams;
        }

        public void Dispose()
        {
            dataUnit.Dispose();
        }
    }
}
