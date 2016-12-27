using JinnSports.BLL.Dtos;
using JinnSports.BLL.Interfaces;
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
        /// <summary>
        /// TeamDto's in database
        /// </summary>
        private List<TeamDto> databaseTeams;

        private ITeamService teamService;

        /// <summary>
        /// TeamDto comparer for comparing collections
        /// </summary>
        private TeamDtoComparer comparer;

        /// <summary>
        /// Init test data
        /// </summary>
        [OneTimeSetUp]
        public void Init()
        {
            this.databaseTeams = new List<TeamDto>();
            this.comparer = new TeamDtoComparer();
            this.teamService = new TeamService();

            this.databaseTeams.Add(new TeamDto()
            {
                Id = 1,
                Name = "Manchester United"
            });
            this.databaseTeams.Add(new TeamDto()
            {
                Id = 2,
                Name = "Milano"
            });
            this.databaseTeams.Add(new TeamDto()
            {
                Id = 3,
                Name = "Manchester City"
            });
            this.databaseTeams.Add(new TeamDto()
            {
                Id = 4,
                Name = "Chelsea"
            });
            this.databaseTeams.Add(new TeamDto()
            {
                Id = 5,
                Name = "Bayern"
            });
            this.databaseTeams.Add(new TeamDto()
            {
                Id = 6,
                Name = "Chicago Bulls"
            });
            this.databaseTeams.Add(new TeamDto()
            {
                Id = 7,
                Name = "Los Angeles Lakers"
            });
            this.databaseTeams.Add(new TeamDto()
            {
                Id = 8,
                Name = "Phoenix Suns"
            });
        }

        /// <summary>
        /// Check team counts
        /// </summary>
        [Test]
        public void TeamCount()
        {
            int actualCount = this.teamService.Count();
            int expectedCount = this.databaseTeams.Count();
            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        [TestCase(0, 4)]
        [TestCase(0, 10)]
        [TestCase(4, 2)]
        [TestCase(4, 10)]
        [TestCase(10, 0)]
        public void GetTeams(int skip, int take)
        {
            List<TeamDto> expectedTeams = this.databaseTeams.OrderBy(t => t.Id)
                .Skip(skip)
                .Take(take)
                .ToList();
            List<TeamDto> actualTeams = this.teamService.GetAllTeams(skip, take).ToList();

            CollectionAssert.AreEqual(expectedTeams, actualTeams, this.comparer);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        public void GetTeamById(int teamId)
        {
            TeamDto expeactedTeam = this.databaseTeams.Where(t => t.Id == teamId).FirstOrDefault();
            TeamDto actualTeam = this.teamService.GetTeamById(teamId);

            Assert.AreEqual(expeactedTeam.Id, actualTeam.Id);
            Assert.AreEqual(expeactedTeam.Name, actualTeam.Name);
        }

        /// <summary>
        /// Comparer for comparing TeamDto collections 
        /// </summary>
        public class TeamDtoComparer : IComparer, IComparer<TeamDto>
        {
            public int Compare(object x, object y)
            {
                TeamDto left = x as TeamDto;
                TeamDto right = y as TeamDto;
                if (left == null || right == null)
                {
                    throw new InvalidOperationException();
                }
                return this.Compare(left, right);
            }

            public int Compare(TeamDto x, TeamDto y)
            {
                if (x.Id == y.Id && x.Name == y.Name)
                {
                    return 0;
                }
                if (x.Id < y.Id)
                {
                    return -1;
                }
                return 1;
            }

            /*
            List<SportType> sportTypes;
            List<Team> teams;
            List<TeamDto> teamsDto;

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
            public void GetTeamByIdCount([Values(1, 2, 3, 4, 5, 6, 7, 8)] int teamId)
            {
                TeamDtoComparer dtoComparer = new TeamDtoComparer();
                TeamService teamService = new TeamService();
                TeamDto testTeamDto = new TeamDto();

                testTeamDto = teamService.GetTeamById(teamId);

                Assert.IsTrue(testTeamDto.Id == this.teamsDto.ElementAt(teamId - 1).Id ||
                    testTeamDto.Name == this.teamsDto.ElementAt(teamId - 1).Name);
            }
            */
        }
    }
}
