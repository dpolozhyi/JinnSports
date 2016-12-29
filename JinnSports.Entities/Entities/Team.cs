using JinnSports.Entities.Entities.Temp;
using System.Collections.Generic;

namespace JinnSports.Entities.Entities
{
    public class Team
    {
        public int Id { get; set; }
        
        public virtual SportType SportType { get; set; }

        public string Name { get; set; }

        public ICollection<TeamName> Names { get; set; }

        public virtual ICollection<Result> Results { get; set; }

        public virtual ICollection<TempResult> TempResults { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}; Name: {Name}; SportType: {SportType.Name}";
        }
    }
}
