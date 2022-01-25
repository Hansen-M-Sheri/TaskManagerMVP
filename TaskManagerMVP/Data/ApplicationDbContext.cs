using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagerMVP.Models;

namespace TaskManagerMVP.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        //entities to create with scaffolding
        public DbSet<TicketPriority> Priorities { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TicketStatus> Statuses { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TicketStatus>(x =>
            {
                x.HasData(
                    new TicketStatus() { Id = 1, Name = "New", Description = "New Task", IsActive = true },
                    new TicketStatus() { Id = 2, Name = "Closed", Description = "Task is Closed", IsActive = true },
                    new TicketStatus() { Id = 3, Name = "On Hold", Description = "Task is waiting on something to proceed", IsActive = true }
                    );
            });

            modelBuilder.Entity<TicketType>(x =>
            {
                x.HasData(
                    new TicketType() { Id = 1, Name = "Bug", Description = "Problem found in code being developed ", IsActive = true },
                    new TicketType() { Id = 2, Name = "Task", Description = "A Todo to complete", IsActive = true },
                    new TicketType() { Id = 3, Name = "Issue", Description = "An or cause that may delay release of project ", IsActive = true }
                    );
            });



            modelBuilder.Entity<TicketPriority>(x =>
            {
                x.HasData(
                    new TicketPriority() { Id = 1, Name = "High", Description = "Task is urgent and needs to be done by end of day", IsActive = true },
                    new TicketPriority() { Id = 2, Name = "Average", Description = "Task should be completed within 3 days", IsActive = true },
                    new TicketPriority() { Id = 3, Name = "Low", Description = "Task ideally done by end of week", IsActive = true }

                    );
            });

        }
    }
}