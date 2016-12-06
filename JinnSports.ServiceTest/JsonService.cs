using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JinnSports.Parser.Service;

namespace JinnSports.ServiceTest
{
    public class JsonService:JsonParserService
    {
        public JsonService()
        { }

        public void JsonStart(string[] args)
        {
            OnStart(args);
        }

        public void JsonStop()
        {
            OnStop();
        }
    }
}
