using Events.Data;
using Events.Web.Repository;
using System.Web.Mvc;

namespace Events.Web.Controllers
{
    public class CommentController : BaseController
    {
        CommentRepository commentRepo = new CommentRepository();

        // GET: Comments
        public PartialViewResult _GetForEvent(int eventId)
        {
            var comments = commentRepo.GetForEvent(eventId);

            ViewBag.EventId = eventId;
            return PartialView("_GetForEvent", comments);
        }

        public PartialViewResult _EditorForm(int eventId)
        {
            var comment = new Comment
            {
                AuthorId = UserId,
                EventId = eventId
            };

            ViewBag.EventId = eventId;
            return PartialView("_EditorForm", comment);
        }

        [ValidateAntiForgeryToken]
        public PartialViewResult _Submit(Comment comment)
        {
            if (ModelState.IsValid)
            {
                commentRepo.Insert(comment);
                commentRepo.Save();
            }

            var comments = commentRepo.GetForEvent(comment.EventId);

            ViewBag.EventId = comment.EventId;
            return PartialView("_GetForEvent", comments);
        }
    }
}