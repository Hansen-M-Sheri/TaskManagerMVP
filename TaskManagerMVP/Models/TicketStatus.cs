using System.ComponentModel.DataAnnotations;

namespace TaskManagerMVP.Models
{
    public class TicketStatus
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Ticket Status Name")]
        [Required(ErrorMessage = "Task Status Name is required.")]
        [StringLength(TaskManagerConstants.MAX_STATUS_NAME_LENGTH, ErrorMessage = "Ticket Status Name must be less than 50 characters")]
        public string Name { get; set; }

        [Display(Name = "Ticket Status Description")]
        [Required(ErrorMessage = "Ticket Status Description is required.")]
        [StringLength(TaskManagerConstants.MAX_STATUS_DESCRIPTION_LENGTH, ErrorMessage = "Ticket Status Description must be less than 500 characters")]
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}

