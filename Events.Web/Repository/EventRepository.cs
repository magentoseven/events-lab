using Events.Data;
using Events.Web.Models;
using System;
using System.Linq;

namespace Events.Web.Repository
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public UpcomingPassedEventsViewModel GetUpcomingPassendEvents(string userId = null)
        {
            var events = _context.Events
                .Where(e => userId == null ? e.IsPublic : e.AuthorId == userId)
                .OrderBy(e => e.StartDateTime)
                .Select(EventViewModel.ViewModel);

            var upcomingEvents = events.Where(e => e.StartDateTime > DateTime.Now);
            var passedEvents = events.Where(e => e.StartDateTime <= DateTime.Now);

            var upcomingPassedEvents = new UpcomingPassedEventsViewModel()
            {
                UpcomingEvents = upcomingEvents,
                PassedEvents = passedEvents
            };

            return upcomingPassedEvents;
        }

        public EventDetailsViewModel GetEventDetails(int id)
        {
            var eventDetails = _context.Events
                .Where(e => e.Id == id)
                .Select(EventDetailsViewModel.ViewModel)
                .FirstOrDefault();

            return eventDetails;
        }
    }
}
