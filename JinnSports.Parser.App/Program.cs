using System;
using System.Collections.Generic;
using System.Linq;
using JinnSports.DAL.Repositories;
using JinnSports.DAL.Entities;
using System.Data.SqlClient;
using JinnSports.Parser.App.JsonParserService;
using JinnSports.Parser.App.ProxyService.ProxyParser;
using JinnSports.Parser.App.ProxyService.ProxyConnection;

namespace JinnSports.Parser.App
{
    class Program
    {
        static void Main(string[] args)
        {
           // ProxyParser pp = new ProxyParser();
            //pp.UpdateData(true);
            //pp.UpdateData();
            //ProxyConnection pc = new ProxyConnection();
            //string proxy = pc.GetProxy();
            //pc.SetStatus(proxy, true);
            string a = ConfigSection.XmlPath();
        }
    }
}
