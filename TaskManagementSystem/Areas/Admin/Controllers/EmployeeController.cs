using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using TaskManagementSystem.DAL.Repositories;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Areas.Admin.Controllers
{
    public class EmployeeController : Controller
    {
        #region Global Declaration

        private readonly EmployeeRepository employeeRepository;
        private readonly ProjectRepository projectRepository;
        private readonly UserRoleRepository userRoleRepository;

        public EmployeeController()
        {
            employeeRepository = new EmployeeRepository();
            projectRepository = new ProjectRepository();
            userRoleRepository = new UserRoleRepository();
        }

        #endregion

        #region AddEmployee

        // GET: Employee/Add
        public ActionResult AddEmployee()
        {
            List<UserRole> roles = userRoleRepository.GetAllRoles();

            ViewBag.RoleList = new SelectList(roles, "RoleId", "RoleName");

            return View();
        }

        // POST: Employee/AddEmployee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEmployee(Employee employee)
        {
            if (employeeRepository.CheckEmailExists(employee.Email))
            {
                TempData["ModalMessage"] = "Employee already exists.";
                return RedirectToAction("AddEmployee");
            }
            else
            {
                // Generate a random password
                string randomPassword = GenerateRandomPassword();

                employee.Password = Common.HashHelper.HashPassword(randomPassword);

                employeeRepository.AddEmployee(employee);

                //SendWelcomeEmail(employee, randomPassword);

                TempData["ModalMessage"] = "Employee added successfully!";

                ModelState.Clear();
            }
            return RedirectToAction("AddEmployee");
        }

        #region Generate a random password and Send Email

        // Method to generate a random password
        private string GenerateRandomPassword()
        {
            const string charset = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()-_=+";
            Random random = new Random();
            return new string(Enumerable.Repeat(charset, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        //  send  email to the new employee
        public void SendWelcomeEmail(Employee employee, string defaultPassword)
        {
            string subject = "Welcome to Task Management System!";
            string body = $"Hello {employee.EmployeeName},<br/><br/>" +
                          $"Welcome to Task Management System!<br/><br/>" +
                          $"Please review your details:<br/><br/>" +
                          $"Name: {employee.EmployeeName}<br/>" +
                          $"Address: {employee.Address}<br/>" +
                          $"Email: {employee.Email}<br/>" +
                          $"Phone Number: {employee.PhoneNumber}<br/><br/>" +
                          $"Your default password is: {defaultPassword}<br/><br/>" +
                          $"Please click on the link below to change your password:<br/>" +
                          $"<a href='{{changePasswordLink}}'>Change Password</a>";

            body = body.Replace("{changePasswordLink}", $"{Request.Url.Scheme}://{Request.Url.Authority}/Account/ResetPassword?email={employee.Email}");

            //MailMessage mail = new MailMessage("suryakanta@concept.co.in", employee.Email);
            //mail.Subject = subject;
            //mail.Body = body;
            //mail.IsBodyHtml = true;

            //using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 25))
            //{
            //    smtp.Host = "mail.concept.co.in";
            //    smtp.Port = 25;

            //    smtp.Send(mail);
            //}

            MailMessage mail = new MailMessage("suryakantbarik2@gmail.com", employee.Email);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)) // Use port 587 for TLS
            {
                smtp.Credentials = new NetworkCredential("suryakantbarik2@gmail.com", "897Aiwu$2$@$k");
                smtp.EnableSsl = true; 

                // Send the email
                smtp.Send(mail);
            }
        }
        #endregion

        #endregion AddEmployee

        #region EditEmployee

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            List<Project> projects = projectRepository.GetAllProjects();
            List<UserRole> userRoles = userRoleRepository.GetAllRoles();

            List<SelectListItem> projectList = new List<SelectListItem>();
            List<SelectListItem> userRoleList = new List<SelectListItem>();

            foreach (var project in projects)
            {
                projectList.Add(new SelectListItem { Value = project.ProjectId.ToString(), Text = project.ProjectName });
            }

            foreach (var userRole in userRoles)
            {
                userRoleList.Add(new SelectListItem { Value = userRole.RoleId.ToString(), Text = userRole.RoleName });
            }

            ViewBag.ProjectList = projectList;
            ViewBag.UserRoleList = userRoleList;

            Employee employee = employeeRepository.GetEmployeeById(id);

            ViewBag.SelectedProjectId = employee.ProjectId;
            ViewBag.SelectedRoleId = employee.RoleId;

            if (employee == null)
            {
                return HttpNotFound();
            }

            return PartialView("Edit", employee);
        }

        // POST: Employee/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                employeeRepository.UpdateEmployee(employee);
                TempData["ModalMessage"] = "Employee details updated successfully!";
                TempData["ClearFields"] = true;
            }
            else
            {
                TempData["ModalMessage"] = "Update failed. Please check the input data and try again.";
            }

            return RedirectToAction("Manage");
        }

        #endregion

        #region ViewEmployee

        // GET: Employee/Manage
        public ActionResult Manage()
        {
            List<Employee> employees = employeeRepository.GetAllEmployees();
            return View(employees);
        }

        #endregion

        #region DeleteEmployee

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            Employee employee = employeeRepository.GetEmployeeById(id);

            if (employee == null)
            {
                return HttpNotFound();
            }

            string deleteMessage = "Are you sure you want to delete this employee?";
            ViewBag.DeleteMessage = deleteMessage;
            ViewBag.FormAction = "Delete"; // Action name
            ViewBag.FormController = "Employee"; // Controller name
            ViewBag.FormRouteValues = new { id = id }; // Route values
            TempData["EmployeeID"] = id;
            return PartialView("_DeleteModal");
        }

        // POST: Employee/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete()
        {
            string employeeIdString = TempData["EmployeeID"].ToString();
            int employeeId;
            if (int.TryParse(employeeIdString, out employeeId))
            {
                employeeRepository.DeleteEmployee(employeeId);
                TempData["ModalMessage"] = "Employee deleted successfully!";
            }
            else
            {
                TempData["ModalMessage"] = "Error while deleting Employee";
            }

            return RedirectToAction("Manage");
        }
        #endregion
    }
}