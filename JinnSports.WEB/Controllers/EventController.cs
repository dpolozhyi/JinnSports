using JinnSports.BLL.Dtos;
using JinnSports.BLL.Interfaces;
using JinnSports.BLL.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace JinnSports.WEB.Controllers
{
    public class EventController : Controller
    {
        private IEventService eventsService;
        public EventController()
        {
            this.eventsService = new EventsService();
        }

        public ActionResult Index()
        {

            IEnumerable<SportTypeDto> sportTypes = this.eventsService.GetSportTypes();
            if (sportTypes != null)
            {
                return this.View(sportTypes);
            }
            else
            {
                return this.HttpNotFound();
            }
            return this.View();
        }

    }
}