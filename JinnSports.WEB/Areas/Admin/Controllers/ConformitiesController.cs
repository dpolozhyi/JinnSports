using JinnSports.BLL.Dtos;
using JinnSports.BLL.Interfaces;
using System.Web.Mvc;

namespace JinnSports.WEB.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ConformitiesController : Controller
    {
        private readonly IConformityService conformityService;

        public ConformitiesController(IConformityService conformityService)
        {
            this.conformityService = conformityService;
        }
        
        public ActionResult Index()
        {
            var model = this.conformityService.GetConformities();            
               
            return this.View(model);
        }
                       
        public ActionResult Edit(string inputName)
        {
            var model = this.conformityService.GetConformityViewModel(inputName.Replace("_", " "));
            return this.View(model);            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(ConformityViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var newModel = this.conformityService.GetConformityViewModel(model.InputName);
                return this.View("Edit", newModel);
            }

            this.conformityService.Save(model);

           return this.RedirectToAction("Index");
        }
    }
}
