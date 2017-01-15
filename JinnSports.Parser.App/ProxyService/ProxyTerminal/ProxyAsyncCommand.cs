using JinnSports.Parser.App.ProxyService.ProxyTerminal.Delegates;
using JinnSports.Parser.App.ProxyService.ProxyInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;
using JinnSports.Parser.App.ProxyService.ProxyConnections;
using System.Threading;

namespace JinnSports.Parser.App.ProxyService.ProxyTerminal
{
    public class ProxyAsyncCommand : ICommand
    {
        private GetProxyAsyncDelegate getProxyAsync;
        private Uri uri;
        private TaskFactory tf = new TaskFactory();
        private ProxyConnection pc;
        private CancellationTokenSource cancelTokSSrc;

        public ProxyAsyncCommand(IProxyConnection proxyConnection, Uri uri)
        {
            this.uri = uri;
            getProxyAsync = new GetProxyAsyncDelegate(proxyConnection.GetProxyResponse);
            this.pc = new ProxyConnection();
            this.cancelTokSSrc = new CancellationTokenSource();
        }

        public void Execute()
        {
            Task<HttpWebResponse>[] tasks = new Task<HttpWebResponse>[5];
            for (int i = 0; i < 5; i++)
            {
                tasks[i] = Task<HttpWebResponse>.Factory.StartNew(() => this.pc.GetProxyResponse(uri), cancelTokSSrc.Token);
            }
            Task.WaitAll(tasks);
            
            Trace.WriteLine("tasks completed");
            //getProxyAsync.Invoke(this.uri);
            //getProxyAsync.BeginInvoke(this.uri, this.ResponseCallBack, null);
        }

        /*private void ResponseCallBack(IAsyncResult asyncResult)
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
        } */
    }
}
