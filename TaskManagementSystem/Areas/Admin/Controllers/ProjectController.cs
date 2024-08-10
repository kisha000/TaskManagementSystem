using System.Collections.Generic;
using System.Web.Mvc;
using TaskManagementSystem.DAL.Repositories;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Areas.Admin.Controllers
{
    public class ProjectController : Controller
    {
        #region Global Declarations

        private readonly ProjectRepository projectRepository;
        private readonly ClientRepository clientRepository;
        private readonly EmployeeRepository employeeRepository;

        public ProjectController()
        {
            projectRepository = new ProjectRepository();
            clientRepository = new ClientRepository();
            employeeRepository = new EmployeeRepository();
        }

        #endregion

        #region Get AllProjects

        // GET: Project
        public ActionResult Manage()
        {
            var projects = projectRepository.GetAllProjects();
            return View(projects);
        }
        #endregion  

        #region Create Project
        // GET: Project/Create
        public ActionResult Create()
        {
            List<Client> clients = clientRepository.GetAllClients();
            List<Employee> employees = employeeRepository.GetAllEmployees();

            List<SelectListItem> clientList = new List<SelectListItem>();
            List<SelectListItem> employeeList = new List<SelectListItem>();

            foreach (var client in clients)
            {
                clientList.Add(new SelectListItem { Value = client.ClientId.ToString(), Text = client.ClientName });
            }

            foreach (var employee in employees)
            {
                employeeList.Add(new SelectListItem { Value = employee.EmployeeId.ToString(), Text = employee.EmployeeName });
            }

            ViewBag.ClientList = clientList;
            ViewBag.EmployeeList = employeeList;

            return View();
        }

        [HttpPost]
        public ActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(project.ProjectName) || string.IsNullOrWhiteSpace(project.Description))
                {
                    TempData["ModalMessage"] = "Please fill in all required fields.";
                }
                else
                {
                    projectRepository.AddProject(project);
                    TempData["ModalMessage"] = "Project created successfully!";
                    return RedirectToAction("Create");
                }
            }
            return View(project);
        }
        #endregion

        #region Edit Projects
        // GET: Project/Edit/5
        public ActionResult Edit(int id)
        {
            Project project = projectRepository.GetProjectById(id);
            List<Client> clients = clientRepository.GetAllClients();
            List<SelectListItem> clientList = new List<SelectListItem>();

            foreach (var client in clients)
            {
                clientList.Add(new SelectListItem { Value = client.ClientId.ToString(), Text = client.ClientName });
            }

            ViewBag.ClientList = clientList;

            ViewBag.SelectedClientId = project.ClientId;

            TempData["ProjectId"] = project.ProjectId;

            if (project == null)
            {
                return HttpNotFound();
            }

            return PartialView("Edit", project);
        }
        // POST: Project/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Project project)
        {
            if (ModelState.IsValid)
            {
                string projectIdString = TempData["ProjectId"].ToString();

                if (int.TryParse(projectIdString, out int projectId))
                {
                    projectRepository.UpdateProject(projectId, project);
                }
                TempData["ModalMessage"] = "Project details updated successfully!";
                TempData["ClearFields"] = true;
            }
            else
            {
                TempData["ModalMessage"] = "Update failed. Please check the input data and try again.";
            }

            return RedirectToAction("Manage");
        }

        #endregion

        #region Delete Projects
        // GET: Project/Delete/5
        public ActionResult Delete(int id)
        {
            Project project = projectRepository.GetProjectById(id);

            if (project == null)
            {
                return HttpNotFound();
            }

            string deleteMessage = "Are you sure you want to delete this Project?";
            ViewBag.DeleteMessage = deleteMessage;
            ViewBag.FormAction = "Delete"; // Action name
            ViewBag.FormController = "Project"; // Controller name
            ViewBag.FormRouteValues = new { id = id }; // Route values
            TempData["ProjectID"] = id;
            return PartialView("_DeleteModal");
        }

        // POST: Project/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete()
        {
            string projectIdString = TempData["ProjectID"].ToString();
            int projectId;
            if (int.TryParse(projectIdString, out projectId))
            {
                projectRepository.DeleteProject(projectId);
                TempData["ModalMessage"] = "Project deleted successfully!";
            }
            else
            {
                TempData["ModalMessage"] = "Error while deleting Project.";
            }

            return RedirectToAction("Manage");
        }
        #endregion

        // GET: Project/AddEmployeetoTheProject/5
        public ActionResult AddEmployeetoTheProject()
        {
            IEnumerable<Project> projects = projectRepository.GetAllProjects();

            if (projects == null)
            {
                ViewBag.ProjectList = new SelectList(new List<Project>(), "ProjectId", "ProjectName");
            }
            else
            {
                ViewBag.ProjectList = new SelectList(projects, "ProjectId", "ProjectName");
            }

            IEnumerable<Employee> employees = employeeRepository.GetAllEmployees();

            ViewBag.EmployeeList = new SelectList(employees, "EmployeeId", "EmployeeName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEmployeetoTheProject(int projectId, string[] selectedEmployees)
        {
            if (ModelState.IsValid)
            {
                string employeeIds = string.Join(",", selectedEmployees);

                Project project = projectRepository.GetProjectById(projectId);

                if (project != null)
                {
                    string projectName = project.ProjectName;

                    projectRepository.InsertProjectEmployees(projectId, employeeIds);

                    int numEmployeesAdded = selectedEmployees.Length;
                    TempData["ModalMessage"] = $"{numEmployeesAdded} employee(s) added to project '{projectName}' successfully.";
                    return RedirectToAction("AddEmployeetoTheProject", "Project");
                }
                else
                {
                    TempData["ModalMessage"] = "Project not found.";
                    return RedirectToAction("AddEmployeetoTheProject", "Project");
                }
            }

            TempData["ModalMessage"] = "Failed to add employees to the project.";
            return RedirectToAction("AddEmployeetoTheProject", "Project");
        }
    }
}