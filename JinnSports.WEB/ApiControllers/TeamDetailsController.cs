using JinnSports.BLL.Dtos;
using JinnSports.BLL.Interfaces;
using JinnSports.BLL.Service;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JinnSports.WEB.ApiControllers
{
    public class TeamDetailsController : ApiController
    {
        private ITeamDetailsService teamDetailsService;
        public TeamDetailsController()
        {
            this.teamDetailsService = new TeamDetailsService();
        }
        // GET: api/TeamDetails/5
        [HttpGet]
        public HttpResponseMessage LoadResults(int teamId = 6)
        {
            //var p = this.Request.Content.ReadAsAsync<JObject>();
            /*string draw = this.Request.Form.GetValues("draw").FirstOrDefault();
            string start = this.Request.Form.GetValues("start").FirstOrDefault();
            string length = this.Request.Form.GetValues("length").FirstOrDefault();
            //string id = this.Request.Form.GetValues("id").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int teamId = id != null ? Convert.ToInt32(id) : 0;
            int recordsTotal = this.teamDetailsService.Count(teamId);*/

            IEnumerable<ResultDto> results = this.teamDetailsService.GetResults(teamId);
            /* .Skip(skip)
             .Take(pageSize)
             .ToList();*/

            /* foreach (ResultDto result in results)
             {
                 re.Results = null;
             }*/
            return Request.CreateResponse(HttpStatusCode.OK, results);
        }

        // PUT: api/TeamDetails/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/TeamDetails/5
        public void Delete(int id)
        {
        }
    }
}
