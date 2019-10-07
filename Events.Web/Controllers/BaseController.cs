using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Events.Data;
using System.Linq;

namespace Events.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (User != null)
            {
                var context = new ApplicationDbContext();
                var username = User.Identity.Name;

                if (!string.IsNullOrEmpty(username))
                {
                    var user = context.Users.SingleOrDefault(u => u.UserName == username);
                    ViewData.Add("UserFullName", user.FullName);
                    ViewData.Add("UserRole", IsAdmin ? "Administrator" : "");
                }
            }
            base.OnActionExecuted(filterContext);
        }

        public bool IsAdmin
        {
            get
            {
                var currentUserId = UserId;
                var isAdmin = currentUserId != null && User.IsInRole("Administrator");
                return isAdmin;
            }
        }

        public string UserId => User.Identity.GetUserId();
    }
}