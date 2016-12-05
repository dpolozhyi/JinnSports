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
        public IList<Team> GetAllTeams()
        {
            IList<Team> teams;
            IUnitOfWork dataUnit = new EFUnitOfWork("SqlServerConnection");

            IRepository<Team> teamsRepository = dataUnit.Set<Team>();
            teams = teamsRepository.GetAll();

            dataUnit.Dispose();
            return teams;
        }
    }
}
