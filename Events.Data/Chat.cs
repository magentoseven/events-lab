using System;
using System.ComponentModel.DataAnnotations;

namespace Events.Data
{
    public class Chat
    {
        public Chat()
        {
            SentTime = DateTime.Now;
        }

        public int Id { get; set; }

        [Required]
        public DateTime SentTime { get; set; }

        [Required]
        [MaxLength(200)]
        public string Text { get; set; }

        public string UserName { get; set; }

    }
}
