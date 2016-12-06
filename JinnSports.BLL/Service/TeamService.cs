using System.Collections.Generic;
using JinnSports.BLL.Interfaces;
using JinnSports.DataAccessInterfaces.Interfaces;
using JinnSports.DAL.Repositories;
using JinnSports.Entities.Entities;

namespace JinnSports.BLL.Service
{
    public class TeamService : ITeamService
    {
        private IUnitOfWork dataUnit;

        public IEnumerable<Team> GetAllTeams()
        {
            this.dataUnit = new EFUnitOfWork("SportsContext");

            IRepository<Team> teamsRepository = this.dataUnit.Set<Team>();
            var teams = teamsRepository.GetAll();

            return teams;
        }

        public void Dispose()
        {
            this.dataUnit.Dispose();
        }
    }
}
