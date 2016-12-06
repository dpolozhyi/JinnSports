using System.Collections.Generic;
using JinnSports.BLL.Interfaces;
using JinnSports.Entities;
using JinnSports.DataAccessInterfaces.Interfaces;
using JinnSports.DAL.Repositories;

namespace JinnSports.BLL.Service
{
    public class TeamService : ITeamService
    {
        private IUnitOfWork dataUnit;

        public IEnumerable<Team> GetAllTeams()
        {
            IEnumerable<Team> teams;
            this.dataUnit = new EFUnitOfWork("SportsContext");

            IRepository<Team> teamsRepository = this.dataUnit.Set<Team>();
            teams = teamsRepository.GetAll();

            return teams;
        }

        public void Dispose()
        {
            this.dataUnit.Dispose();
        }
    }
}
