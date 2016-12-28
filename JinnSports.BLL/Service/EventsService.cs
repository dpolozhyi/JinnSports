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

        private readonly IUnitOfWork dataUnit;        

        public EventsService(IUnitOfWork unitOfWork)
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

        public IEnumerable<ResultDto> GetSportEvents(int sportTypeId, int skip, int take)
        {
            IList<ResultDto> results = new List<ResultDto>();

                IEnumerable<SportEvent> sportEvents;
                if (sportTypeId != 0)
                {
                    sportEvents =
                        this.dataUnit.GetRepository<SportEvent>().Get(
                        filter: m => m.SportType.Id == sportTypeId,
                        orderBy: s => s.OrderByDescending(x => x.Date).ThenByDescending(x => x.Id),
                        skip: skip,
                        take: take);
                }
                else
                {
                    sportEvents =
                        this.dataUnit.GetRepository<SportEvent>().Get(
                        orderBy: s => s.OrderByDescending(x => x.Date).ThenByDescending(x => x.Id),
                        skip: skip,
                        take: take);
                }                
                foreach (SportEvent sportEvent in sportEvents)
                {
                    results.Add(Mapper.Map<SportEvent, ResultDto>(sportEvent));
                }
            return results;
        }

        public IEnumerable<SportTypeDto> GetSportTypes()
        {
            IList<SportTypeDto> sportTypeDto = new List<SportTypeDto>();
          
                IEnumerable<SportType> sportTypes =
                    this.dataUnit.GetRepository<SportType>().Get();
                
                foreach (SportType sportType in sportTypes)
                {
                    sportTypeDto.Add(Mapper.Map<SportType, SportTypeDto>(sportType));
                }
            return sportTypeDto;
        }

        public bool SaveSportEvents(ICollection<SportEventDTO> eventDTOs)
        {
            Log.Info("Writing transferred data...");
            try
            {
                Log.Info("Writing transferred data...");                
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
