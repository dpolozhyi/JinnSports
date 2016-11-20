using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinnSports.BLL.DTO
{
    class Team
    {
        public int Id { get; set; }
        public SportType SportType { get; set; }
        public List<CompetitionEvent> CompetitionEvent { get; set; }
    }
}
