using System;
using System.Collections;
using System.Collections.Generic;

namespace JinnSports.BLL.Dtos
{
    public class SportEventDto
    {
        public SportEventDto()
        {
            this.Results = new Dictionary<string, int>();
        }

        public string SportType
        {
            get; set;
        }
        public DateTime Date
        {
            get; set;
        }
        public IDictionary<string, int> Results
        {
            get; set;
        }
    }
}
