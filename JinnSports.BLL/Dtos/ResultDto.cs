using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JinnSports.BLL.Extentions;

namespace JinnSports.BLL.Dtos
{
    public class ResultDto
    {
        public int Id { get; set; }
        public string Score { get; set; }
        public string Date { get; set; }
        public IEnumerable<string> TeamNames { get; set; } = new List<string>(2);
        public IEnumerable<int> TeamIds { get; set; } = new List<int>(2);
    }
}
