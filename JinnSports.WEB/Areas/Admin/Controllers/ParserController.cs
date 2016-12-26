﻿using System.Collections.Generic;
using System.Web.Http;
using DTO.JSON;
using JinnSports.BLL.Interfaces;

namespace JinnSports.WEB.Areas.Admin.Controllers
{
    public class ParserController : ApiController
    {
        private readonly IEventService eventService;

        public ParserController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        public IHttpActionResult PostResults(List<SportEventDTO> events)
        {
            if (this.eventService.SaveSportEvents(events))
            {
                return this.Ok();
            }
            else
            {
                return this.BadRequest();
            } 
        }
    }
}
