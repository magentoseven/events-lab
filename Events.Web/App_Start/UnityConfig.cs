using Events.Web.Repository;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace Events.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IEventRepository, EventRepository>();
            container.RegisterType<ICommentRepository, CommentRepository>();


            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}