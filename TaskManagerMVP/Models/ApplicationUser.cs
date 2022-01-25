using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerMVP.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual List<Ticket> Tickets { get; set; }
    }
}
