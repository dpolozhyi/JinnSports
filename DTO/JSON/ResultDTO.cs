using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DTO.JSON
{
    [DataContract]
    public class ResultDTO
    {
        [DataMember]
        public string TeamName { get; set; }

        [DataMember]
        public int Score { get; set; }

    }
}
