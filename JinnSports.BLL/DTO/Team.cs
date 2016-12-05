using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinnSports.BLL.DTO
{
    public class Team
    {
        public int Id { get; set; }
        public virtual SportType SportType { get; set; }
        public string Name { get; set; }

        public List<Result> Results { get; set; }
    }
}
