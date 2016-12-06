using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JinnSports.BLL.Interfaces;
using JinnSports.DataAccessInterfaces;
using JinnSports.DAL.Repositories;
using JinnSports.Entities;
using JinnSports.BLL.DTO;
using JinnSports.DataAccessInterfaces.Interfaces;

namespace JinnSports.BLL.Service
{
    public class EventsService : IEventService
    {
        private IUnitOfWork dataUnit;

        public IList<CompetitionEventDTO> GetCEvents()
        {
            IList<CompetitionEventDTO> events = new List<CompetitionEventDTO>();
            string competitionEventResult = string.Empty;

            dataUnit = new EFUnitOfWork("SportsContext");

            IRepository<CompetitionEvent> eventsRepository = dataUnit.Set<CompetitionEvent>();
            IRepository<Team> teams = dataUnit.Set<Team>();
            IRepository<Result> results = dataUnit.Set<Result>();

            IEnumerable<CompetitionEvent> competitionEvents = eventsRepository.GetAll();
            foreach (CompetitionEvent ce in competitionEvents)
            {
                CompetitionEventDTO competitionEvent = new CompetitionEventDTO();
                competitionEvent.Date = ce.Date;

                var datedResults = results.GetAll(r => r.CompetitionEvent.Id == ce.Id);

                competitionEventResult = string.Empty;

                foreach (Result res in datedResults)
                {
                    Team selectedTeam = teams.Get(t => t.Id == res.Team.Id);
                    if (!competitionEvent.SportType.Any())
                    {
                        competitionEvent.SportType = selectedTeam.SportType.ToString();
                    }

                    if (!competitionEvent.Team1.Any())
                    {
                        competitionEvent.Team1 = res.Team.Name;
                    }
                    else
                    {
                        competitionEvent.Team2 = res.Team.Name;
                    }

                    if (!competitionEvent.Result1.Any())
                    {
                        competitionEvent.Result1 = res.Score;
                    }
                    else
                    {
                        competitionEvent.Result2 = res.Score;
                    }


                }
                events.Add(competitionEvent);
            }

            return events;
        }

        public void Dispose()
        {
            dataUnit.Dispose();
        }

    }
}
