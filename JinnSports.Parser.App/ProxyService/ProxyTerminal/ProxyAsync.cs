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
    public class ProxyAsync : IProxyAsync
    {
        private Uri uri;
        private ProxyConnection pc;
        private CancellationTokenSource cancelTokenSrc;

        public ProxyAsync(IProxyConnection proxyConnection, Uri uri)
        {
            this.uri = uri;
            this.pc = new ProxyConnection();
            this.cancelTokenSrc = new CancellationTokenSource();
        }

        public HttpWebResponse GetProxyAsync()
        {
            Task<HttpWebResponse>[] tasks = new Task<HttpWebResponse>[2];
            for (int i = 0; i < 2; i++)
            {
                Trace.WriteLine(String.Format("{0} - Cycle step", i));

                //Thread.Sleep(4000);
                tasks[i] = Task<HttpWebResponse>.Factory.StartNew(() =>
                {
                    var result = this.pc.GetProxyResponse(uri, cancelTokenSrc.Token);
                    if (result != null)
                    {
                        cancelTokenSrc.Cancel();
                    }
                    return result;
                }
                , this.cancelTokenSrc.Token);

                if (this.cancelTokenSrc.Token.IsCancellationRequested)
                {
                    Trace.WriteLine(String.Format("Task Group canceled"));
                    break;
                }
            }
            Task.WaitAll(tasks.ToArray());
            foreach (Task<HttpWebResponse> task in tasks)
            {
                if (task.Status == TaskStatus.RanToCompletion)
                {
                    if (task.Result != null)
                    {
                        Trace.WriteLine("*&*^&^*^&^**^^*&^        Valid IP Found     *&*^&^*^&^**^^*&^");
                        return task.Result as HttpWebResponse;
                    }
                }
            }
            Trace.WriteLine("tasks completed");
            return null;      
        }
    }
}
