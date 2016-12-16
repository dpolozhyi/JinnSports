using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JinnSports.BLL.Interfaces;
using JinnSports.BLL.Service;
using JinnSports.BLL.Dtos;

namespace JinnSports.WEB.Controllers
{
    public class TeamDetailsController : Controller
    {
        private readonly ITeamDetailsService teamDetailsService;
        // GET: TeamDetails
        public ActionResult Index()
        {
            return View();
        }
        // GET: Team
        public ActionResult Details(int? id = 2)
        {
            return this.View();
        }

        public TeamDetailsController()
        {
            this.teamDetailsService = new TeamDetailsService();
        }

        [HttpPost]
        public ActionResult LoadResults(int? id = 2)
        {
            string draw = this.Request.Form.GetValues("draw").FirstOrDefault();
            string start = this.Request.Form.GetValues("start").FirstOrDefault();
            string length = this.Request.Form.GetValues("length").FirstOrDefault();
            //string id = this.Request.Form.GetValues("id").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = this.teamDetailsService.Count();
            int teamId = id != null ? Convert.ToInt32(id) : 0;

            List<ResultDto> results = this.teamDetailsService.GetResults(teamId)
                .Skip(skip)
                .Take(pageSize)
                .ToList();

           /* foreach (ResultDto result in results)
            {
                re.Results = null;
            }*/

            return this.Json(
                new
                {
                    draw = draw,
                   // recordsFiltered = recordsTotal,
                    //recordsTotal = recordsTotal,
                    data = results
                },
                JsonRequestBehavior.AllowGet);
        }
    }
}