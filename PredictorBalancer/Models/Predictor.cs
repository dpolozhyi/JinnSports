using log4net;
using PredictorDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace PredictorBalancer.Models
{
    public class Predictor
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Predictor));

        public enum Status {Awailable , Buisy, NotAwailable};

        private string baseUrl;
        private string controllerUrn;
        private int timeoutSec;

        public Predictor(string baseUrl, string controllerUrn, int timeoutSec)
        {
            this.baseUrl = baseUrl;
            this.controllerUrn = controllerUrn;
            this.timeoutSec = timeoutSec;
        }

        public int Id { get; set; }
        public Status CurrentStatus { get; private set; }

        public void SendIncomingEvents(IEnumerable<IncomingEventDTO> incomingEvents)
        {
            ApiConnection<IEnumerable<IncomingEventDTO>> connection = 
                new ApiConnection<IEnumerable<IncomingEventDTO>>(baseUrl, controllerUrn, timeoutSec);
            connection.Send(incomingEvents);
        }

        public async void UpdateStatus()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    Log.Info("Starting Data transfer");

                    client.BaseAddress = new Uri(baseUrl);
                    client.Timeout = new TimeSpan(0, 0, timeoutSec);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync(controllerUrn);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        CurrentStatus = Status.Awailable;
                        Log.Info($"Predictor #{Id} is currently awailable");
                    }
                    else if (response.StatusCode == HttpStatusCode.ServiceUnavailable)
                    {
                        CurrentStatus = Status.Buisy;
                        Log.Info($"Predictor #{Id} is currently buisy");
                    }
                    else
                    {
                        CurrentStatus = Status.NotAwailable;
                        Log.Info($"Predictor #{Id} is currently unawailable");
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("Exception occured while trying to update status", ex);
                }
            }
        }
    }
}