using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Events.Data.Repository
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public IEnumerable<TResult> GetPublicEvents<TResult>(Expression<Func<Event, TResult>> selector)
        {
            var db = _context;
            var events = db.Events
                .Where(e => e.IsPublic)
                .OrderBy(e => e.StartDateTime)
                .Select(selector).ToList();
            return events;
        }
    }
}
