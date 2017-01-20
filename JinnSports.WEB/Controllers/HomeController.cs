using System.Web.Mvc;
using System.Web.UI;
using JinnSports.BLL.Interfaces;

namespace JinnSports.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly INewsService newsService;

        private IEventService eventService;

        public HomeController(INewsService newsService, IEventService eventService)
        {
            this.newsService = newsService;
            this.eventService = eventService;
        }

        [OutputCache(Duration = 600, Location = OutputCacheLocation.Server)]
        public ActionResult Index()
        {
            var news = this.newsService.GetLastNews();

            return this.View(news);
        }

        public ActionResult UpcomingEvents()
        {


        }
    }
}