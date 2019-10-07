using Events.Data;
using Events.Web.signalr.hubs;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using System;
using System.Linq;
using System.Web.Http;

namespace Events.Web.Controllers
{
    public class ChatController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Chat
        public IHttpActionResult Get()
        {
            var chatLogList = db.Chats
                .OrderBy(c => c.SentTime)
                .ToList();

            // Get the latest 20 chats.
            var chatLog = chatLogList.Skip(Math.Max(0, chatLogList.Count() - 20));

            return Ok(chatLog);
        }

        // POST: api/Chat
        public IHttpActionResult Post([FromBody]Chat chat)
        {
            var currentUserId = User.Identity.GetUserId();
            var userName = currentUserId == null ? "Anonymous" : User.Identity.GetUserName();
            chat.SentTime = DateTime.Now;
            chat.UserName = userName;

            // Save to database
            db.Chats.Add(chat);
            db.SaveChanges();

            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            hubContext.Clients.All.getChatLog();

            return Ok(chat);
        }
    }
}
