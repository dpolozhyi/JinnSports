using System.Collections.Generic;
using JinnSports.BLL.Dtos;
using DTO.JSON;

namespace JinnSports.BLL.Interfaces
{
    public interface IEventService
    {
        /// <summary>
        /// Метод подсчёта количества результатов в заданном виде спорта
        /// </summary>
        /// <param name="sport"></param>
        /// <returns></returns>                        
        int Count(string sport);
        
        /// <summary>
        /// Метод выдачи результатов по заданному виду спорта
        /// </summary>
        /// <param name="sport"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        IEnumerable<ResultDto> GetSportEvents(string sport, int skip, int take);

        bool SaveSportEvents(ICollection<SportEventDTO> events);
    }
}
