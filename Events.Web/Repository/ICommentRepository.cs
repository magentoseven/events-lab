using Events.Data;
using Events.Web.Models;
using System.Collections.Generic;

namespace Events.Web.Repository
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        List<CommentViewModel> GetForEvent(int eventId);
    }
}
