using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using JinnSports.Parser.App.JsonEntities;

namespace JinnSports.Parser.App
{
    public class JsonParser
    {
        public Uri FonbetUri { get; private set; }

        public JsonParser()
        {
            FonbetUri = new Uri("http://results.fbwebdn.com/results.json.php");
        }

        public string GetJsonFromUrl()
        {
            return this.GetJsonFromUrl(FonbetUri);
        }

        public string GetJsonFromUrl(Uri uri)
        {
            int ch;
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://results.fbwebdn.com/results.json.php");
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            for (int i = 1; ; i++)
            {
                ch = stream.ReadByte();
                if (ch == -1) break;
                result += (char)ch;
            }
            resp.Close();
            return result;
        }

        public JsonResult DeserializeJson(string jsonStr)
        {
            JsonResult res = JsonConvert.DeserializeObject<JsonResult>(jsonStr);
            return res;
        }
    }
}
