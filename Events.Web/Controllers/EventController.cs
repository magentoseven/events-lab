using Events.Data;
using Events.Web.Models;
using Events.Web.Extensions;
using System.Web.Mvc;
using System.Web.Routing;
using Events.Web.Repository;

namespace Events.Web.Controllers
{
    [Authorize]
    public class EventController : BaseController
    {
        private IEventRepository eventRepository;

        public EventController(IEventRepository eventRepository)
        {
            this.eventRepository = eventRepository;
        }

        public ActionResult Index()
        {
            return RedirectToAction("My");
        }

        [AllowAnonymous]
        public PartialViewResult _PublicEvents()
        {
            var publicEvents = eventRepository.GetUpcomingPassendEvents(null);
            return PartialView("_UpcomingPassedEvents", publicEvents);
        }

        public ActionResult My()
        {
            var myEvents = eventRepository.GetUpcomingPassendEvents(UserId);
            ViewBag.UserId = UserId.ToString();
            return View(myEvents);
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var eventDetails = eventRepository.GetEventDetails(id);
            ViewBag.CanEdit = (IsAdmin || eventDetails.AuthorId == UserId) && (UserId != null);
            return View(eventDetails);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Title = "Create New Event";
            ViewBag.SubmitButtonText = "Save";
            return View("_EditorForm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventEditorFormViewModel eventViewModel)
        {
            if (eventViewModel != null && ModelState.IsValid)
            {
                var newEvent = new Event()
                {
                    AuthorId = UserId
                };
                eventViewModel.ToModel(newEvent);
                eventRepository.Insert(newEvent);
                eventRepository.Save();

                this.AddNotification("Event created.", NotificationType.SUCCESS);
                return RedirectToAction("Details", new RouteValueDictionary(
                new { controller = "Event", action = "Details", id = newEvent.Id }));
            }

            ViewBag.Title = "Create New Event";
            ViewBag.SubmitButtonText = "Save";
            return View("_EditorForm");
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            var eventToEdit = eventRepository.GetById(id);
            if (eventToEdit == null)
            {
                this.AddNotification(string.Format("Cannot edit event #{0}", id), NotificationType.ERROR);
                return RedirectToAction("My");
            }

            var eventViewModel = new EventEditorFormViewModel();
            eventViewModel.FromModel(eventToEdit);

            ViewBag.Title = "Edit Event";
            ViewBag.SubmitButtonText = "Save";
            return View("_EditorForm", eventViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EventEditorFormViewModel eventViewModel)
        {
            var eventToEdit = eventRepository.GetById(id);
            if (eventToEdit == null)
            {
                this.AddNotification(string.Format("Cannot edit event #{0}", id), NotificationType.ERROR);
                return RedirectToAction("My");
            }

            if (eventViewModel != null && ModelState.IsValid)
            {
                eventViewModel.ToModel(eventToEdit);
                eventRepository.Update(eventToEdit);
                eventRepository.Save();

                this.AddNotification("Event edited.", NotificationType.INFO);
                return RedirectToAction("Details", new RouteValueDictionary(
                new { controller = "Event", action = "Details", id = eventToEdit.Id }));
            }

            ViewBag.Title = "Edit Event";
            ViewBag.SubmitButtonText = "Save";
            return View("_EditorForm", eventViewModel);
        }

        [HttpGet]
        public ActionResult DeleteModal(int id)
        {
            var eventToDelete = eventRepository.GetById(id);
            if (eventToDelete == null)
            {
                return Content("");
            }

            ModalViewModel viewModel = new ModalViewModel()
            {
                Title = "Delete",
                Body = string.Format("Are you sure want to delete \"{0}\"?", eventToDelete.Title),
                FormAction = "/Event/Delete",
                Id = eventToDelete.Id
            };

            return PartialView("_Modal", viewModel);
        }

        [HttpPost]
        public ActionResult Delete(ModalViewModel viewModel)
        {
            var id = viewModel.Id;
            var eventToDelete = eventRepository.GetById(id);
            if (eventToDelete == null)
            {
                this.AddNotification(string.Format("Cannot delete event #{0}", id), NotificationType.ERROR);
                return RedirectToAction("My");
            }

            eventRepository.Delete(id);
            eventRepository.Save();
            this.AddNotification("Event deleted.", NotificationType.INFO);
            return RedirectToAction("My");
        }
    }
}