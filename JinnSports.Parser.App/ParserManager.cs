using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JinnSports.Entities;
using JinnSports.DataAccessInterfaces.Interfaces;
using JinnSports.DAL.Repositories;
using JinnSports.Parser.App.Interfaces;
using JinnSports.Parser.App.JsonParsers;
using JinnSports.Parser.App.JsonParsers.JsonEntities;
using JinnSports.Parser.App.HtmlParsers;

namespace JinnSports.Parser.App
{
    public class ParserManager
    {
        JsonParser jParser;

        HTMLParser24score htmlParser;

        public ParserManager()
        {
            jParser = new JsonParser();
            htmlParser = new HTMLParser24score(new EFUnitOfWork("SportsContext"));
        }

        public void StartJsonParser()
        {
            JsonResult jResults = jParser.DeserializeJson(jParser.GetJsonFromUrl(jParser.FonbetUri));
            List<Result> res = jParser.GetResultsList(jResults);
            jParser.DBSaveChanges(res);
        }

        public void StartHtmlParser()
        {
            htmlParser.Parse(1);
        }
    }
}
