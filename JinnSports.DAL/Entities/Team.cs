using System;
using System.Collections.Generic;

namespace JinnSports.DAL.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public SportType SportType { get; set; }
        public List<CompetitionEvent> CompetitionEvent { get; set; }
    }
}
