using System.Linq;
using System.Web.Mvc;
using TaskManagementSystem.DAL.Repositories;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Areas.User.Controllers
{
    public class HomepageController : Controller
    {
        private readonly ProjectRepository projectRepository;
        private readonly TaskRepository taskRepository;

        public HomepageController()
        {
            projectRepository = new ProjectRepository();
            taskRepository = new TaskRepository();
        }

        public ActionResult Index()
        {
            int employeeId = Common.SessionCookieManager.GetSessionValue<int>("EmployeeId");

            int myProjectsCount = projectRepository.GetProjectsByEmployee(employeeId).Count();
            int myTasksCount = taskRepository.GetTasksByEmployee(employeeId).Count();

            var viewModel = new Dashboard
            {
                TotalOpenProjects = myProjectsCount,
                TotalTasksAssigned = myTasksCount,
            };

            return View(viewModel);
        }


    }
}