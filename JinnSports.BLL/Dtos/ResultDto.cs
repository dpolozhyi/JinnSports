using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinnSports.BLL.Dtos
{
    public class ResultDto
    {
        public int Id { get; set; }
        public string Score { get; set; }
        public DateTime Date { get; set; }
        public int TeamFirstId { get; set; }
        public string TeamFirst { get; set; }
        public int TeamSecondId { get; set; }
        public string TeamSecond { get; set; }
    }
}
