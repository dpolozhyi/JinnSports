using System.Collections.Generic;
using JinnSports.BLL.Dtos;
using JinnSports.BLL.Interfaces;
using JinnSports.BLL.Matcher;
using JinnSports.DAL.Repositories;
using JinnSports.DataAccessInterfaces.Interfaces;
using JinnSports.Entities.Entities;
using System.Linq;
using AutoMapper;
using DTO.JSON;
using System;
using log4net;
using JinnSports.Entities.Entities.Temp;

namespace JinnSports.BLL.Service
{
    public class EventsService : IEventService
    {
        private const string SPORTCONTEXT = "SportsContext";

        private static readonly ILog Log = LogManager.GetLogger(typeof(EventsService));        

        private IUnitOfWork dataUnit;

        public int Count(int sportId)
        {
            int count;
            using (this.dataUnit = new EFUnitOfWork(SPORTCONTEXT))
            {
                count = this.dataUnit.GetRepository<SportEvent>()
                    .Get(filter: m => m.SportType.Id == sportId)
                    .Count();
            }
            return count;
        }

        public IEnumerable<ResultDto> GetSportEvents(int sportId, int skip, int take)
        {
            IList<ResultDto> results = new List<ResultDto>();

            using (this.dataUnit = new EFUnitOfWork(SPORTCONTEXT))
            {
                // Выбираем результаты заданного вида спорта, сортируя по дате
                IEnumerable<SportEvent> sportEvents =
                    this.dataUnit.GetRepository<SportEvent>().Get(
                        filter: m => m.SportType.Id == sportId,
                        orderBy: s => s.OrderByDescending(x => x.Date),
                        skip: skip,
                        take: take);

                // Формирование SportEventDto из SportEvent при помощи AutoMapper
                foreach (SportEvent sportEvent in sportEvents)
                {
                    results.Add(Mapper.Map<SportEvent, ResultDto>(sportEvent));
                }
            }

            return results;
        }

        public bool SaveSportEvents(ICollection<SportEventDTO> eventDTOs)
        {
            try
            {
                Log.Info("Writing transferred data...");
                this.dataUnit = new EFUnitOfWork(SPORTCONTEXT);
                NamingMatcher matcher = new NamingMatcher(this.dataUnit);

                IEnumerable<SportType> sportTypes = this.dataUnit.GetRepository<SportType>().Get();
                IEnumerable<SportEvent> exustingEvents = this.dataUnit.GetRepository<SportEvent>().Get();

                foreach (SportEventDTO eventDTO in eventDTOs)
                {
                    SportType sportType = sportTypes.FirstOrDefault(st => st.Name == eventDTO.SportType) 
                                            ?? new SportType { Name = eventDTO.SportType };
                    
                    bool isConflictExist = false;
                    List<Result> results = new List<Result>();
                    List<TempResult> tempResults = new List<TempResult>();

                    foreach (ResultDTO resultDTO in eventDTO.Results)
                    {
                        List<Conformity> conformities = new List<Conformity>();

                        Team team = new Team { Name = resultDTO.TeamName, SportType = sportType };
                        Team resolvedTeam = matcher.ResolveNaming(team, out conformities);

                        if (resolvedTeam != null)
                        {
                            Result result = new Result { Team = team, Score = resultDTO.Score };
                            results.Add(result);                            
                        }
                        else
                        {
                            isConflictExist = true;

                            TempResult result = new TempResult { Team = team, Score = resultDTO.Score };
                            
                            foreach (Conformity conformity in conformities)
                            {
                                result.Conformities.Add(conformity);
                            }
                            conformities.Clear();
                            tempResults.Add(result);                            
                        }                        
                    }

                    if (isConflictExist)
                    {
                        TempSportEvent tempSportEvent = new TempSportEvent { SportType = sportType, Date = new DateTime(eventDTO.Date) };
                        foreach (TempResult tempResult in tempResults)
                        {
                            tempSportEvent.TempResults.Add(tempResult);
                        }
                        foreach (Result result in results)
                        {
                            TempResult tempRes = new TempResult { Team = result.Team, Score = result.Score, IsHome = result.IsHome };
                            tempSportEvent.TempResults.Add(tempRes);
                        }
                        this.dataUnit.GetRepository<TempSportEvent>().Insert(tempSportEvent);
                    }
                    else
                    {
                        SportEvent sportEvent = new SportEvent { SportType = sportType, Date = new DateTime(eventDTO.Date) };
                        foreach (Result result in results)
                        {
                            sportEvent.Results.Add(result);
                        }
                        if (!exustingEvents.Contains(sportEvent))
                        {
                            this.dataUnit.GetRepository<SportEvent>().Insert(sportEvent); 
                        }
                    }
                }
                this.dataUnit.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error("Exception when trying to save transferred data to DB", ex);
                return false;
            }
            finally
            {
                if (this.dataUnit != null)
                {
                    this.dataUnit.Dispose();
                }
            }
            Log.Info("Transferred data sucessfully saved");
            return true;
        }        
    }
}
