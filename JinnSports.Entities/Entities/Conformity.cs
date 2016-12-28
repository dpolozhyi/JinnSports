using JinnSports.Entities.Entities.Temp;
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

        public string InputName { get; set; }

        public string ExistedName { get; set; }

        public bool IsConfirmed { get; set; }

        public TempResult TempResult { get; set; }
    }
}
