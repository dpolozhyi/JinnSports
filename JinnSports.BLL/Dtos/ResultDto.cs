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

        /*public override int GetHashCode()
        {
            if (Date == null || SportType == null || SportType.Name == null)
            {
                return 0;
            }

            int hashCode = Date.GetHashCode() ^ SportType.Name.GetHashCode();
            List<string> teamNames = Results.Select(r => r.Team.Name).ToList();

            foreach (string teamName in teamNames)
            {
                hashCode ^= teamName.GetHashCode();
            }

            return hashCode;
        }*/
    }
}
