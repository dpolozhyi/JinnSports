using JinnSports.BLL.Dtos;
using JinnSports.BLL.Interfaces;
using JinnSports.WEB.Areas.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JinnSports.WEB.Areas.Mvc.Controllers
{
    public class TeamController : Controller
    {
        private const int PAGESIZE = 2;

        private readonly ITeamService teamService;

        private readonly ITeamDetailsService teamDetailsService;

        public TeamController(ITeamService teamService, ITeamDetailsService teamDetailsService)
        {
            this.teamService = teamService;
            this.teamDetailsService = teamDetailsService;
        }

        // GET: Mvc/Team
        public ActionResult Index(int id = 1)
        {
            int recordsTotal = this.teamService.Count();
            int page;

            if (id < 1)
            {
                page = 1;
            }
            else
            {
                page = id;
            }

            PageInfo pageInfo = new PageInfo(
                "Team", "Index", recordsTotal, page, PAGESIZE);

            IEnumerable<TeamDto> teams = this.teamService.GetAllTeams(
                (page - 1) * PAGESIZE, PAGESIZE);

            TeamViewModel teamViewModel = new TeamViewModel()
            {
                PageInfo = pageInfo,
                TeamDtos = teams
            };

            return View(teamViewModel);
        }
        public ActionResult Details(int id)
        {
            int teamResultsCount = teamDetailsService.Count(id);

            TeamDto team = this.teamService.GetTeamById(id);
            IEnumerable<ResultDto> results = this.teamDetailsService.GetResults(id, 0, teamResultsCount);

            TeamResultsDto teamResults = new TeamResultsDto { Team = team, Results = results };

            if (teamResults != null)
            {
                return View(teamResults);
            }
            else
            {
                return View();
            }
        }
    }
}