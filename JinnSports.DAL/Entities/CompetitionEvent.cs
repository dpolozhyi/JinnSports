using System;

namespace JinnSports.DAL.Entities
{
    public class CompetitionEvent
    {
        public int Id { get; set; }
        public Result Result { get; set; }
        public DateTime Date { get; set; }
    }
}
