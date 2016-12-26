using System.Collections.Generic;
using System.Linq;
using JinnSports.BLL.Interfaces;
using JinnSports.DataAccessInterfaces.Interfaces;
using JinnSports.Entities.Entities;
using JinnSports.BLL.Dtos;

namespace JinnSports.BLL.Service
{
    public class TeamService : ITeamService
    {
        private readonly IUnitOfWork dataUnit;

        public TeamService(IUnitOfWork unitOfWork)
        {
            this.dataUnit = unitOfWork;
        }

        public int Count()
        {
            var count = this.dataUnit.GetRepository<Team>().Count();
            return count;
        }

        public IEnumerable<TeamDto> GetAllTeams(int skip, int take)
        {
            IList<TeamDto> teamDtoList = new List<TeamDto>();
            
            IEnumerable<Team> teams = this.dataUnit.GetRepository<Team>().Get(
                orderBy: s => s.OrderBy(x => x.Id), 
                skip: skip, 
                take: take);

            foreach (Team team in teams)
            {
                TeamDto teamDto = new TeamDto { Id = team.Id, Name = team.Name };

                teamDtoList.Add(teamDto);
            }

           return teamDtoList;
        }
    }
}
