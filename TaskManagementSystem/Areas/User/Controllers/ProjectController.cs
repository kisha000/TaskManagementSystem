using System.Collections.Generic;
using System.Web.Mvc;
using TaskManagementSystem.DAL.Repositories;

namespace TaskManagementSystem.Areas.User.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ProjectRepository projectRepository;
        private readonly ClientRepository clientRepository;
        private readonly EmployeeRepository employeeRepository;

        public ProjectController()
        {
            projectRepository = new ProjectRepository();
            clientRepository = new ClientRepository();
            employeeRepository = new EmployeeRepository();
        }

        #region Get ProjectsByEmployee
        // GET: Project
        public ActionResult ViewProjects()
        {
            int employeeId = Common.SessionCookieManager.GetSessionValue<int>("EmployeeId");

            var projects = projectRepository.GetProjectsByEmployee(employeeId);

            var actionList = new List<SelectListItem>
            {
                new SelectListItem { Text = "Post Comment", Value = "PostComment" },
                new SelectListItem { Text = "View Members", Value = "GetProjectMembers" }
            };

            ViewBag.ActionList = new SelectList(actionList, "Value", "Text");

            return View(projects);
        }

        #endregion

        public ActionResult GetProjectMembers(int projectId)
        {
            var employees = projectRepository.GetEmployeesByProjectId(projectId);
            return PartialView("_ProjectMembers", employees);
        }
        public ActionResult Chat()
        {
            return View();
        }
        public ActionResult GetProjectDetails(int projectId)
        {
            var project = projectRepository.GetProjectById(projectId);

            if (project != null)
            {
                return Json(project, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null);
            }
        }
    }
}