using System.Web.Mvc;

namespace SkinnedWebChat.Bot.Areas.Bot
{
    public class BotAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Bot";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Bot_default",
                "Bot/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}