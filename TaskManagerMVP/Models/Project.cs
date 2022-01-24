using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerMVP.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Project Name")]
        [Required(ErrorMessage = "Project Name is required.")]
        [StringLength(TaskManagerConstants.MAX_PROJ_NAME_LENGTH, ErrorMessage = "Project Name must be less than 50 characters")]
        public string Name { get; set; }

        [Display(Name = "Project Description")]
        [Required(ErrorMessage = "Project Description is required.")]
        [StringLength(TaskManagerConstants.MAX_PROJ_DESCRIPTION_LENGTH, ErrorMessage = "Project Description must be less than 500 characters")]
        public string Description { get; set; }

        public bool IsActive { get; set; }

        //Navigation Properties
        
        public List<Ticket> Tickets { get; set; }
    }
}
