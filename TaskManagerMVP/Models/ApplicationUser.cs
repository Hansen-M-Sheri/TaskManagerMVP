using Microsoft.AspNetCore.Identity;

namespace TaskManagerMVP.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual List<Ticket> Tickets { get; set; }
    }
}

