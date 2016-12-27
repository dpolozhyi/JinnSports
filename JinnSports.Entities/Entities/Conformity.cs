using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinnSports.Entities.Entities
{
    public class Conformity
    {
        public int Id { get; set; }

        public string InputTeamName { get; set; }

        public Team ExistedTeam { get; set; }

        public bool IsConfirmed { get; set; }
    }
}
