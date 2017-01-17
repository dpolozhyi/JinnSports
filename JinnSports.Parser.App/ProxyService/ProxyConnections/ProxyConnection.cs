using System;
using System.Collections.Generic;
using System.Linq;
using JinnSports.Parser.App.ProxyService.ProxyEntities;
using JinnSports.Parser.App.ProxyService.ProxyRepository;
using System.Net.NetworkInformation;
using System.Net;
using System.Diagnostics;
using JinnSports.Parser.App.ProxyService.ProxyInterfaces;
using JinnSports.Parser.App.Exceptions;
using System.Threading;
using JinnSports.Parser.App.ProxyService.ProxyEnums;
using System.Threading.Tasks;

namespace JinnSports.Parser.App.ProxyService.ProxyConnections
{
    public class ProxyConnection : IProxyConnection
    {
        private static object connectionLocker = new Object();

        public void SetStatus(string ip, bool connected)
        {
            lock (connectionLocker)
            {
                ProxyRepository<ProxyServer> xmlWriter = new ProxyRepository<ProxyServer>();
                List<ProxyServer> proxyCollection = xmlWriter.GetAll();
                ProxyServer proxy = proxyCollection.Where(x => x.Ip == ip).FirstOrDefault();
                try
                {
                    if (connected)
                    {
                        if (proxy.Priority != 0)
                        {
                            proxy.Priority = 0;
                            proxy.Status = ProxyStatus.PS_Stable;
                        }
                        else
                        {
                            if (proxy.Status == ProxyStatus.PS_New)
                            {
                                proxy.Status = ProxyStatus.PS_Stable;
                            }
                        }
                    }
                    else
                    {
                        switch (proxy.Priority)
                        {
                            case 0:
                                {
                                    proxy.Priority++;
                                    proxy.Status = ProxyStatus.PS_Unstable;
                                    break;
                                }
                            case 1:
                                {
                                    proxy.Priority++;
                                    proxy.Status = ProxyStatus.PS_Bad;
                                    break;
                                }
                            case 2:
                                {
                                    proxy.Priority++;
                                    proxy.Status = ProxyStatus.PS_Eliminated;
                                    break;
                                }
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                proxy.LastUsed = DateTime.Now;
                proxy.IsBusy = false;
                xmlWriter.Modify(proxy);
            }
        }

        public string GetProxy()
        {
            lock (connectionLocker)
            {
                ProxyRepository<ProxyServer> xmlWriter = new ProxyRepository<ProxyServer>();
                List<ProxyServer> proxyCollection = xmlWriter.GetAll();
                List<ProxyServer> usableProxies = proxyCollection.Where(x => x.Priority == 0 && xmlWriter.IsAvaliable(x)).ToList();
                if (usableProxies.Count == 0)
                {
                    usableProxies = proxyCollection.Where(x => x.Priority == 1 && xmlWriter.IsAvaliable(x)).ToList();
                    if (usableProxies.Count == 0)
                    {
                        usableProxies = proxyCollection.Where(x => x.Priority == 2 && xmlWriter.IsAvaliable(x)).ToList();
                    }
                }
                try
                {
                    ProxyServer proxyServer = usableProxies.ElementAt(new Random().Next(0, usableProxies.Count));
                    proxyServer.IsBusy = true;
                    xmlWriter.Modify(proxyServer);
                    return proxyServer.Ip;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return string.Empty;
            }
        }

        public bool CanPing(string address)
        {
            Ping ping = new Ping();

            try
            {
                PingReply reply = ping.Send(address, 2000);
                if (reply == null)
                {
                    return false;
                }

                return reply.Status == IPStatus.Success;
            }
            catch (PingException e)
            {
                return false;
            }
        }

        public HttpWebResponse GetProxyResponse(Uri uri, CancellationToken token)
        {
            HttpWebResponse response;
            HttpWebRequest request;
            // Create the request object.
            string proxy = this.GetProxy();
            Trace.WriteLine("************************************");
            Trace.WriteLine("Current IP : " + proxy);
            Trace.WriteLine("************************************");
            if (proxy != string.Empty)
            {
                if (this.CanPing(proxy))
                {
                    try
                    {
                        request = (HttpWebRequest)WebRequest.Create(uri);

                        Trace.WriteLine("New task <Web Response>");

                        request.Headers.Set(HttpRequestHeader.ContentEncoding, "1251");
                        WebProxy webProxy = new WebProxy(proxy, true);
                        request.Proxy = webProxy;
                        //1st check cancellation token

                        if (token.IsCancellationRequested)
                        {
                            Trace.WriteLine("Задача tsk отменена до получения webresponse");
                            return null;
                        }

                        Task<WebResponse> task = Task.Factory.FromAsync(
                        request.BeginGetResponse,
                        request.EndGetResponse,
                        request);

                        task.ContinueWith(t =>
                        {
                            if (t.IsFaulted)
                            {
                                Trace.WriteLine("Bad IP FOUND in RESPONSE after TaskEnd FUNCTION _____ EXCEPTION");
                                Trace.WriteLine("_____________________________________________________________");
                                proxy = ((t.AsyncState as HttpWebRequest).Proxy as WebProxy).Address.Host;
                                this.SetStatus(proxy, false);
                            }
                            else
                            {
                                //good ip found
                            }
                        });

                        response = task.Result as HttpWebResponse;
                        if (token.IsCancellationRequested)
                        {
                            Trace.WriteLine("Задача tsk отменена после получения webresponse");
                            return null;
                        }
                        if (response != null)
                        {
                            //set cancellation token
                            Trace.WriteLine("Good IP FOUND in RESPONSE after TaskEnd FUNCTION");
                            Trace.WriteLine("_____________________________________________________________");

                            Debug.WriteLine("Good IP : " + ((task.AsyncState as HttpWebRequest).Proxy as WebProxy).Address.Host);
                            proxy = ((task.AsyncState as HttpWebRequest).Proxy as WebProxy).Address.Host;
                            this.SetStatus(proxy, true);
                            return response;
                        }
                        else
                        {
                            Trace.WriteLine("Bad IP FOUND in RESPONSE after TaskEnd FUNCTION");
                            Trace.WriteLine("_____________________________________________________________");
                            proxy = ((task.AsyncState as HttpWebRequest).Proxy as WebProxy).Address.Host;
                            this.SetStatus(proxy, false);
                        }
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex.Message, "Bad IP FOUND");
                        Trace.WriteLine("_____________________________________________________________");
                        this.SetStatus(proxy, false);
                        //throw new WebResponseException(ex.Message, ex.InnerException);
                    }
                }
                else
                {
                    Trace.WriteLine("Bad IP FOUND in RESPONSE after TaskEnd FUNCTION");
                    Trace.WriteLine("_____________________________________________________________");
                    this.SetStatus(proxy, false);
                }
            }
            return null;
        }

        /*public static Task<HttpWebResponse> GetProxyResponseAsync(this HttpWebRequest request, TimeSpan timeout,
            CancellationToken token)
        {
            Trace.WriteLine("In function GetProxyResponseAsync (Working)");

            return Task.Run(() =>
            {
                if (token.IsCancellationRequested)
                    return null;
                Task<WebResponse> task;
                try
                {
                    token.Register(request.Abort);
                    task = Task.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, null);

                    if (!task.Wait(timeout))
                        throw new WebException("No response was received during the time-out period specified.",
                            WebExceptionStatus.Timeout);
                }
                catch (Exception ex)
                {
                    var exception = ex as WebException;
                    if (exception != null)
                        throw exception;

                    throw new WebException(ex.Message, WebExceptionStatus.ConnectionClosed);
                }

                return task.Result as HttpWebResponse;
            }, token);
        }*/

    }
}

