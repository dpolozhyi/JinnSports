using System;
using JinnSports.Parser.App.ProxyService.ProxyInterfaces;
using System.Xml.Serialization;

namespace JinnSports.Parser.App.ProxyService.ProxyEnities
{
    public abstract class BaseProxyServer
    {
        [XmlElement]
        public string Ip { get; set; }
    }
    [Serializable]
    public class ProxyServer : BaseProxyServer, IProxyServer
    {
        [XmlAttribute]
        public string Status { get; set; }
        [XmlAttribute]
        public int Priority { get; set; }
        [XmlElement]
        public DateTime LastUsed { get; set; }
    }
}
