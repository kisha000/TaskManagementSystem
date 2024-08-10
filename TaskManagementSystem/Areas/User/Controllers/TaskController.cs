using System.IO;
using System.Linq;
using System.Web.Mvc;
using TaskManagementSystem.DAL.Repositories;

namespace TaskManagementSystem.Areas.User.Controllers
{
    public class TaskController : Controller
    {
        private readonly TaskRepository taskRepository;

        public TaskController()
        {
            taskRepository = new TaskRepository();
        }
        // GET: User/Task
        public ActionResult Index()
        {
            int employeeId = Common.SessionCookieManager.GetSessionValue<int>("EmployeeId");
            var tasks = taskRepository.GetTasksByEmployee(employeeId);
            return View(tasks);
        }
        public ActionResult TaskDetails(int taskId)
        {
            var task = taskRepository.GetTaskById(taskId);
            if (task == null)
            {
                return HttpNotFound();
            }

            string attachmentsFolderPath = Server.MapPath("~/Files/" + task.TaskName + "-Attachments");
            int attachmentCount = 0;
            if (Directory.Exists(attachmentsFolderPath))
            {
                string[] attachmentFileNames = Directory.GetFiles(attachmentsFolderPath)
                    .Select(Path.GetFileName)
                    .ToArray();

                attachmentCount = attachmentFileNames.Length;
                ViewBag.AttachmentFileNames = attachmentFileNames;
            }

            ViewBag.AttachmentCount = attachmentCount;

            return View(task);
        }
    }
}