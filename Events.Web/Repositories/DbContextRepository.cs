using Events.Data;

namespace Events.Web.Repositories
{
    public class DbContextRepository
    {
        protected ApplicationDbContext _context = new ApplicationDbContext();
    }
}