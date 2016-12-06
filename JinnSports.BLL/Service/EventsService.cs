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

            IList<CompetitionEvent> competitionEvents = eventsRepository.GetAll();
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

                    if (!competitionEventResult.Any())
                    {
                        competitionEventResult = selectedTeam.Name + " " + res.Score;
                    } 
                    else
                    {
                        competitionEventResult += " : " + res.Score + " " + selectedTeam.Name;
                    }
                }
                competitionEvent.Result = competitionEventResult;
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
