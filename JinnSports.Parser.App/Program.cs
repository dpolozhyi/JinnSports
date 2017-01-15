using DTO.JSON;
using System.Collections.Generic;
using JinnSports.Parser.App.JsonParsers;
using JinnSports.Parser.App.ProxyService.ProxyTerminal;
using System.Net;
using JinnSports.Parser.App.ProxyService.ProxyParser;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace JinnSports.Parser.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*ProxyTerminal pt = new ProxyTerminal();
            ProxyParser pp = new ProxyParser();
            pp.UpdateData(true, "http://foxtools.ru/Proxy");*/
            ProxyTerminal pt = new ProxyTerminal();
            pt.GetProxyResponse(new System.Uri("https://2ip.ua/ru"));
        }
    }
}
