using System.Collections.Generic;
using System.Web.Mvc;
using TaskManagementSystem.DAL.Repositories;

namespace TaskManagementSystem.Areas.Admin.Controllers
{
    public class ReportController : Controller
    {
        private readonly ProjectRepository projectRepository;
        private readonly TaskRepository taskRepository;

        public ReportController()
        {
            projectRepository = new ProjectRepository();
            taskRepository = new TaskRepository();
        }

        // GET: Admin/Report/OpenProjects
        public ActionResult CurrentProjects()
        {
            var projects = projectRepository.GetAllOpenProjects();

            foreach (var project in projects)
            {
                var employees = projectRepository.GetEmployeesByProjectId(project.ProjectId);
                project.ProjectEmployees = employees;
            }

            return View(projects);
        }
        public ActionResult CurrentTasks()
        {
            var tasks = taskRepository.GetAllOpenTasks();
            Dictionary<string, List<TaskManagementSystem.Models.Task>> tasksByProject = new Dictionary<string, List<TaskManagementSystem.Models.Task>>();

            foreach (var task in tasks)
            {
                if (!tasksByProject.ContainsKey(task.ProjectName))
                {
                    tasksByProject[task.ProjectName] = new List<TaskManagementSystem.Models.Task>();
                }

                tasksByProject[task.ProjectName].Add(task);
            }

            ViewBag.TasksByProject = tasksByProject;
            return View();
        }

    }
}