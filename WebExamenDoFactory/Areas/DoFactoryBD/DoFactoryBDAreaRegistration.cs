using System.Web.Mvc;

namespace WebExamenDoFactory.Areas.DoFactoryBD
{
    public class DoFactoryBDAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DoFactoryBD";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DoFactoryBD_default",
                "DoFactoryBD/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}