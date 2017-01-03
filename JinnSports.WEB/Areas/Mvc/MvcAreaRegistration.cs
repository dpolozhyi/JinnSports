using System.Web.Mvc;

namespace JinnSports.WEB.Areas.Mvc
{
    public class MvcAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Mvc";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Mvc_default",
                "Mvc/{controller}/{action}",
                new { action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
               "Mvc_select",
               "Mvc/{controller}/{action}/{id}",
               new { action = "SportTypeSelect", id = UrlParameter.Optional }
           );
        }
    }
}