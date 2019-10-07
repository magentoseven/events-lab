using System;
using System.Linq;
using Events.Data;
using Events.Web.Models;

namespace Events.Web.Repositories
{
    public sealed class EventRepository : DbContextRepository, IEventRepository
    {
        private EventRepository() { }

        private static readonly Lazy<EventRepository> instance = new Lazy<EventRepository>(() => new EventRepository());
        public static EventRepository GetInstance => instance.Value;

        public Event GetById(int id, string authorId = null, bool isAdmin = false)
        {
            var eventToLoad = _context.Events
                .Where(e => e.Id == id)
                .Where(e => e.IsPublic || isAdmin || (e.AuthorId != null && e.AuthorId == authorId))
                .FirstOrDefault(e => e.AuthorId == authorId || isAdmin);
            return eventToLoad;
        }

        public void Insert(Event model)
        {
            _context.Events.Add(model);
        }

        public void Update(Event model)
        {
            _context.Entry(model).State = System.Data.Entity.EntityState.Modified;
        }

        public Event Delete(Event model)
        {
            _context.Events.Remove(model);
            return model;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public UpcomingPassedEventsViewModel GetUpcomingAndPassed(string authorId = null, bool isAdmin = false)
        {
            var events = _context.Events
                .Where(e => authorId != null ? e.AuthorId == authorId : e.IsPublic || (e.AuthorId == authorId) || isAdmin)
                .OrderBy(e => e.StartDateTime)
                .Select(EventViewModel.ViewModel);

            var upcomingEvents = events.Where(e => e.StartDateTime > DateTime.Now);
            var passedEvents = events.Where(e => e.StartDateTime <= DateTime.Now);

            return new UpcomingPassedEventsViewModel()
            {
                UpcomingEvents = upcomingEvents,
                PassedEvents = passedEvents
            };
        }
    }
}