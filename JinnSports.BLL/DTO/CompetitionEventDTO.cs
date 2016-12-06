using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinnSports.BLL.DTO
{
    public class CompetitionEventDTO
    {
        public string SportType
        {
            get; set;
        }
        public DateTime Date
        {
            get; set;
        }
        public string Result
        {
            get; set;
        }
    }
}
