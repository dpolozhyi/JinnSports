using JinnSports.BLL.Dtos;
using JinnSports.BLL.Interfaces;
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
        // GET: api/Event
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [HttpPost]
        // GET: api/Event/5
        public IHttpActionResult LoadEvents(int id)
        {         
            /*string draw = this.Request.Form.GetValues("draw").FirstOrDefault();
            string start = this.Request.Form.GetValues("start").FirstOrDefault();
            string length = this.Request.Form.GetValues("length").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;

            int recordsTotal = this.eventService.Count(FOOTBALL);*/

            IEnumerable<SportEventDto> events = this.eventService
                .GetSportEvents(FOOTBALL, 0, 15);

            return Ok(events);

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
