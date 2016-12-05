using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using JinnSports.DAL.Repositories;
using JinnSports.Entities;
using JinnSports.Parser.App.JsonParsers;
using JinnSports.Parser.App.JsonParsers.JsonEntities;
using JinnSports.Parser.App.ProxyService.ProxyParser;
using JinnSports.Parser.App.ProxyService.ProxyConnection;
using JinnSports.DataAccessInterfaces.Interfaces;

namespace JinnSports.Parser.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //            ProxyParser pp = new ProxyParser();
            //            pp.UpdateData(true, "http://foxtools.ru/Proxy");
            //            //pp.UpdateData();
            //            ProxyConnection pc = new ProxyConnection();
            //            string proxy = pc.GetProxy();
            //            pc.SetStatus(proxy, true);
            //            //string a = ConfigSection.XmlPath();
            //            string path = ConfigSettings.Xml();
            ParserManager pm = new ParserManager();
            Thread jThread = new Thread(() => pm.StartJsonParser());
            jThread.Start();
            Console.WriteLine("Done!");
            //jp.DBSaveChanges(res);
        }
    }
}
