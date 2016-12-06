using JinnSports.BLL.DTO;
using JinnSports.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JinnSports.WEB.Mapper
{
    internal static class ResultMapper
    {
        public static ResultViewModel MapToViewModel(this CompetitionEventDTO compEvent)
        {
            return new ResultViewModel  {
                Result = string.Concat(compEvent.Team1, " ", 
                compEvent.Result1, ":", compEvent.Result2, " ", compEvent.Team2),
                Date = compEvent.Date.ToShortDateString()
            };
        }
    }
}