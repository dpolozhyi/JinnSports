using System.Collections.Generic;
using JinnSports.BLL.Dtos;
using JinnSports.BLL.Interfaces;
using JinnSports.DAL.Repositories;
using JinnSports.DataAccessInterfaces.Interfaces;
using JinnSports.Entities.Entities;
using System.Linq;
using AutoMapper;

namespace JinnSports.BLL.Service
{
    public class EventsService : IEventService
    {
        private IUnitOfWork dataUnit; 
        
        public IDictionary<string, List<SportEventDto>> GetSportEvents()
        {
            IDictionary<string, List<SportEventDto>> orderedEvents = new Dictionary<string, List<SportEventDto>>();
            
            using (this.dataUnit = new EFUnitOfWork("SportsContext"))
            {
                IEnumerable<SportEvent> sportEvents = this.dataUnit.GetRepository<SportEvent>().Get();
                IEnumerable<SportType> sportTypes = this.dataUnit.GetRepository<SportType>().Get();

                foreach (SportType sportType in sportTypes)
                {
                    orderedEvents.Add(sportType.Name, new List<SportEventDto>());
                }

                foreach (SportEvent sportEvent in sportEvents)
                {
                    orderedEvents[sportEvent.SportType.Name].Add(Mapper.Map<SportEvent, SportEventDto>(sportEvent));
                }
            }

            return orderedEvents;
        }

        public void SortEventsByDate(IDictionary<string, List<SportEventDto>> orderedEvents)
        {
            ICollection<List<SportEventDto>> eventsLists = orderedEvents.Values;
            foreach (List<SportEventDto> eventsList in eventsLists)
            {
                eventsList.OrderBy(e => e.Date);
            }
        }
    }
}
