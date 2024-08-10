using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using TaskManagementSystem.DAL.Repositories;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Areas.Admin.Controllers
{
    public class TaskController : Controller
    {
        private readonly TaskRepository taskRepository;
        public TaskController()
        {
            taskRepository = new TaskRepository();
        }

        // GET: Task
        public ActionResult Index()
        {
            List<Task> tasks = taskRepository.GetAllTasks();
            return View(tasks);
        }

        // GET: Task/Details/5
        public ActionResult Details(int id)
        {
            Task task = taskRepository.GetTaskById(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        public ActionResult Create()
        {
            var projectRepository = new ProjectRepository();
            var projects = projectRepository.GetAllProjects();
            List<SelectListItem> projectList = new List<SelectListItem>();

            foreach (var project in projects)
            {
                projectList.Add(new SelectListItem { Value = project.ProjectId.ToString(), Text = project.ProjectName });
            }

            var employeeRepository = new EmployeeRepository();
            var employees = employeeRepository.GetAllEmployees();
            List<SelectListItem> employeeList = new List<SelectListItem>();

            foreach (var employee in employees)
            {
                employeeList.Add(new SelectListItem { Value = employee.EmployeeId.ToString(), Text = employee.EmployeeName });
            }

            ViewBag.ProjectList = projectList;
            ViewBag.EmployeeList = employeeList;

            int newTaskId = taskRepository.GetNextTaskId();
            string taskName = $"TMS-{newTaskId.ToString("D3")}";

            var newTask = new Task
            {
                TaskName = taskName,
            };

            return View(newTask);
        }


        [HttpPost]
        public ActionResult Create(Task task, int? employeeId, IEnumerable<HttpPostedFileBase> attachments)
        {
            if (ModelState.IsValid)
            {
                task.EmployeeId = employeeId;
                string attachmentsFolderPath = Server.MapPath("~/Files/" + task.TaskName + "-Attachments");
                if (!Directory.Exists(attachmentsFolderPath))
                {
                    Directory.CreateDirectory(attachmentsFolderPath);
                }

                int counter = 1;
                foreach (var file in attachments)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        string originalFileName = Path.GetFileNameWithoutExtension(file.FileName);
                        string fileExtension = Path.GetExtension(file.FileName);

                        // Generate a unique file name based on original name, timestamp, and counter
                        string uniqueFileName = $"{originalFileName}_{DateTime.Now:ddMMyyyyHHmmss}_{counter}{fileExtension}";

                        string filePath = Path.Combine(attachmentsFolderPath, uniqueFileName);
                        file.SaveAs(filePath);

                        counter++;
                    }
                }

                taskRepository.AddTask(task);
                TempData["ModalMessage"] = "Task added successfully.";
                return RedirectToAction("Create");
            }
            else
            {
                TempData["ModalMessage"] = "Please fill in all the required fields.";
            }

            return View(task);
        }

        // GET: Task/Edit/5
        public ActionResult Edit(int id)
        {
            Task task = taskRepository.GetTaskById(id);

            var projectRepository = new ProjectRepository();
            var employeeRepository = new EmployeeRepository();

            List<Project> projects = projectRepository.GetAllProjects();
            List<Employee> employees = employeeRepository.GetAllEmployees();

            List<SelectListItem> projectList = new List<SelectListItem>();
            List<SelectListItem> employeeList = new List<SelectListItem>();

            foreach (var project in projects)
            {
                projectList.Add(new SelectListItem { Value = project.ProjectId.ToString(), Text = project.ProjectName });
            }

            foreach (var employee in employees)
            {
                employeeList.Add(new SelectListItem { Value = employee.EmployeeId.ToString(), Text = employee.EmployeeName });
            }

            ViewBag.ProjectList = projectList;
            ViewBag.EmployeeList = employeeList;
            if (task == null)
            {
                return HttpNotFound();
            }
            TempData["TaskId"] = id;
            return PartialView("Edit", task);
        }

        // POST: Task/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Task task)
        {
            if (ModelState.IsValid)
            {
                string TaskIdString = TempData["TaskId"].ToString();
                if (int.TryParse(TaskIdString, out int taskId))
                {
                    taskRepository.UpdateTask(taskId, task);
                }
                TempData["ModalMessage"] = "Task details updated successfully!";
                TempData["ClearFields"] = true;
            }
            else
            {
                TempData["ModalMessage"] = "Update failed. Please check the input data and try again.";
            }

            return RedirectToAction("Manage");
        }


        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            Task task = taskRepository.GetTaskById(id);

            if (task == null)
            {
                return HttpNotFound();
            }

            string deleteMessage = "Are you sure you want to delete this task?";
            ViewBag.DeleteMessage = deleteMessage;
            ViewBag.FormAction = "Delete"; // Action name
            ViewBag.FormController = "Task"; // Controller name
            ViewBag.FormRouteValues = new { id = id }; // Route values
            TempData["TaskId"] = id;
            return PartialView("_DeleteModal");
        }

        // POST: Employee/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete()
        {
            string taskIdString = TempData["TaskId"].ToString();
            int taskId;
            if (int.TryParse(taskIdString, out taskId))
            {
                taskRepository.DeleteTask(taskId);
                TempData["ModalMessage"] = "Task deleted successfully!";
            }
            else
            {
                TempData["ModalMessage"] = "Error while deleting Task";
            }

            return RedirectToAction("Manage");
        }

        public ActionResult Manage()
        {
            var projects = taskRepository.GetAllTasks();
            return View(projects);
        }
    }
}