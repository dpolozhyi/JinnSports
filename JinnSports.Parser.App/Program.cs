using System;
using System.Collections.Generic;
using System.Linq;
using JinnSports.DAL.Repositories;
using JinnSports.Entities;
using System.Data.SqlClient;
using JinnSports.Parser.App.JsonParsers;
using JinnSports.Parser.App.ProxyService.ProxyParser;
using JinnSports.Parser.App.ProxyService.ProxyConnection;
using JinnSports.DataAccessInterfaces.Interfaces;
using System.Threading;
using System.Net;

namespace JinnSports.Parser.App
{
    public class Program
    {
        static void Main(string[] args)
        {
            /*ProxyParser pp = new ProxyParser();
            pp.UpdateData(true, "http://foxtools.ru/Proxy");*/
            //pp.UpdateData();*/
            /*EFUnitOfWork unit = new EFUnitOfWork("SportsContext");
            Team t1 = new Team()
            {
                Name = "Manchester City",
            };
            Team t2 = new Team()
            {
                Name = "Manchester City",
            };
            Team t3 = new Team()
            {
                Name = "Manchester United",
            };
            unit.Set<Team>().Add(t1);
            unit.Set<Team>().Add(t2);
            unit.Set<Team>().Add(t3);
            unit.SaveChanges();
            IList<Team> f = unit.Set<Team>().GetAll();
            ProxyConnection pc = new ProxyConnection();
            while (true)
            {
                string proxy = pc.GetProxy();
                if (proxy != string.Empty)
                {
                    if (pc.CanPing(proxy) == true)
                    {
                        try
                        {
                            WebProxy webProxy = new WebProxy(proxy, true);
                            WebRequest request = WebRequest.Create("https://2ip.ru");
                            request.Proxy = webProxy;
                            WebResponse response = request.GetResponse();
                            Console.WriteLine("Good IP : " + proxy);
                            pc.SetStatus(proxy, true);
                        }
                        catch (Exception e)
                        {
                            pc.SetStatus(proxy, false);
                        }
                    }
                    else
                    {
                        pc.SetStatus(proxy, false);
                    }

                }
                /*pc.SetStatus(proxy, true);
                //string a = ConfigSection.XmlPath();
                string path = ConfigSettings.Xml();
            }
        }*/
            Thread jThread;
            /* ParserManager pm = new ParserManager();
             jThread = new Thread(() => pm.StartJsonParser(new EFUnitOfWork("StudetnsContext")));
             jThread.Start();*/
        }
    }
}
