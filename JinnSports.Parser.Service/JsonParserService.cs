using System.Data.SqlClient;
using System.IO;
using System.ServiceProcess;
using System.Threading;
using JinnSports.Parser.App;

namespace JinnSports.Parser.Service
{
    public partial class JsonParserService : ServiceBase
    {
        ParserManager pm;

        Thread jParserThread;

        StreamWriter sw;

        public JsonParserService()
        {
            InitializeComponent();
            sw = new StreamWriter(@"C:/Users/Denis/Desktop/log.txt");
            pm = new ParserManager();
            jParserThread = new Thread(()=>pm.StartJsonParser());
        }

        protected override void OnStart(string[] args)
        {
            jParserThread.Start();
            sw.WriteLine("Thread started");
        }

        protected override void OnStop()
        {
            sw.WriteLine("Thread stopped");
            jParserThread.Abort();
            sw.Close();
        }
    }
}
