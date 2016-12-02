using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using JinnSports.DAL.Entities;
using JinnSports.DataAccessInterfaces;


namespace JinnSports.Parser.App
{
    public class HTMLParser24score
    { 
        public HTMLParser24score(IUnitOfWork unit)
        {
            this.Unit = unit;
        }

        public IUnitOfWork Unit { get; private set; }

        public void Parse(uint daysCount = 1)
        {
            Uri footballUrl = new Uri("https://24score.com/?date=");
            Uri basketballUrl = new Uri("https://24score.com/basketball/?date=");
            Uri hokkeyUrl = new Uri("https://24score.com/ice_hockey/?date=");
            List<Uri> selectedUris = new List<Uri>();
            selectedUris.Add(footballUrl);
            selectedUris.Add(basketballUrl);
            selectedUris.Add(hokkeyUrl);

            foreach (Uri baseUrl in selectedUris)
            {
                SportType sport = new SportType();
                if (baseUrl.OriginalString.Contains("basketball"))
                {
                    sport.Name = "Basketball";
                }
                else if (baseUrl.OriginalString.Contains("ice_hockey"))
                {
                    sport.Name = "Hockey";
                }
                else
                {
                    sport.Name = "Football";
                }

                this.Unit.SportTypes.Add(sport);
                DateTime now = DateTime.Now;

                for (int i = 1; i < daysCount + 1; i++)
                {
                    DateTime date = now.Subtract(new TimeSpan(i, 0, 0, 0));
                    string url = baseUrl.ToString() + date.Date.ToString("yyyy-MM-dd");

                    WebRequest reqGET = WebRequest.Create(url);
                    reqGET.Headers.Set(HttpRequestHeader.ContentEncoding, "1251");
                    WebResponse resp = reqGET.GetResponse();
                    Stream stream = resp.GetResponseStream();
                    StreamReader sr = new StreamReader(stream);
                    string html = sr.ReadToEnd();

                    var parser = new HtmlParser();
                    var document = parser.Parse(html);

                    foreach (IElement htmlTr in document.QuerySelectorAll(".daymatches tr[class]:not(.hidden)"))
                    {
                        CompetitionEvent c = new CompetitionEvent() { Date = date };

                        string name1 = htmlTr.Children[1].FirstElementChild.TextContent;
                        string name2 = htmlTr.Children[2].FirstElementChild.TextContent;

                        Team t1 = new Team() { Name = name1, SportType = sport };
                        Team t2 = new Team() { Name = name2, SportType = sport };

                        var score = htmlTr.QuerySelectorAll("span[id*='score']")[0].TextContent;
                        score = score.Replace("\n", string.Empty).Replace("\t", string.Empty);
                        score = score.Split('(')[0];

                        Result result1 = new Result() { Team = t1, CompetitionEvent = c, Score = score };

                        string score2;
                        if (score.Contains(":"))
                        {
                            var scores = score.Split(':');
                            score2 = scores[1] + ":" + scores[0];
                        }
                        else
                        {
                            score2 = score;
                        }

                        Result result2 = new Result() { Team = t2, CompetitionEvent = c, Score = score2 };
                        this.Unit.Results.Add(result1);
                        this.Unit.Results.Add(result2);
                    }
                }

            }
        }
    }
}
