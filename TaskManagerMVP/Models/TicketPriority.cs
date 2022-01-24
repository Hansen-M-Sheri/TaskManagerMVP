using System.ComponentModel.DataAnnotations;

namespace TaskManagerMVP.Models
{
    public class TicketPriority
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Ticket Priority Name")]
        [Required(ErrorMessage = "Ticket Priority Name is required.")]
        [StringLength(TaskManagerConstants.MAX_PRIORITY_NAME_LENGTH, ErrorMessage = "Ticket Priority Name must be less than 50 characters")]
        public string Name { get; set; }

        [Display(Name = "Ticket Priority Description")]
        [Required(ErrorMessage = "Ticket Priority Description is required.")]
        [StringLength(TaskManagerConstants.MAX_PRIORITY_DESCRIPTION_LENGTH, ErrorMessage = "Ticket Priority Description must be less than 500 characters")]
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
