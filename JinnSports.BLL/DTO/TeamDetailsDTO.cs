using System.Collections.Generic;

namespace JinnSports.BLL.DTO
{
    public class TeamDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ResultDTO> Results { get; set; }
    }
}
