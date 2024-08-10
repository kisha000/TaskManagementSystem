using System.Web.Mvc;

namespace TaskManagementSystem.Areas.User
{
    public class UserAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "User";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "User_Homepage",
                "User/Homepage",
                new { controller = "Homepage", action = "Index" }
            );
            context.MapRoute(
               "User_Project_Homepage",
               "User/Project",
               new { controller = "Project", action = "ViewProjects" }
           );
            context.MapRoute(
                "User_default",
                "User/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}