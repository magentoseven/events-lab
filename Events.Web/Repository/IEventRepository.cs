using Events.Data;
using Events.Web.Models;

namespace Events.Web.Repository
{
    public interface IEventRepository : IGenericRepository<Event>
    {
        UpcomingPassedEventsViewModel GetUpcomingPassendEvents(string userId = null);

        EventDetailsViewModel GetEventDetails(int id);
    }
}