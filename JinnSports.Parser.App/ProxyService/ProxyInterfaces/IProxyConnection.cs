using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JinnSports.Parser.App.ProxyService.ProxyInterfaces
{
    public interface IProxyConnection
    {
        void SetStatus(string ip, bool connected);

        string GetProxy();

        bool CanPing(string address);

        HttpWebResponse GetProxyResponse(string url, int tries);

        HttpWebResponse GetProxyResponse(Uri uri);
    }
}
