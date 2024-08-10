using System.Linq;
using System.Web.Mvc;
using TaskManagementSystem.DAL.Repositories;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Areas.Admin.Controllers
{
    public class HomepageController : Controller
    {
        #region Global Declaration

        private readonly ProjectRepository projectRepository;
        private readonly TaskRepository taskRepository;

        public HomepageController()
        {
            projectRepository = new ProjectRepository();
            taskRepository = new TaskRepository();
        }

        #endregion

        #region Index
        public ActionResult Index()
        {
            int totalOpenProjects = projectRepository.GetAllOpenProjects().Count();
            int totalTasksAssigned = taskRepository.GetAllOpenTasks().Count();

            var viewModel = new Dashboard
            {
                TotalOpenProjects = totalOpenProjects,
                TotalTasksAssigned = totalTasksAssigned
            };

            return View(viewModel);
        }
        #endregion
    }
}