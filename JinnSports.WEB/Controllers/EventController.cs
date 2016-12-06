using JinnSports.BLL.DTO;
using JinnSports.BLL.Interfaces;
using JinnSports.BLL.Service;
using JinnSports.DAL.Repositories;
using JinnSports.Entities;
using JinnSports.WEB.Mapper;
using JinnSports.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JinnSports.WEB.Controllers
{
    public class EventController:Controller
    {
        public ActionResult History()
        {
            IEventService bllService = new EventsService();
            IList<CompetitionEventDTO> events = bllService.GetCEvents();
            IList<SportResultsViewModel> viewModel = EventSorter.Sort(events);
            var unit = new EFUnitOfWork("SportsContext");
            var a = unit.Set<Result>().GetAll();
            bllService.Dispose();

            return View(viewModel);
        }
    }
}