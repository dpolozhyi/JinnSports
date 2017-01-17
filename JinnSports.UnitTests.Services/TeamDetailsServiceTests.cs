using System;
using JinnSports.BLL.Service;
using JinnSports.Entities.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using JinnSports.BLL.Dtos;
using JinnSports.DAL.Repositories;
using JinnSports.DataAccessInterfaces.Interfaces;
using System.Linq;
using System.Collections;
using System.Data.Entity;
using JinnSports.BLL.Extentions;
using AutoMapper;
using JinnSports.DAL.EFContext;
using JinnSports.WEB;

namespace JinnSports.UnitTests.Services
{
    [TestFixture]
    public class TeamDetailsServiceTests
    {
        private TeamDetailsService teamDetailsService;

        private SportsContext databaseSportsContext;

        private DbContextTransaction databaseTransaction;



        private List<SportType> sportTypes;
        private List<Team> teams;
        private List<Result> results;
        private List<SportEvent> sportEvents;
        private IEnumerable<List<ResultDto>> resultsDtoCollection;
        private List<ResultDto> resultsDto;
        private List<Result> testResults;

        [OneTimeSetUp]
        public void Init()
        {
            this.databaseSportsContext = new SportsContext("SportsContext");

            // Other transactions can't update and insert data
            this.databaseTransaction = this.databaseSportsContext
                .Database.BeginTransaction(System.Data.IsolationLevel.Serializable);

            // Clear tables
            this.databaseSportsContext.Results.RemoveRange(
                this.databaseSportsContext.Results);
            this.databaseSportsContext.SportEvents.RemoveRange(
                this.databaseSportsContext.SportEvents);
            this.databaseSportsContext.Teams.RemoveRange(
                this.databaseSportsContext.Teams);
            this.databaseSportsContext.SportTypes.RemoveRange(
                this.databaseSportsContext.SportTypes);

            this.databaseSportsContext.SaveChanges();

            AutoMapperConfiguration.Configure();

            this.databaseSportsContext.Database.ExecuteSqlCommand(
                @"SET IDENTITY_INSERT [dbo].[SportTypes] ON;
                INSERT INTO [dbo].[SportTypes] ([Id], [Name])
                VALUES
                (1, 'Football'),
                (2, 'Basketball'),
                (3, 'Tennis'),
                (4, 'Snooker');               
                SET IDENTITY_INSERT [dbo].[SportTypes] OFF;");

            this.databaseSportsContext.Database.ExecuteSqlCommand(
                @"SET IDENTITY_INSERT [dbo].[Teams] ON;
                INSERT INTO [dbo].[Teams] ([Id], [Name], [SportType_Id])
                VALUES
                (1, 'Manchester United', 1),
                (2, 'Milano', 1),
                (3, 'Manchester City', 1),
                (4, 'Chelsea', 1),
                (5, 'Bayern', 1),
                (6, 'Chicago Bulls', 2),
                (7, 'Los Angeles Lakers', 2),
                (8, 'Phoenix Suns', 2);               
                SET IDENTITY_INSERT [dbo].[Teams] OFF;");

            this.databaseSportsContext.Database.ExecuteSqlCommand(
                @"SET IDENTITY_INSERT [dbo].[SportEvents] ON;
                INSERT INTO [dbo].[SportEvents] ([Id], [Date], [SportType_Id])
                VALUES
                (1, '20161119 17:00', 1),
                (2, '20161028 17:00', 1),
                (3, '20161017 18:00', 1),
                (4, '20161103 16:00', 1),
                (5, '20161105 16:00', 2),
                (6, '20161129 16:00', 2),
                (7, '20161115 16:00', 2);               
                SET IDENTITY_INSERT [dbo].[SportEvents] OFF;");

            this.databaseSportsContext.Database.ExecuteSqlCommand(
                @"SET IDENTITY_INSERT [dbo].[Results] ON;
                INSERT INTO [dbo].[Results] ([Id], [Score], [SportEvent_Id], [Team_Id])
                VALUES
                (1, 2, 1, 1),
                (2, 1, 2, 2),
                (3, 3, 4, 2),
                (4, 1, 1, 3),
                (5, 0, 3, 3),
                (6, 0, 3, 4),
                (7, 2, 4, 4),
                (8, 4, 2, 5),
                (9, 68, 5, 6),
                (10, 52, 6, 6),
                (11, 65, 5, 7),
                (12, 65, 7, 7),
                (13, 64, 7, 8),
                (14, 52, 6, 8);               
                SET IDENTITY_INSERT [dbo].[Results] OFF;");

            this.databaseSportsContext.SaveChanges();

            this.teamDetailsService = new TeamDetailsService(
                new EFUnitOfWork(this.databaseSportsContext));
        }

        [OneTimeTearDown]
        public void Clean()
        {
            this.databaseTransaction.Rollback();
            this.databaseTransaction.Dispose();
        }


        [SetUp]
        public void InitTestEntities()
        {
            this.sportTypes = new List<SportType>();
            this.teams = new List<Team>();
            this.results = new List<Result>();
            this.sportEvents = new List<SportEvent>();

            this.testResults = new List<Result>();
            this.resultsDto = new List<ResultDto>();
            this.resultsDtoCollection = new List<List<ResultDto>>();

            AutoMapperConfiguration.Configure();
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
            //    --- Init Events --- 

            SportEvent ChicagoBulls_vs_LA_event = new SportEvent()
            {
                Id = 1,
                Date = new DateTime(2016, 11, 5, 16, 0, 0),
                SportType = basketball,
                Results = new List<Result>()
            };
            SportEvent ChicagoBulls_vs_Suns_event = new SportEvent()
            {
                Id = 2,
                Date = new DateTime(2016, 11, 29, 16, 0, 0),
                SportType = basketball,
                Results = new List<Result>()
            };
            SportEvent LA_vs_Suns_event = new SportEvent()
            {
                Id = 3,
                Date = new DateTime(2016, 11, 15, 16, 0, 0),
                SportType = basketball,
                Results = new List<Result>()
            };

            SportEvent MU_vs_MC_event = new SportEvent()
            {
                Id = 5,
                Date = new DateTime(2016, 11, 19, 17, 0, 0),
                SportType = football,
                Results = new List<Result>()
            };

            SportEvent Bayern_vs_Milano_event = new SportEvent()
            {
                Id = 6,
                Date = new DateTime(2016, 10, 28, 17, 0, 0),
                SportType = football,
                Results = new List<Result>()
            };
            Result Bayern_vs_Milano = new Result()
            {
                Id = 8,
                Team = Bayern,
                Score = 4,
                SportEvent = Bayern_vs_Milano_event
            };
            Result Milano_vs_Bayern = new Result()
            {
                Id = 2,
                Team = Milano,
                Score = 1,
                SportEvent = Bayern_vs_Milano_event
            };


            SportEvent Chelsea_vs_MC_event = new SportEvent()
            {
                Id = 7,
                Date = new DateTime(2016, 10, 17, 18, 0, 0),
                SportType = football,
                Results = new List<Result>()
            };
            Result Chelsea_vs_MC = new Result()
            {
                Id = 6,
                Team = Chelsea,
                Score = 0,
                SportEvent = Chelsea_vs_MC_event
            };
            Result MC_vs_Chelsea = new Result()
            {
                Id = 5,
                Team = MC,
                Score = 0,
                SportEvent = Chelsea_vs_MC_event
            };

            SportEvent Chelsea_vs_Milano_event = new SportEvent()
            {
                Id = 8,
                Date = new System.DateTime(2016, 11, 3, 16, 0, 0),
                SportType = football,
                Results = new List<Result>()
            };
            Result Chelsea_vs_Milano = new Result()
            {
                Id = 7,
                Team = Chelsea,
                Score = 2,
                SportEvent = Chelsea_vs_Milano_event
            };
            Result Milano_vs_Chelsea = new Result()
            {
                Id = 3,
                Team = Milano,
                Score = 3,
                SportEvent = Chelsea_vs_Milano_event
            };

            // Init Results
            Result Ch_vs_LA = new Result()
            {
                Id = 9,
                Score = 68,
                Team = ChicagoBulls,
                SportEvent = ChicagoBulls_vs_LA_event
            };
            Result LA_vs_Ch = new Result()
            {
                Id = 11,
                Score = 65,
                Team = LALakers,
                SportEvent = ChicagoBulls_vs_LA_event
            };

            Result LA_vs_Ph = new Result()
            {
                Id = 12,
                Score = 65,
                Team = LALakers,
                SportEvent = LA_vs_Suns_event
            };
            Result Ph_vs_LA = new Result()
            {
                Id = 13,
                Score = 64,
                Team = PhoenixSuns,
                SportEvent = LA_vs_Suns_event
            };

            Result Ch_vs_Ph = new Result()
            {
                Id = 10,
                Score = 52,
                Team = ChicagoBulls,
                SportEvent = ChicagoBulls_vs_Suns_event
            };
            Result Ph_vs_Ch = new Result()
            {
                Id = 14,
                Score = 52,
                Team = PhoenixSuns,
                SportEvent = ChicagoBulls_vs_Suns_event
            };
            Result MU_vs_MC = new Result()
            {
                Id = 1,
                Score = 2,
                Team = MU,
                SportEvent = MU_vs_MC_event
            };
            Result MC_vs_MU = new Result()
            {
                Id = 4,
                Score = 1,
                Team = MC,
                SportEvent = MU_vs_MC_event
            };

            this.sportTypes.Add(football);
            this.sportTypes.Add(basketball);
            this.sportTypes.Add(tennis);
            this.sportTypes.Add(snooker);


            this.teams.Add(MU);
            this.teams.Add(Milano);
            this.teams.Add(MC);
            this.teams.Add(Chelsea);
            this.teams.Add(Bayern);
            this.teams.Add(ChicagoBulls);
            this.teams.Add(LALakers);
            this.teams.Add(PhoenixSuns);

            this.sportEvents.Add(ChicagoBulls_vs_LA_event);
            this.sportEvents.Add(ChicagoBulls_vs_Suns_event);
            this.sportEvents.Add(LA_vs_Suns_event);
            this.sportEvents.Add(Bayern_vs_Milano_event);
            this.sportEvents.Add(MU_vs_MC_event);
            this.sportEvents.Add(Chelsea_vs_Milano_event);
            this.sportEvents.Add(Chelsea_vs_MC_event);

            this.results.Add(MU_vs_MC);
            this.results.Add(Milano_vs_Bayern);
            this.results.Add(Milano_vs_Chelsea);
            this.results.Add(MC_vs_MU);
            this.results.Add(MC_vs_Chelsea);
            this.results.Add(Chelsea_vs_MC);
            this.results.Add(Chelsea_vs_Milano);
            this.results.Add(Bayern_vs_Milano);
            this.results.Add(Ch_vs_LA);
            this.results.Add(Ch_vs_Ph);
            this.results.Add(LA_vs_Ch);
            this.results.Add(LA_vs_Ph);
            this.results.Add(Ph_vs_LA);
            this.results.Add(Ph_vs_Ch);

            MU.Results.Add(MU_vs_MC);
            Milano.Results.Add(Milano_vs_Bayern);
            Milano.Results.Add(Milano_vs_Chelsea);
            MC.Results.Add(MC_vs_Chelsea);
            MC.Results.Add(MC_vs_MU);
            Chelsea.Results.Add(Chelsea_vs_MC);
            Chelsea.Results.Add(Chelsea_vs_Milano);
            Bayern.Results.Add(Bayern_vs_Milano);
            ChicagoBulls.Results.Add(Ch_vs_LA);
            ChicagoBulls.Results.Add(Ch_vs_Ph);
            LALakers.Results.Add(LA_vs_Ch);
            LALakers.Results.Add(LA_vs_Ph);
            PhoenixSuns.Results.Add(Ph_vs_Ch);
            PhoenixSuns.Results.Add(Ph_vs_LA);

            ChicagoBulls_vs_LA_event.Results.Add(Ch_vs_LA);
            ChicagoBulls_vs_LA_event.Results.Add(LA_vs_Ch);
            LA_vs_Suns_event.Results.Add(LA_vs_Ph);
            LA_vs_Suns_event.Results.Add(Ph_vs_LA);
            ChicagoBulls_vs_Suns_event.Results.Add(Ch_vs_Ph);
            ChicagoBulls_vs_Suns_event.Results.Add(Ph_vs_Ch);
            Bayern_vs_Milano_event.Results.Add(Bayern_vs_Milano);
            Bayern_vs_Milano_event.Results.Add(Milano_vs_Bayern);
            MU_vs_MC_event.Results.Add(MU_vs_MC);
            MU_vs_MC_event.Results.Add(MC_vs_MU);
            Chelsea_vs_Milano_event.Results.Add(Chelsea_vs_Milano);
            Chelsea_vs_Milano_event.Results.Add(Milano_vs_Chelsea);
            Chelsea_vs_MC_event.Results.Add(Chelsea_vs_MC);
            Chelsea_vs_MC_event.Results.Add(MC_vs_Chelsea);

            //ResultDto formation
            ResultDto ChicagoBulls_vs_LA_Result_Dto = new ResultDto()
            {
                Date = new EventDate(ChicagoBulls_vs_LA_event.Date).ToString(),
                Id = Ch_vs_LA.Id,
                Score = String.Format("{0} : {1}", Ch_vs_LA.Score, LA_vs_Ch.Score),
                TeamIds = new List<int>()
                {
                    ChicagoBulls.Id,
                    LALakers.Id
                },
                TeamNames = new List<string>()
                {
                    ChicagoBulls.Name,
                    LALakers.Name
                }
            };
            ResultDto LA_vs_ChicagoBulls_Result_Dto = new ResultDto()
            {
                Date = new EventDate(LA_vs_Suns_event.Date).ToString(),
                Id = LA_vs_Ch.Id,
                Score = String.Format("{0} : {1}", LA_vs_Ch.Score, Ch_vs_LA.Score),
                TeamIds = new List<int>()
                {
                    LALakers.Id,
                    ChicagoBulls.Id
                },
                TeamNames = new List<string>()
                {
                    LALakers.Name,
                    ChicagoBulls.Name
                }
            };
            ResultDto Suns_vs_ChicagoBulls_Result_Dto = new ResultDto()
            {
                Date = new EventDate(ChicagoBulls_vs_Suns_event.Date).ToString(),
                Id = Ph_vs_Ch.Id,
                Score = String.Format("{0} : {1}", Ph_vs_Ch.Score, Ch_vs_Ph.Score),
                TeamIds = new List<int>()
                {
                    PhoenixSuns.Id,
                    ChicagoBulls.Id
                },
                TeamNames = new List<string>()
                {
                    PhoenixSuns.Name,
                    ChicagoBulls.Name
                }            
            };
            ResultDto ChicagoBulls_vs_Suns_Result_Dto = new ResultDto()
            {
                Date = new EventDate(ChicagoBulls_vs_Suns_event.Date).ToString(),
                Id = Ch_vs_Ph.Id,
                Score = String.Format("{0} : {1}", Ch_vs_Ph.Score, Ph_vs_Ch.Score),
                TeamIds = new List<int>()
                {
                    ChicagoBulls.Id,
                    PhoenixSuns.Id
                },
                TeamNames = new List<string>()
                {
                    
                    ChicagoBulls.Name,
                    PhoenixSuns.Name
                } 
            };
            ResultDto Suns_vs_LA_Result_Dto = new ResultDto()
            {
                Date = new EventDate(LA_vs_Suns_event.Date).ToString(),
                Id = Ph_vs_LA.Id,
                Score = String.Format("{0} : {1}", Ph_vs_LA.Score, LA_vs_Ph.Score),
                TeamIds = new List<int>()
                {
                    PhoenixSuns.Id,
                    LALakers.Id
                },
                TeamNames = new List<string>()
                {
                    PhoenixSuns.Name,
                    LALakers.Name
                }
            };
            ResultDto LA_vs_Suns_Result_Dto = new ResultDto()
            {
                Date = new EventDate(LA_vs_Suns_event.Date).ToString(),
                Id = LA_vs_Ph.Id,
                Score = String.Format("{0} : {1}", LA_vs_Ph.Score, Ph_vs_LA.Score),
                TeamIds = new List<int>()
                {
                    LALakers.Id,
                    PhoenixSuns.Id
                },
                TeamNames = new List<string>()
                {
                    LALakers.Name,
                    PhoenixSuns.Name
                }
            };
            List<ResultDto> Ch_Results_dto = new List<ResultDto>()
            {
                ChicagoBulls_vs_LA_Result_Dto,
                ChicagoBulls_vs_Suns_Result_Dto
            };
            List<ResultDto> LA_Results_dto = new List<ResultDto>()
            {
                LA_vs_ChicagoBulls_Result_Dto,
                LA_vs_Suns_Result_Dto
            };
            List<ResultDto> Ph_Results_dto = new List<ResultDto>()
            {
                Suns_vs_ChicagoBulls_Result_Dto,
                Suns_vs_LA_Result_Dto
            };
            resultsDtoCollection = new List<List<ResultDto>>
            {
                Ch_Results_dto,
                LA_Results_dto,
                Ph_Results_dto
            };
            resultsDto = new List<ResultDto>()
            {
                ChicagoBulls_vs_LA_Result_Dto,
                LA_vs_ChicagoBulls_Result_Dto,
                Suns_vs_ChicagoBulls_Result_Dto,
                ChicagoBulls_vs_Suns_Result_Dto,
                Suns_vs_LA_Result_Dto,
                LA_vs_Suns_Result_Dto
            };
            testResults = new List<Result>()
            {
                Ch_vs_LA,
                LA_vs_Ch,
                Ph_vs_Ch,
                Ch_vs_Ph,
                Ph_vs_LA,
                LA_vs_Ph
            };
        }

        [Test]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 2)]
        [TestCase(4, 2)]
        [TestCase(5, 1)]
        [TestCase(6, 2)]
        [TestCase(7, 2)]
        [TestCase(8, 2)]
        public void TeamResultsCount(int teamId, int result)
        {
            int count = this.teamDetailsService.Count(teamId);

            Assert.AreEqual(result, count); 
        }

        [Test]
        [TestCase(6, 0)]
        [TestCase(7, 1)]
        [TestCase(8, 2)]
        public void GetTeamResults(int teamId, int element)
        {
            List<ResultDto> resultDtoCollection = new List<ResultDto>();
            List<ResultDto> dtoTest = this.resultsDtoCollection.ElementAt(element);
            ResultDtoComparer dtoComparer = new ResultDtoComparer();
            int skip = 0; int take = 10;
            resultDtoCollection = this.teamDetailsService.GetResults(teamId, skip, take).ToList();

            CollectionAssert.AreEqual(resultDtoCollection, dtoTest, dtoComparer);
        }

        [Test]
        public void ResultToResultDtoMapping()
        {
            ResultDtoComparer dtoComparer = new ResultDtoComparer();
          
            List<ResultDto> resultsDto = this.resultsDto;
            List<ResultDto> mappedDto = new List<ResultDto>();
            List<Result> testResults = this.testResults;

            foreach (Result result in testResults)
            {
                mappedDto.Add(Mapper.Map<Result, ResultDto>(result));
            }
                 
            CollectionAssert.AreEqual(resultsDto, mappedDto, dtoComparer);
        }

        [Test]
        public void TeamToTeamDtoMapping()
        {
            List<Team> teams = this.teams;
            List<TeamDto> teamsTestDto = new List<TeamDto>();
            List<TeamDto>mappedDto = new List<TeamDto>();
            TeamDtoComparer dtoComparer = new TeamDtoComparer();

            foreach (Team team in teams)
            {
                teamsTestDto.Add(new TeamDto
                {
                    Name = team.Name,
                    Id = team.Id
                });
                mappedDto.Add(Mapper.Map<Team, TeamDto>(team));
            }

            CollectionAssert.AreEqual(teamsTestDto, mappedDto, dtoComparer);
        }

        public class ResultDtoComparer : IComparer, IComparer<ResultDto>
        {
            public int Compare(object x, object y)
            {
                var ldto = x as ResultDto;
                var rdto = y as ResultDto;
                if (ldto == null || rdto == null)
                    throw new InvalidOperationException();
                return this.Compare(ldto, rdto);
            }

            public int Compare(ResultDto ldto, ResultDto rdto)
            {
                if (ldto.Date == null || string.IsNullOrEmpty(ldto.Score) || ldto.Id < 1 || ldto.TeamIds.ElementAt(0) < 1 || ldto.TeamIds.ElementAt(1) < 1 ||
                string.IsNullOrEmpty(ldto.TeamNames.ElementAt(0)) || string.IsNullOrEmpty(ldto.TeamNames.ElementAt(1)))
                {
                    return -1;
                }
                return ((ldto.Date.CompareTo(rdto.Date)) & (ldto.Score.CompareTo(rdto.Score)) &
                    (ldto.Id.CompareTo(rdto.Id)) & (ldto.TeamIds.ElementAt(0).CompareTo(rdto.TeamIds.ElementAt(0))) &
                    (ldto.TeamIds.ElementAt(1).CompareTo(rdto.TeamIds.ElementAt(1))) & (ldto.TeamNames.ElementAt(0).CompareTo(rdto.TeamNames.ElementAt(0))) &
                    (ldto.TeamNames.ElementAt(1).CompareTo(rdto.TeamNames.ElementAt(1))));
            }
        }
        public class TeamDtoComparer : IComparer, IComparer<TeamDto>
        {
            public int Compare(object x, object y)
            {
                var ldto = x as TeamDto;
                var rdto = y as TeamDto;
                if (ldto == null || rdto == null)
                    throw new InvalidOperationException();
                return this.Compare(ldto, rdto);
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
    }
}
