using JinnSports.BLL.Dtos;
using JinnSports.BLL.Interfaces;
using JinnSports.BLL.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JinnSports.WEB.ApiControllers
{
    public class TeamController : ApiController
    {
        private ITeamService teamService;

        public TeamController()
        {
            this.teamService = new TeamService();
        }

        // GET: api/Team/5
        [HttpGet]
        public IHttpActionResult LoadTeams()
        {
            /*string draw = this.Request.Form.GetValues("draw").FirstOrDefault();
            string start = this.Request.Form.GetValues("start").FirstOrDefault();
            string length = this.Request.Form.GetValues("length").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = this.teamService.Count();*/

            IEnumerable<TeamDto> teams = this.teamService.GetAllTeams();
               /* .Skip(skip)
                .Take(pageSize)
                .ToList();*/

            foreach (TeamDto team in teams)
            {
                team.Results = null;
            }
            return Ok(teams);
        }

        // POST: api/Team
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Team/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Team/5
        public void Delete(int id)
        {
        }
    }
}
