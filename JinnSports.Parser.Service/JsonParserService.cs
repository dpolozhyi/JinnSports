using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JinnSports.Parser.App;
using JinnSports.Parser.App.JsonParsers;

namespace JinnSports.Parser.Service
{
    public partial class JsonParserService : ServiceBase
    {
        ParserManager pm;

        Thread jThread;
        
        public JsonParserService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Thread.Sleep(20000);
            pm = new ParserManager();
            jThread = new Thread(() => pm.StartJsonParser());
            StreamWriter sw = new StreamWriter(@"C:/Users/Denis/Desktop/log.txt");
            sw.WriteLine("Ready to start thread");
            jThread.Start();
            sw.WriteLine("Thread was started. End logging");
            sw.Close();
        }

        protected override void OnStop()
        {
            jThread.Abort();
        }
    }
}
