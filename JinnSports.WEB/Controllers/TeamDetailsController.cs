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
        public ActionResult Index()
        {
            return this.View();
        }
        // GET: TeamDetails
        public ActionResult Details()
        {
            string url = string.Format("/api/TeamDetails/LoadResults?id={0}", this.Request.QueryString["id"]);
            return this.View((object)url);
        }
    }
}