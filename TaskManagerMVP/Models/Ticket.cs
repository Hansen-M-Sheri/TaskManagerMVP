using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagerMVP.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Ticket Name")]
        [Required(ErrorMessage = "Ticket Name is required.")]
        [StringLength(TaskManagerConstants.MAX_TICKET_NAME_LENGTH, ErrorMessage = "Ticket Name must be less than 50 characters")]
        public string Name { get; set; }

        [Display(Name = "Ticket Description")]
        [Required(ErrorMessage = "Ticket Description is required.")]
        [StringLength(TaskManagerConstants.MAX_TICKET_DESCRIPTION_LENGTH, ErrorMessage = "Ticket Description must be less than 500 characters")]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        
        [Display(Name = "User")]
        [Required(ErrorMessage = "User Id is required to map the contact to a user correctly")]
        public string UserId { get; set; }

        [Display(Name = "Project")]
        [Required(ErrorMessage = "Project is required")]
        public int ProjectId { get; set; }
        
        [Display(Name = "Ticket Type")]
        public int TicketTypeId { get; set; }
        
        [Display(Name = "Status")]
        public int TicketStatusId { get; set; }
        
        [Display(Name = "Priority")]
        public int TicketPriorityId { get; set; }
        [DefaultValue(true)]
        public bool IsActive { get; set; }

        //Hack-made all nav properties nullable to avoid modelState.isValid = false in create/edit tickets not sure how to do it correctly
        //Navigation Properties
        public ApplicationUser? User { get; set; }
        public Project? Project { get; set; }
        public TicketType? TicketType { get; set; }
        public TicketStatus? TicketStatus { get; set; }
        public TicketPriority? TicketPriority { get; set; }
    }
}
