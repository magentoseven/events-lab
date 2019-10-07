using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Events.Data
{
    public class Event
    {
        public Event()
        {
            IsPublic = true;
            StartDateTime = DateTime.Now;
            Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public DateTime StartDateTime { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public string Description { get; set; }

        [MaxLength(200)]
        public string Location { get; set; }

        public bool IsPublic { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
