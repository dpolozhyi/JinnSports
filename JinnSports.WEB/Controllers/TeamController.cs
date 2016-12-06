using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using JinnSports.BLL.Interfaces;
using JinnSports.BLL.Service;
using JinnSports.Entities;
using JinnSports.WEB.Mappers;
using JinnSports.WEB.Views.ViewModels;

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
            IEnumerable<Team> teams = teamService.GetAllTeams();

            List<TeamViewModel> teamViewModels = new List<TeamViewModel>();
            foreach (var team in teams)
            {
                teamViewModels.Add(team.MapToTeamViewModel());
            }

            return View(teamViewModels);
        }

        // GET: Team/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Team/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Team/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Team/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Team/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Team/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Team/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
