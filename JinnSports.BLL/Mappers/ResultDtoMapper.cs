using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JinnSports.BLL.DTO;
using JinnSports.Entities.Entities;

namespace JinnSports.BLL.Mappers
{
    internal static class ResultDtoMapper
    {
        public static ResultDTO MapToResultDto(this Result result, Result result2)
        {
            return new ResultDTO
            {
                Date = result.CompetitionEvent.Date.ToShortDateString(),
                Score = $"{result.Score}:{result2.Score}",
                Team1 = result.Team.Name,
                Team2 = result2.Team.Name,
                Team1Id = result.Team.Id,
                Team2Id = result2.Team.Id
            };
        }
    }
}
