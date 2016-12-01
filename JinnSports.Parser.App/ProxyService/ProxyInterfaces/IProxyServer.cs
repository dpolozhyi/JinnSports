using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinnSports.Parser.App.ProxyService.ProxyInterfaces
{
    public interface IProxyServer
    {
        string Ip { get; set; }
        string Status { get; set; }
        int Priority { get; set; }
        DateTime LastUsed { get; set; }
    }
}
