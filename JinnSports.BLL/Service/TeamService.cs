using System.Collections.Generic;
using System.Linq;
using JinnSports.BLL.Interfaces;
using JinnSports.DataAccessInterfaces.Interfaces;
using JinnSports.DAL.Repositories;
using JinnSports.Entities.Entities;
using JinnSports.BLL.Dtos;
using AutoMapper;

namespace JinnSports.BLL.Service
{
    public class TeamService : ITeamService
    {
        private const string SPORTCONTEXT = "SportsContext";

        private IUnitOfWork dataUnit;

        public int Count()
        {
            int count;
            using (this.dataUnit = new EFUnitOfWork(SPORTCONTEXT))
            {
                count = this.dataUnit.GetRepository<Team>().Count();
            }
            return count;
        }

        public IEnumerable<TeamDto> GetAllTeams(int skip, int take)
        {
            IList<TeamDto> teamDtoList = new List<TeamDto>();

            using (this.dataUnit = new EFUnitOfWork(SPORTCONTEXT))
            {
                IEnumerable<Team> teams = this.dataUnit.GetRepository<Team>().Get(
                    orderBy: s => s.OrderBy(x => x.Id),
                    skip: skip,
                    take: take);

                foreach (Team team in teams)
                {
                    TeamDto teamDto = new TeamDto { Id = team.Id, Name = team.Name };

                    teamDtoList.Add(teamDto);
                }
            }

            return teamDtoList;
        }
        public TeamDto GetTeamById(int teamId)
        {
            TeamDto teamDto = new TeamDto();
            Team team = new Team();
            using (this.dataUnit = new EFUnitOfWork(SPORTCONTEXT))
            {
                team = this.dataUnit.GetRepository<Team>().GetById(teamId);
            }
            teamDto = Mapper.Map<Team, TeamDto>(team);
            return teamDto;
        }
    }
}
