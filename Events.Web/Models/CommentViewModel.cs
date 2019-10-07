using Events.Data;
using System;
using System.Linq.Expressions;

namespace Events.Web.Models
{
    public class CommentViewModel
    {
        public string Text { get; set; }
        public string Author { get; set; }
        public DateTime CommentTime { get; set; }
        public static Expression<Func<Comment, CommentViewModel>> ViewModel
        {
            get
            {
                return c => new CommentViewModel()
                {
                    Text = c.Text,
                    Author = c.Author.FullName,
                    CommentTime = c.Date
                };
            }
        }

        public static string TimeDisplay(DateTime date)
        {
            var timeDisplay = "";
            TimeSpan interval = DateTime.Now - date;
            var days = Math.Abs(interval.Days);
            var hours = Math.Abs(interval.Hours);
            var minutes = Math.Abs(interval.Minutes);
            if (days > 1)
            {
                timeDisplay = string.Format("{0} days ago, at ", days) + date.ToString("dd-MMM-yyyy HH:mm");
            }
            else if (days == 1)
            {
                timeDisplay = "yesterday, at " + date.ToString("HH:mm");
            }
            else if (hours > 1)
            {
                timeDisplay = string.Format("{0} hours ago", hours);
            }
            else if (minutes > 1)
            {
                timeDisplay = string.Format("{0} minutes ago", minutes);
            }
            else
            {
                timeDisplay = "a moment ago";
            }
            return timeDisplay;
        }
    }
}