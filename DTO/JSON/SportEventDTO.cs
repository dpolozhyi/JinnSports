using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DTO.JSON
{
    [DataContract]
    public class SportEventDTO
    {
        [DataMember]
        public string Date { get; set; }

        [DataMember]
        public string SportType { get; set; }

        [DataMember]
        public ICollection<ResultDTO> Results { get; set; } = new List<ResultDTO>();
    }
}
