using Microsoft.AspNetCore.Mvc.Rendering;

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
