using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using JinnSports.DAL.Repositories;
using JinnSports.Entities;
using JinnSports.Parser.App.HtmlParsers;
using JinnSports.Parser.App.JsonParsers;
using JinnSports.Parser.App.JsonParsers.JsonEntities;
using JinnSports.Parser.App.ProxyService.ProxyParser;
using JinnSports.Parser.App.ProxyService.ProxyConnection;
using JinnSports.DataAccessInterfaces.Interfaces;
using JinnSports.Entities.Entities;
using System.Net;
using log4net;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace JinnSports.Parser.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*Thread jsonThread;
            JsonParser jp = new JsonParser();
            jsonThread = new Thread(() => jp.StartParser());
            jsonThread.Name = "JsonParserThread";
            jsonThread.Start();
            int j = 0;
            while (jsonThread.IsAlive)
            {
                Console.Clear();
                Console.WriteLine("Json parser works for {0}s", j++);
                Thread.Sleep(1000);
            }*/

            IUnitOfWork unit = new EFUnitOfWork("SportsContext");
            List<Team> teams = unit.GetRepository<Team>().Get().ToList();
            foreach(var t in teams)
            {
                Console.WriteLine("{0} {1}", t.Name, t.SportType.Name);
            }

            /*HTMLParser24score htmlParser = new HTMLParser24score(new EFUnitOfWork("SportsContext"));
            JsonParser jp = new JsonParser();
            Thread htmlThread = new Thread(() => htmlParser.Parse());
            Thread jsonThead = new Thread(() => jp.StartParser());
            int t = 0;
            htmlThread.Start();
            jsonThead.Start();
            while(htmlThread.IsAlive || jsonThead.IsAlive)
            {
                Console.Clear();
                Console.WriteLine("Parsers are working for {0}s", t++);
                Thread.Sleep(1000);
            }*/

            /*string result;
            JsonResult jsonResults;
            List<Result> res;

            result = jp.GetJsonFromUrl(jp.SiteUri);

            jsonResults = jp.DeserializeJson(result);

            res = jp.GetResultsList(jsonResults);

            jp.DBSaveChanges(res);*/

        }
    }
}
