using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using JinnSports.BLL.Interfaces;
using JinnSports.BLL.Dtos.SportType;
using JinnSports.BLL.Dtos;

namespace JinnSports.WEB.Controllers
{
    public class HomeController : Controller
    {
        private ISportTypeService sportTypeService;

        public HomeController(ISportTypeService sportTypeService)
        {
            this.sportTypeService = sportTypeService;
        }


        public ActionResult Index()
        {
            List<SportTypeListDto> eventList = sportTypeService.GetSportTypes(0, 1).SportTypeResults.ToList();
            foreach(var ev in eventList)
            {
                ev.Results.OrderByDescending(x => x.Date);
            }
            return this.View(eventList);
        }
    }
}