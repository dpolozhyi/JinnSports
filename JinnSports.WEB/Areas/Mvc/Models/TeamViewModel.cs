using JinnSports.BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JinnSports.WEB.Areas.Mvc.Models
{
    public class TeamViewModel
    {
        public IEnumerable<TeamDto> TeamDtos { get; set; }

        public PageInfo PageInfo { get; set; }
    }
}