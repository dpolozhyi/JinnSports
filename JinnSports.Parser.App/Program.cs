using DTO.JSON;
using System.Collections.Generic;
using JinnSports.Parser.App.JsonParsers;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace JinnSports.Parser.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            JsonParser jp = new JsonParser();
            List<SportEventDTO> sportList = jp.GetSportEventsList(jp.DeserializeJson(jp.GetJsonFromUrl()));
           /* foreach(var s in sportList)
            {
                Console.WriteLine("{0} {1}", s.SportType, s.Date);
                List<ResultDTO> rList = s.Results.ToList();
                Console.WriteLine("{0} {1}:{2} {3}\n", rList[0].TeamName, rList[0].Score, rList[1].Score, rList[1].TeamName);
            }*/
        }
    }
}
