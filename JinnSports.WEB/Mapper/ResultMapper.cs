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
            return new ResultViewModel { Result = compEvent.Result, Date = compEvent.Date.ToShortDateString() };
        }
    }
}