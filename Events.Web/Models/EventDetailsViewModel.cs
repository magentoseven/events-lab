using Events.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Events.Web.Models
{
    public class EventDetailsViewModel : EventViewModel
    {
        public string Description { get; set; }
        public string AuthorId { get; set; }
        public string Author { get; set; }
        public IEnumerable<CommentViewModel> Comments { get; set; }
        public new static Expression<Func<Event, EventDetailsViewModel>> ViewModel
        {
            get
            {
                return e => new EventDetailsViewModel()
                {
                    Id = e.Id,
                    Title = e.Title,
                    StartDateTime = e.StartDateTime,
                    Location = e.Location,
                    Description = e.Description,
                    Comments = e.Comments.AsQueryable().Select(CommentViewModel.ViewModel),
                    AuthorId = e.Author.Id,
                    Author = e.Author.FullName
                };
            }
        }
    }
}