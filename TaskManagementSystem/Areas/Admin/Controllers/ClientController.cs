using System.Web.Mvc;
using TaskManagementSystem.DAL.Repositories;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Areas.Admin.Controllers
{
    public class ClientController : Controller
    {
        private readonly ClientRepository clientRepository;

        public ClientController()
        {
            clientRepository = new ClientRepository();
        }

        // GET: Client/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
        [HttpPost]
        public ActionResult Create(Client client)
        {
            if (ModelState.IsValid)
            {
                clientRepository.AddClient(client);
                TempData["ModalMessage"] = "Client added successfully!";
            }
            return View(client);
        }

        public ActionResult Manage()
        {
            var projects = clientRepository.GetAllClients();
            return View(projects);
        }
        // GET: Client/Edit/5
        public ActionResult Edit(int id)
        {
            Client client = clientRepository.GetClientById(id);

            if (client == null)
            {
                return HttpNotFound();
            }

            return PartialView("Edit", client);
        }

        // POST: Client/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Client client)
        {
            if (ModelState.IsValid)
            {
                clientRepository.UpdateClient(client);
                TempData["ModalMessage"] = "Client details updated successfully!";
                TempData["ClearFields"] = true;
            }
            else
            {
                TempData["ModalMessage"] = "Update failed. Please check the input data and try again.";
            }

            return RedirectToAction("Manage");
        }

        // GET: Client/Delete/5
        public ActionResult Delete(int id)
        {
            Client client = clientRepository.GetClientById(id);

            if (client == null)
            {
                return HttpNotFound();
            }

            string deleteMessage = "Are you sure you want to delete this client?";
            ViewBag.DeleteMessage = deleteMessage;
            ViewBag.FormAction = "Delete"; // Action name
            ViewBag.FormController = "Client"; // Controller name
            ViewBag.FormRouteValues = new { id = id }; // Route values
            TempData["ClientID"] = id;
            return PartialView("_DeleteModal");
        }

        // POST: Client/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Client client)
        {
            string clientIdString = TempData["ClientID"].ToString();
            int clientId;
            if (int.TryParse(clientIdString, out clientId))
            {
                clientRepository.DeleteClient(clientId);
                TempData["ModalMessage"] = "Client deleted successfully!";
            }
            else
            {
                TempData["ModalMessage"] = "Error: Invalid client ID.";
            }

            return RedirectToAction("Manage");
        }


    }
}