﻿using JinnSports.BLL.Dtos;
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
    public class EventController : ApiController
    {
        private const string FOOTBALL = "Football";
        private IEventService eventService;

        public EventController()
        {
            this.eventService = new EventsService();
        }

        [HttpGet]
        public IHttpActionResult LoadEvents()
        {
            string draw = this.Request.GetQueryNameValuePairs().Where(x => x.Key == "draw").FirstOrDefault().Value;
            string start = this.Request.GetQueryNameValuePairs().Where(x => x.Key == "start").FirstOrDefault().Value;
            string length = this.Request.GetQueryNameValuePairs().Where(x => x.Key == "length").FirstOrDefault().Value;

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = this.eventService.Count(FOOTBALL);

            IEnumerable<ResultDto> results = this.eventService
                .GetSportEvents(FOOTBALL, skip, pageSize);

            return this.Ok(results);
        }

        // POST: api/Event
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Event/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Event/5
        public void Delete(int id)
        {
        }
    }
}
