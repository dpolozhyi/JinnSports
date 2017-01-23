using AutoMapper;
using JinnSports.BLL.Dtos;
using JinnSports.BLL.Dtos.SportType;
using JinnSports.BLL.Interfaces;
using JinnSports.DataAccessInterfaces.Interfaces;
using JinnSports.Entities.Entities;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JinnSports.BLL.Service
{
    public class SportTypeService : ISportTypeService
    {
        private const string SPORTCONTEXT = "SportsContext";

        private static readonly ILog Log = LogManager.GetLogger(typeof(EventsService));

        private readonly IUnitOfWork dataUnit;

        public SportTypeService(IUnitOfWork unitOfWork)
        {
            this.dataUnit = unitOfWork;
        }
        public int Count(int sportTypeId)
        {
            int count;
            if (sportTypeId != 0)
            {
                count = this.dataUnit.GetRepository<SportEvent>()
                .Get(filter: m => m.SportType.Id == sportTypeId)
                .Count();
            }
            else
            {
                count = this.dataUnit.GetRepository<SportEvent>()
                .Get().Count();
            }
            return count;
        }

        public SportTypeSelectDto GetSportTypes(int sportTypeId, int time)
        {
            IList<ResultDto> results = new List<ResultDto>();
            IList<SportTypeListDto> sportTypeListDtos = new List<SportTypeListDto>();
            ICollection<SportEvent> sportEvents = new List<SportEvent>();
            string selectedName;

            Expression<Func<SportEvent, bool>> filter = null;
            Func<IQueryable<SportEvent>, IOrderedQueryable<SportEvent>> orderBy = null;

            if (sportTypeId != 0)
            {
                if (time != 0)
                {
                    filter = x => x.SportType.Id == sportTypeId && DateTime.Compare(x.Date, DateTime.UtcNow) == time;
                }
                else
                {
                    filter = x => x.SportType.Id == sportTypeId;
                }
            }
            else
            {
                if (time != 0)
                {
                    filter = x => DateTime.Compare(x.Date, DateTime.UtcNow) == time;
                }
            }

            if (time == 1)
            {
                orderBy = x => x.OrderBy(m => m.Date).ThenByDescending(m => m.Id);
            }
            else
            {
                orderBy = x => x.OrderByDescending(m => m.Date).ThenByDescending(m => m.Id);
            }

            sportEvents = this.dataUnit.GetRepository<SportEvent>().Get(filter: filter, orderBy: orderBy).ToList();


            if (sportTypeId != 0)
            {
                if (sportEvents.Count() > 0)
                {
                    results = Mapper.Map<List<SportEvent>, List<ResultDto>>(sportEvents.ToList());
                    sportTypeListDtos.Add(new SportTypeListDto
                    {
                        SportType = Mapper.Map<SportType, SportTypeDto>(sportEvents.FirstOrDefault().SportType),
                        Results = results
                    });
                }
            }
            else
            {
                IEnumerable<SportType> sportTypes = this.dataUnit.GetRepository<SportType>().Get();

                foreach (SportType sportType in sportTypes)
                {
                    if (sportEvents.Count() > 0)
                    {
                        foreach (SportEvent sportEvent in sportEvents)
                        {
                            results.Add(Mapper.Map<SportEvent, ResultDto>(sportEvent));
                        }

                        results = Mapper.Map<List<SportEvent>, List<ResultDto>>(sportEvents.ToList());

                        sportTypeListDtos.Add(new SportTypeListDto
                        {
                            SportType = Mapper.Map<SportType, SportTypeDto>(sportType),
                            Results = Mapper.Map<List<SportEvent>, List<ResultDto>>(sportEvents.ToList())
                        });

                        results = new List<ResultDto>();
                    }
                }
            }

            if (sportTypeId == 0)
            {
                selectedName = "Sport Events";
            }
            else
            {
                selectedName = sportEvents.FirstOrDefault().SportType.Name;
            }

            SportTypeSelectDto sportTypeModel = new SportTypeSelectDto()
            {
                SelectedId = sportTypeId,
                SelectedName = selectedName,
                SportTypes = this.GetAllSportTypes(),
                SportTypeResults = sportTypeListDtos
            };
            return sportTypeModel;
        }

        public IEnumerable<SportTypeDto> GetAllSportTypes()
        {
            IEnumerable<SportType> sportTypes = this.dataUnit.GetRepository<SportType>().Get();
            IList<SportTypeDto> sportTypesDto = new List<SportTypeDto>();

            foreach (SportType sportType in sportTypes)
            {
                sportTypesDto.Add(Mapper.Map<SportType, SportTypeDto>(sportType));
            }
            return sportTypesDto;
        }
    }
}
