using System.Collections.Generic;
using System.Web.Mvc;
using JinnSports.BLL.Interfaces;
using JinnSports.BLL.Service;

namespace JinnSports.WEB.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamService teamService;

        public TeamController()
        {
            this.teamService = new TeamService();
        }

        // GET: Team
        public ActionResult Index()
        {
            return this.View();
        }

        // GET: Team
        public ActionResult Details(int id)
        {            
            return this.View();
        }
    }
}
