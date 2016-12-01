using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinnSports.Parser.App.JsonParserService.JsonEntities
{
    public class JsonResult
    {
        public int Id { get; set; }

        public IEnumerable<Sport> Sports { get; set; }

        public IEnumerable<Section> Sections { get; set; }

        public IEnumerable<Event> Events { get; set; }
    }
}
