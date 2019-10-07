using Events.Data;
using Events.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace Events.Web.Repository
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public List<CommentViewModel> GetForEvent(int eventId)
        {
            var comments = _context.Comments
                .Where(c => c.EventId == eventId)
                .OrderBy(c => c.Date)
                .Select(CommentViewModel.ViewModel)
                .ToList();

            return comments;
        }
    }
}