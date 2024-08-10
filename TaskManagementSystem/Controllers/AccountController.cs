using System.Collections.Generic;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using TaskManagementSystem.DAL.Repositories;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        #region Global Decalrations

        private readonly EmployeeRepository employeeRepository = new EmployeeRepository();
        private readonly AccountRepository accountRepository = new AccountRepository();
        #endregion

        #region Login/Logout Management

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Employee employee)
        {
            Employee dbEmployee = employeeRepository.GetEmployeeByEmail(employee.Email);
            bool authenticated = false;

            if (dbEmployee != null)
            {
                if (dbEmployee.Password == Common.HashHelper.HashPassword(employee.Password))
                {
                    authenticated = true;
                }
            }

            if (authenticated)
            {
                Dictionary<string, object> sessionValues = new Dictionary<string, object>
                {
                    { "EmployeeId", dbEmployee.EmployeeId },
                    { "EmployeeName", dbEmployee.EmployeeName },
                    { "RoleName", dbEmployee.RoleName }
                };
                Common.SessionCookieManager.SetSessionValues(sessionValues);

                if (dbEmployee.RoleName == "Admin")
                {
                    return RedirectToAction("Index", "Homepage", new { area = "Admin" });
                }
                else if (dbEmployee.RoleName == "User")
                {
                    return RedirectToAction("Index", "Homepage", new { area = "User" });
                }
                else
                {
                    TempData["ModalMessage"] = "Invalid Credentials";
                    return View(employee);
                }
            }
            else
            {
                TempData["ModalMessage"] = "Invalid Credentials";
                return View(employee);
            }
        }

        public ActionResult Logout()
        {
            Common.SessionCookieManager.AbandonSession();
            Common.SessionCookieManager.ClearSession();
            return RedirectToAction("Login", "Account");
        }


        #endregion

        #region ForgotPassword

        public ActionResult ForgotPassword()
        {
            Employee employee = new Employee();
            return PartialView("ForgotPassword", employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(string email)
        {
            if (!employeeRepository.CheckEmailExists(email))
            {
                TempData["ModalMessage"] = "Email does not exist.";
                return RedirectToAction("Login");
            }

            string encryptedEmail = Common.EncryptionHelper.EncryptQueryString(email);
            string resetLink = $"{Request.Url.Scheme}://{Request.Url.Authority}/Account/ResetPassword?email={HttpUtility.UrlEncode(encryptedEmail)}";

            //SendResetEmail(email, resetLink);

            accountRepository.SetPasswordResetStatus(email, true);
            accountRepository.UpdateResetLinkTimestamp(email);

            TempData["ModalMessage"] = "Password reset instructions sent to your email.";
            return RedirectToAction("Login");
        }


        #region SendResetLink
        private void SendResetEmail(string email, string resetLink)
        {
            string from = "suryakanta@concept.co.in";
            using (MailMessage mail = new MailMessage(from, email))
            {
                mail.Subject = "Password Reset Link (For testing)";
                mail.Body = $"You have requested to reset your password. Click <a href='{resetLink}'>here</a> to reset your password.";
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("mail.concept.co.in", 25))
                {
                    smtp.Send(mail);
                }
            }
        }
        #endregion

        #endregion

        #region ResetPassword
        public ActionResult ResetPassword(string email)
        {
            string decryptedEmail = Common.EncryptionHelper.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString["email"].ToString()).Replace(" ", "+").Replace("%2b", "+").Replace("%2f", "/").Replace("%3d", "="));

            if (accountRepository.IsPasswordResetValid(decryptedEmail))
            {
                TempData["decryptedEmail"] = decryptedEmail;
                return View();
            }

            // Invalid or expired link
            TempData["ModalMessage"] = "The password reset link is invalid or expired.";
            return RedirectToAction("Login");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(string email, Employee employee)
        {
            if (employee.Password != employee.ConfirmPassword)
            {
                TempData["ModalMessage"] = "Passwords do not match.";
                return RedirectToAction("ResetPassword", new { email = email });
            }
            email = TempData["decryptedEmail"]?.ToString() ?? email;

            string hashedPassword = Common.HashHelper.HashPassword(employee.Password);

            accountRepository.UpdateEmployeePassword(email, hashedPassword);
            accountRepository.SetPasswordResetStatus(email, false);

            TempData["ModalMessage"] = "Password updated successfully.";

            ViewBag.RedirectTime = 5;
            return RedirectToAction("Login");
        }

        #endregion
    }
}
