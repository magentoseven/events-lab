namespace Events.Web.Repositories
{
    public class DbRepositories
    {
       public static IEventRepository DbEvent()
        {
            return EventRepository.GetInstance;
        }
    }
}