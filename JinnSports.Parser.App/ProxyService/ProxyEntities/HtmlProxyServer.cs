using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JinnSports.Parser.App.ProxyService.ProxyInterfaces;

namespace JinnSports.Parser.App.ProxyService.ProxyEnities
{
    public class HtmlProxyServer : IHtmlProxyServer
    {
        public string Ip { get; set; }
        public string Port { get; set; }
        public string Anonymity { get; set; }
        public string Type { get; set; }
        public double Ping { get; set; }
    }
    public class HtmlProxyServerCollection
    {
        public HtmlProxyServerCollection()
        {
            HtmlProxies = new List<HtmlProxyServer>();
        }
        public List<HtmlProxyServer> HtmlProxies { get; set; }
    }
}
