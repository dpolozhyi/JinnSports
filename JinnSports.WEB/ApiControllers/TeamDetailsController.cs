﻿using JinnSports.BLL.Dtos;
using JinnSports.BLL.Interfaces;
using JinnSports.BLL.Service;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet]
        public IHttpActionResult LoadResults(int id = 1)
        {
            string draw = this.Request.GetQueryNameValuePairs().Where(x => x.Key == "draw").FirstOrDefault().Value;
            string start = this.Request.GetQueryNameValuePairs().Where(x => x.Key == "start").FirstOrDefault().Value;
            string length = this.Request.GetQueryNameValuePairs().Where(x => x.Key == "length").FirstOrDefault().Value;

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = this.teamDetailsService.Count(id);

            IEnumerable<ResultDto> results = this.teamDetailsService.GetResults(id, skip, pageSize);

            return this.Ok(results);
        }

        public void Post([FromBody]string value)
        {
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
