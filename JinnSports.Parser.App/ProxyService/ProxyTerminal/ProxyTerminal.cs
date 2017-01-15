using JinnSports.Parser.App.ProxyService.ProxyInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using JinnSports.Parser.App.ProxyService.ProxyConnections;
using JinnSports.Parser.App.Exceptions;
using System.Diagnostics;
using System.Threading;

namespace JinnSports.Parser.App.ProxyService.ProxyTerminal
{
    public class ProxyTerminal : IProxyTerminal
    {
        ICommand getProxyAsyncCommand;
        private ProxyConnection pc;
        private HttpWebRequest request;

        public ProxyTerminal()
        {
            this.pc = new ProxyConnection();
        }

        /*HttpWebResponse GetProxyResponse(string url, int tries)
        {
            return new HttpWebResponse();
        }*/

        public void GetProxyResponse(Uri uri)
        {
            getProxyAsyncCommand = new ProxyAsyncCommand(this.pc, uri);

            getProxyAsyncCommand.Execute();
        }

    }
}
