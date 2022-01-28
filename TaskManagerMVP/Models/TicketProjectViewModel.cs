using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerMVP.Models
{
    public class TicketProjectViewModel
    {
        public List<Ticket>? Tickets { get; set; }
        public SelectList? Projects { get; set; }
        public string? TicketProject { get; set; }
        public string? SearchString { get; set; }
    }
}
