using JinnSports.DAL.EFContext;
using JinnSports.DAL.Repositories;
using JinnSports.DataAccessInterfaces.Interfaces;
using JinnSports.Parser.App.Configuration.Parser;
using JinnSports.Parser.App.HtmlParsers;
using JinnSports.Parser.App.JsonParsers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JinnSports.BLL.Service
{
    public static class ParserService
    {
        private static CancellationToken cancellationToken = new CancellationToken();

        public static async Task Initialize()
        {
            int timeInterval = ParserSettings.GetInterval("original");

            /*Task t = Task.Factory.StartNew(() =>
            {*/

            while (!cancellationToken.IsCancellationRequested)
            {
                await Run();
                await Task.Delay(timeInterval * 1000, cancellationToken);
            }
            /*
                }
            });
        }*/
        }

        public static async Task Run()
        {

            JsonParser jsonParser = new JsonParser();
            HTMLParser24score htmlParser = new HTMLParser24score();

            List<Task> tasks = new List<Task>();
            tasks.Add(Task.Factory.StartNew(() =>
            {
                jsonParser.StartParser();
            }));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                htmlParser.Parse();
            }));

            await Task.WhenAll(tasks.ToArray());
        }
    }
}
