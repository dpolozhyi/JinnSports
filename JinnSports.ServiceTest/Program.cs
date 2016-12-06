using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JinnSports.ServiceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            JsonService js = new JsonService();
            js.JsonStart(new string[0]);
            for(int i=0; i<5000; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(100);
            }
        }
    }
}
