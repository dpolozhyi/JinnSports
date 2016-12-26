using DTO.JSON;
using JinnSports.Parser.App.Exceptions;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace JinnSports.Parser
{
    public class ApiConnection
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ApiConnection));

        private string baseUrl = "http://localhost:55714/Admin";
        private string controllerUrn = "api/Data";

        private HttpClient client;

        public ApiConnection()
        {

        }

        public ApiConnection(string baseUrl, string controllerUrn)
        {
            this.baseUrl = baseUrl;
            this.controllerUrn = controllerUrn;
        }

        /// <summary>
        /// Accepts collection of SportEventDTO and try to serialize and send it to Api Controller
        /// 
        /// </summary>
        /// <param name="events"></param>
        /// <exception cref="SaveDataException"></exception>
        public async void SendEvents(ICollection<SportEventDTO> events)
        {
            try
            {
                Log.Info("Starting Data transfer");

                client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string json = JsonConvert.SerializeObject(events, Formatting.Indented);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(controllerUrn, content);
                if (response.IsSuccessStatusCode)
                {
                    Log.Info("Data sucsessfully transfered");
                } 
                else
                {
                    Log.Info("Error occured during Data transfer");
                }
            }
            catch (Exception ex)
            {
                throw new SaveDataException(ex.Message);
            }
            finally
            {
                if (client != null)
                {
                    client.Dispose();
                    Log.Info("Data transfer closed");
                }
            }

        }
    }
}
