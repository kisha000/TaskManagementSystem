using System.Net;
using System.Web.Mvc;
using TaskManagementSystem.DAL.Repositories;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Areas.Admin.Controllers
{
    public class RoleController : Controller
    {
        private readonly EmployeeRepository employeeRepository;
        private readonly UserRoleRepository userRoleRepository;

        public RoleController()
        {
            employeeRepository = new EmployeeRepository();
            userRoleRepository = new UserRoleRepository();
        }

        public ActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRole(UserRole role)
        {
            if (ModelState.IsValid)
            {
                userRoleRepository.AddRole(role);
                TempData["ModalMessage"] = "Role added successfully!";
                return RedirectToAction("AddRole", new { area = "Admin" });
            }

            return View(role);
        }
        public ActionResult AssignRole()
        {
            var employees = employeeRepository.GetAllEmployees();
            var roles = userRoleRepository.GetAllRoles();

            ViewBag.EmployeeList = new SelectList(employees, "EmployeeId", "EmployeeName");
            ViewBag.RoleList = new SelectList(roles, "RoleId", "RoleName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignRole(int employeeId, int? roleId)
        {
            if (employeeId <= 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var employee = employeeRepository.GetEmployeeById(employeeId);
            if (employee == null)
                return HttpNotFound();

            if (!roleId.HasValue || roleId.Value <= 0)
            {
                TempData["PopupMessage"] = "Please select a role.";
            }
            else
            {
                employee.RoleId = roleId.Value;
                employeeRepository.UpdateEmployee(employee);
                TempData["PopupMessage"] = "Role assigned successfully!";
            }

            return RedirectToAction("AssignRole");
        }
    }
}