using JinnSports.BLL.DTO;
using JinnSports.BLL.Interfaces;
using JinnSports.BLL.Service;
using JinnSports.WEB.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using JinnSports.WEB.Filters;
using JinnSports.WEB.Mappers;

namespace JinnSports.WEB.Controllers
{
    public class EventController:Controller
    {
        public ActionResult History()
        {
            IEventService bllService = new EventsService();
            IList<CompetitionEventDTO> events = bllService.GetCEvents();
            IList<SportResultsViewModel> viewModel = EventSorter.Sort(events);
            bllService.Dispose();

            return View(viewModel);
        }
    }
}