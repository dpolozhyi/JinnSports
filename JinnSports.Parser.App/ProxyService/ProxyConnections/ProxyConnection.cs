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

        public HttpWebResponse GetProxyResponse(string url, int tries)
        {
            int iter = 0;
            HttpWebResponse response;
            HttpWebRequest request;
            try
            {
                while (iter < tries)
                {
                    string proxy = this.GetProxy();
                    if (proxy != string.Empty)
                    {
                        if (this.CanPing(proxy) == true)
                        {
                            try
                            {
                                request = (HttpWebRequest)WebRequest.Create(url);
                                request.Headers.Set(HttpRequestHeader.ContentEncoding, "1251");
                                WebProxy webProxy = new WebProxy(proxy, true);
                                request.Proxy = webProxy;
                                response = (HttpWebResponse)request.GetResponse();
                                Debug.WriteLine("Good IP : " + proxy);
                                this.SetStatus(proxy, true);
                                return response;
                            }
                            catch (Exception e)
                            {
                                this.SetStatus(proxy, false);
                                iter++;
                            }
                        }
                        else
                        {
                            this.SetStatus(proxy, false);
                            iter++;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new WebResponseException(ex.Message, ex.InnerException);
            }
        }

        public HttpWebResponse GetProxyResponse(Uri uri)
        {
            Trace.WriteLine("In function GetProxyResponce (Working)______________");



            int intAvailableThreads, intAvailableIoAsynThreds;

            ThreadPool.GetAvailableThreads(out intAvailableThreads,
            out intAvailableIoAsynThreds);

            // построить сообщение для записи
            string strMessage =
                String.Format(@"Is Thread Pool: {0},
            Thread Id: {1} Free Threads {2}",
                    Thread.CurrentThread.IsThreadPoolThread.ToString(),
                    Thread.CurrentThread.GetHashCode(),
                    intAvailableThreads);

            // проверить, находится ли поток в пуле потоков.
            Trace.WriteLine(strMessage);

            Trace.WriteLine("____________________________________________");




            HttpWebResponse response;
            HttpWebRequest request;
            // Create the request object.
                string proxy = this.GetProxy();
                Trace.WriteLine("************************************");
                Trace.WriteLine("Current IP : " + proxy);
                Trace.WriteLine("************************************");
                if (proxy != string.Empty)
                {
                    if (this.CanPing(proxy) == true)
                    {
                        try
                        {
                            request = (HttpWebRequest)WebRequest.Create(uri);
                            request.Headers.Set(HttpRequestHeader.ContentEncoding, "1251");
                            WebProxy webProxy = new WebProxy(proxy, true);
                            request.Proxy = webProxy;
                            response = (HttpWebResponse)request.GetResponse();
                            Debug.WriteLine("Good IP : " + proxy);
                            this.SetStatus(proxy, true);

                            return response;
                        }
                        catch (Exception ex)
                        {
                            this.SetStatus(proxy, false);
                            //throw new WebResponseException(ex.Message, ex.InnerException);
                        }
                    }
                    else
                    {
                        this.SetStatus(proxy, false);
                    }
                }
                return null;
        }
    }
}

