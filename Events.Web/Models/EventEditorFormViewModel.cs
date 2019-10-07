using Events.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace Events.Web.Models
{
    public class EventEditorFormViewModel : IViewModel<Event>
    {
        [Required(ErrorMessage = "Event title is required.")]
        [StringLength(200, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = 1)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Date and Time")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy HH:mm}")]
        public DateTime StartDateTime { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Location")]
        [MaxLength(200)]
        public string Location { get; set; }

        [Display(Name = "Is Public?")]
        public bool IsPublic { get; set; }

        public void ToModel(Event model)
        {
            model.Title = Title;
            model.StartDateTime = StartDateTime;
            model.Description = Description;
            model.Location = Location;
            model.IsPublic = IsPublic;
        }

        public void FromModel(Event model)
        {
            Title = model.Title;
            StartDateTime = model.StartDateTime;
            Description = model.Description;
            Location = model.Location;
            IsPublic = model.IsPublic;
        }
    }
}