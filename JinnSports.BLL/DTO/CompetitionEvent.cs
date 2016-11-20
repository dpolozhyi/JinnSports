using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinnSports.BLL.DTO
{
    class CompetitionEvent
    {
        public int Id { get; set; }
        public Team FirstTeam { get; set; }
        public Team SecondTeam { get; set; }
        public Result Result { get; set; }
        public DateTime Date { get; set; }
    }
}
