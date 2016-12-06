using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using JinnSports.DAL.Repositories;
using JinnSports.Entities;
using JinnSports.Parser.App.JsonParsers;
using JinnSports.Parser.App.ProxyService.ProxyParser;
using JinnSports.Parser.App.ProxyService.ProxyConnection;
using JinnSports.DataAccessInterfaces.Interfaces;
using System.Net;

namespace JinnSports.Parser.App
{
    public class Program
    {
        static void Main(string[] args)
        {
            JsonParser jp = new JsonParser();
            Thread jThread = new Thread(() => jp.StartParser());
            int sec = 0;
            jThread.Start();
            while(jThread.IsAlive)
            {
                //Console.Clear();
                //Console.WriteLine("JParser works for {0}s", sec);
                sec++;
                Thread.Sleep(1000);
            }
            Console.WriteLine("Done!");
        }
    }
}
