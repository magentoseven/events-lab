using Events.Data;
using Events.Web.Models;

namespace Events.Web.Repositories
{
    public interface IEventRepository
    {
        Event GetById(int id, string authorId = null, bool isAdmin = false);
        void Insert(Event model);
        void Update(Event model);
        Event Delete(Event model);
        void Save();
        UpcomingPassedEventsViewModel GetUpcomingAndPassed(string authorId = null, bool isAdmin = false);
    }
}
