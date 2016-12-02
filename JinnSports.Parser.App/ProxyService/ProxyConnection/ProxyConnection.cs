using System;
using System.Collections.Generic;
using System.Linq;
using JinnSports.Parser.App.ProxyService.ProxyRepository;
using JinnSports.Parser.App.ProxyService.ProxyEnities;

namespace JinnSports.Parser.App.ProxyService.ProxyConnection
{
    public class ProxyConnection
    {
        public void SetStatus(string ip, bool connected)
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
                        proxy.Status = "Stable";
                    }
                    else
                    {
                        if (proxy.Status == "New")
                        {
                            proxy.Status = "Stable";
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
                                proxy.Status = "Unstable";
                                break;
                            }
                        case 1:
                            {
                                proxy.Priority++;
                                proxy.Status = "Bad";
                                break;
                            }
                        case 2:
                            {
                                proxy.Priority++;
                                proxy.Status = "Eliminated";
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
            xmlWriter.Modify(proxy);
        }
        public string GetProxy()
        {
            ProxyRepository<ProxyServer> xmlWriter = new ProxyRepository<ProxyServer>();
            List<ProxyServer> proxyCollection = xmlWriter.GetAll();
            List<ProxyServer> usableProxies = proxyCollection.Where(x => x.Priority == 0 && xmlWriter.isAvaliable(x)).ToList();
            if (usableProxies.Count == 0)
            {
                usableProxies = proxyCollection.Where(x => x.Priority == 1 && xmlWriter.isAvaliable(x)).ToList();
                if (usableProxies.Count == 0)
                {
                    usableProxies = proxyCollection.Where(x => x.Priority == 2 && xmlWriter.isAvaliable(x)).ToList();
                }
            }
            try
            {
                ProxyServer proxyServer = usableProxies.ElementAt(new Random().Next(0, usableProxies.Count));
                return proxyServer.Ip;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            return "";
        }
    }
}
