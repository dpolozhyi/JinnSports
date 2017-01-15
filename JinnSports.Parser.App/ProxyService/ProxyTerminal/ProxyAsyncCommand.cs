using JinnSports.Parser.App.ProxyService.ProxyTerminal.Delegates;
using JinnSports.Parser.App.ProxyService.ProxyInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;

namespace JinnSports.Parser.App.ProxyService.ProxyTerminal
{
    public class ProxyAsyncCommand : ICommand
    {
        private GetProxyAsyncDelegate getProxyAsync;
        private Uri uri;

        public ProxyAsyncCommand(IProxyConnection proxyConnection, Uri uri)
        {
            this.uri = uri;
            getProxyAsync = new GetProxyAsyncDelegate(proxyConnection.GetProxyResponse);
        }

        public void Execute()
        {
            //getProxyAsync.Invoke(this.uri);
            getProxyAsync.BeginInvoke(this.uri, this.ResponseCallBack, null);
        }

        private void ResponseCallBack(IAsyncResult asyncResult)
        {
            Trace.WriteLine("Call back function");
            // получить набор данных как вывод
            HttpWebResponse response = getProxyAsync.EndInvoke(asyncResult);

            if (response != null)
            {
                string strMessage = "Good IP FOUND in RESPONSE CALLBACK FUNCTION";
                Trace.WriteLine("_____________________________________________________________");
                Trace.WriteLine(strMessage);
            } else
            {
                string strMessage = "Bad IP FOUND in RESPONSE CALLBACK FUNCTION";
                Trace.WriteLine("_____________________________________________________________");
                Trace.WriteLine(strMessage);
            }

            //безопасное завершение потоков, передеча управления парсеру
        }
    }
}
