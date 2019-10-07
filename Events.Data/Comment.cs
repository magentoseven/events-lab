using System;
using System.ComponentModel.DataAnnotations;

namespace Events.Data
{
    public class Comment
    {
        public Comment()
        {
            Date = DateTime.Now;
        }

        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        [Required]
        public int EventId { get; set; }

        public virtual Event Event { get; set; }
    }
}
