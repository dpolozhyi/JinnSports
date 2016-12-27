using JinnSports.BLL.Dtos;
using JinnSports.BLL.Extentions;
using JinnSports.BLL.Service;
using JinnSports.Entities.Entities;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinnSports.UnitTests.Services
{
    [TestFixture]
    public class TeamServiceTests
    {
        List<SportType> sportTypes;
        List<Team> teams;
        List<TeamDto> teamsDto;

        public class TeamDtoComparer : IComparer, IComparer<TeamDto>
        {
            public int Compare(object x, object y)
            {
                var ldto = x as TeamDto;
                var rdto = y as TeamDto;
                if (ldto == null || rdto == null) throw new InvalidOperationException();
                return Compare(ldto, rdto);
            }

            public int Compare(TeamDto ldto, TeamDto rdto)
            {
                if (string.IsNullOrEmpty(ldto.Name) || ldto.Id < 1)
                {
                    return -1;
                }

                return ((ldto.Name.CompareTo(rdto.Name)) &
                    (ldto.Id.CompareTo(rdto.Id)));
            }
        }
        [SetUp]
        public void Init()
        {
            this.sportTypes = new List<SportType>();
            this.teams = new List<Team>();
            this.teamsDto = new List<TeamDto>();

            SportType football = new SportType()
            {
                Id = 1,
                Name = "Football",
            };

            SportType basketball = new SportType()
            {
                Id = 2,
                Name = "Basketball"
            };

            SportType tennis = new SportType()
            {
                Id = 3,
                Name = "Tennis"
            };

            SportType snooker = new SportType()
            {
                Id = 4,
                Name = "Snooker"
            };

            // ---- Init teams ----

            // Football teams
            Team MU = new Team()
            {
                Id = 1,
                Name = "Manchester United",
                SportType = football,
                Results = new List<Result>()
            };
            Team Milano = new Team()
            {
                Id = 2,
                Name = "Milano",
                SportType = football,
                Results = new List<Result>()
            };
            Team MC = new Team()
            {
                Id = 3,
                Name = "Manchester City",
                SportType = football,
                Results = new List<Result>()
            };
            Team Chelsea = new Team()
            {
                Id = 4,
                Name = "Chelsea",
                SportType = football,
                Results = new List<Result>()
            };
            Team Bayern = new Team()
            {
                Id = 5,
                Name = "Bayern",
                SportType = football,
                Results = new List<Result>()
            };

            // Basketball teams
            Team ChicagoBulls = new Team()
            {
                Id = 6,
                Name = "Chicago Bulls",
                SportType = basketball,
                Results = new List<Result>()
            };
            Team LALakers = new Team()
            {
                Id = 7,
                Name = "Los Angeles Lakers",
                SportType = basketball,
                Results = new List<Result>()
            };
            Team PhoenixSuns = new Team()
            {
                Id = 8,
                Name = "Phoenix Suns",
                SportType = basketball,
                Results = new List<Result>()
            };

            teams.Add(MU);
            teams.Add(Milano);
            teams.Add(MC);
            teams.Add(Chelsea);
            teams.Add(Bayern);
            teams.Add(ChicagoBulls);
            teams.Add(LALakers);
            teams.Add(PhoenixSuns);


            foreach (Team team in teams)
            {
                teamsDto.Add(new TeamDto
                {
                    Id = team.Id,
                    Name = team.Name
                });
            }   
        }
        [Test]

        public void GetTeamByIdCount([Values(1,2,3,4,5,6,7,8)] int teamId)
        {
            TeamDtoComparer dtoComparer = new TeamDtoComparer();
            TeamService teamService = new TeamService();
            TeamDto testTeamDto = new TeamDto();

            testTeamDto = teamService.GetTeamById(teamId);

            Assert.IsTrue(testTeamDto.Id == this.teamsDto.ElementAt(teamId - 1).Id ||
                testTeamDto.Name == this.teamsDto.ElementAt(teamId - 1).Name);
        }
    }
}
