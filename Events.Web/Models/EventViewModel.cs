using Events.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Events.Web.Models
{
    public class EventViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy - HH:mm}")]
        public DateTime StartDateTime { get; set; }

        public string Location { get; set; }

        public static Expression<Func<Event, EventViewModel>> ViewModel
        {
            get
            {
                return e => new EventViewModel()
                {
                    Id = e.Id,
                    Title = e.Title,
                    StartDateTime = e.StartDateTime,
                    Location = e.Location
                };
            }
        }

        public Event Model
        {
            get
            {
                return new Event
                {
                    Id = Id,
                    Title = Title,
                    StartDateTime = StartDateTime,
                    Location = Location
                };
            }
        }
    }
}