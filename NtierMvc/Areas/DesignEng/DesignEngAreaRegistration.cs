using System.Web.Mvc;

namespace NtierMvc.Areas.DesignEng
{
    public class DesignEngAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DesignEng";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DesignEng_default",
                "DesignEng/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}