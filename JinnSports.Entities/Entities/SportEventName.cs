using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinnSports.Entities.Entities
{
    public class SportEventName
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public SportEvent SportEvent { get; set; }
    }
}
