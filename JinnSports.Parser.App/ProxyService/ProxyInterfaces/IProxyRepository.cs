using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinnSports.Parser.App.ProxyService.ProxyInterfaces
{
    interface IProxyRepository<T> where T : IProxyServer
    {
        void Delete(string ip);
        void Modify(T proxy);
        void Clear();
        int Count();
        void Add(T proxy);
        void Add(List<T> proxyList);
        List<T> GetAll();
        bool isAvaliable(T proxy);
        bool Contains(string ip);
    }
}
