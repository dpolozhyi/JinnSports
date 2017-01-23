using System.Web.Mvc;
using JinnSports.BLL.Interfaces;
using JinnSports.BLL.Dtos;

namespace JinnSports.WEB.Controllers
{
    public class TeamDetailsController : Controller
    {
        private readonly ITeamService teamService;
        private readonly IChartService chartService;

        public TeamDetailsController(ITeamService teamService, IChartService chartService)
        {
            this.teamService = teamService;
            this.chartService = chartService;
        }

        public ActionResult Details(int id = 0)
        {
            TeamDto team = this.teamService.GetTeamById(id);
            var data = chartService.GetDataTableForTeam(team.Id);

            if (team != null)
            {
                return this.View(new TeamDetailsDto
                {
                    TeamDto = team,
                    WinRateDataTable = data
                });
            }
            else
            {
                return this.HttpNotFound();
            }
        }
    }
}