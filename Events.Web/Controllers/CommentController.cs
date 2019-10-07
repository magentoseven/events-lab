using Events.Data;
using Events.Web.Repository;
using System.Web.Mvc;

namespace Events.Web.Controllers
{
    public class CommentController : BaseController
    {
        private ICommentRepository commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        // GET: Comments
        public PartialViewResult _GetForEvent(int eventId)
        {
            var comments = commentRepository.GetForEvent(eventId);

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
                commentRepository.Insert(comment);
                commentRepository.Save();
            }

            var comments = commentRepository.GetForEvent(comment.EventId);

            ViewBag.EventId = comment.EventId;
            return PartialView("_GetForEvent", comments);
        }
    }
}