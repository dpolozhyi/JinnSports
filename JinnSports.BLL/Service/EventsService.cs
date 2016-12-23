using System.Collections.Generic;
using JinnSports.BLL.Dtos;
using JinnSports.BLL.Interfaces;
using JinnSports.DAL.Repositories;
using JinnSports.DataAccessInterfaces.Interfaces;
using JinnSports.Entities.Entities;
using System.Linq;
using AutoMapper;
using DTO.JSON;

namespace JinnSports.BLL.Service
{
    public class EventsService : IEventService
    {
        private const string SPORTCONTEXT = "SportsContext";

        private IUnitOfWork dataUnit;

        public int Count(string sport)
        {
            int count;
            using (this.dataUnit = new EFUnitOfWork(SPORTCONTEXT))
            {
                count = this.dataUnit.GetRepository<SportEvent>()
                    .Get(filter: m => m.SportType.Name == sport)
                    .Count();
            }
            return count;
        }

        public IEnumerable<SportEventDto> GetSportEvents(string sport, int skip, int take)
        {
            IList<SportEventDto> results = new List<SportEventDto>();

            using (this.dataUnit = new EFUnitOfWork(SPORTCONTEXT))
            {
                // Выбираем результаты заданного вида спорта, сортируя по дате
                IEnumerable<SportEvent> sportEvents =
                    this.dataUnit.GetRepository<SportEvent>().Get(
                        filter: m => m.SportType.Name == sport,
                        orderBy: s => s.OrderByDescending(x => x.Date),
                        skip: skip,
                        take: take);

                // Формирование SportEventDto из SportEvent при помощи AutoMapper
                foreach (SportEvent sportEvent in sportEvents)
                {
                    results.Add(Mapper.Map<SportEventDto>(sportEvent));
                }
            }

            return results;
        }

       public bool SaveSportEvents(ICollection<SportEventDTO> events)
        {
            return false;
        }
    }
}
