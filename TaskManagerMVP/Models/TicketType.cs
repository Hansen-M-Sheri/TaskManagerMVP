using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerMVP.Models
{
    public class TicketType
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Ticket Type Name")]
        [Required(ErrorMessage = "Ticket Type Name is required.")]
        [StringLength(TaskManagerConstants.MAX_STATUS_NAME_LENGTH, ErrorMessage = "Ticket Type Name must be less than 50 characters")]
        public string Name { get; set; }

        [Display(Name = "Ticket Type Description")]
        [Required(ErrorMessage = "Ticket Type Description is required.")]
        [StringLength(TaskManagerConstants.MAX_STATUS_DESCRIPTION_LENGTH, ErrorMessage = "Ticket Type Description must be less than 500 characters")]
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
