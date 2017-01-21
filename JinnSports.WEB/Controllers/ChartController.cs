using System.Web.Mvc;
using JinnSports.BLL.Dtos.Charts;
using JinnSports.BLL.Interfaces;

namespace JinnSports.WEB.Controllers
{
    public class ChartController : Controller
    {
        private readonly IChartService chartService;

        public ChartController(IChartService chartService)
        {
            this.chartService = chartService;
        }

        public ActionResult WinRateChartForTeam(int id)
        {
            var data = chartService.GetDataTableForTeam(id);

            return View(new WinRateChartDto
            {
                Title = "",
                Subtitle = "",
                DataTable = data
            });
        }
    }
}