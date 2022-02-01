#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManagerMVP.Data;
using TaskManagerMVP.Models;

namespace TaskManagerMVP.Controllers
{

    [Authorize]
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        protected async Task<string> GetCurrentUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return userId;
        }
        // GET: Tickets
        public async Task<IActionResult> Index(string ticketProject)
        {
            var userId = await GetCurrentUserId();
            //Use LINQ to get list of projects
            IQueryable<string> projectsQuery = from t in _context.Projects
                                               orderby t.Name
                                               select t.Name;
            //Set query to get tickets only for logged in user
            var tickets =  _context.Tickets
                              .Where(x => x.UserId == userId)
                              .Include(t => t.Project).Include(t => t.TicketPriority).Include(t => t.TicketStatus).Include(t => t.TicketType).Include(t => t.User); 
            if (User.IsInRole("Admin")) //query to get ALL tickets if admin
            {
                tickets = _context.Tickets
                             .Include(t => t.Project).Include(t => t.TicketPriority).Include(t => t.TicketStatus).Include(t => t.TicketType).Include(t => t.User);
            }
            else if (!String.IsNullOrEmpty(ticketProject))
            {
                tickets = _context.Tickets
                    .Where(s => s.Project.Name!.Contains(ticketProject)) 
                    .Include(t => t.Project).Include(t => t.TicketPriority).Include(t => t.TicketStatus).Include(t => t.TicketType).Include(t => t.User);
            }
          
            var ticketProjectVM = new TicketProjectViewModel()
            {
                Projects = new SelectList(await projectsQuery.Distinct().ToListAsync()),
                Tickets = await tickets.ToListAsync()
            };

           
                return View(ticketProjectVM);
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var userId = await GetCurrentUserId();
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Project)
                .Include(t => t.TicketPriority)
                .Include(t => t.TicketStatus)
                .Include(t => t.TicketType)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }
            //I don't know how to check BEFORE I run the query - but this doesn't seem efficient
            if (ticket.UserId == userId || User.IsInRole("Admin"))
            {
                return View(ticket);
            }
            else
            {
                return Forbid(); //only admins and users should see their own tickets
            }
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            var userId = GetCurrentUserId();

            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name");
            ViewData["TicketPriorityId"] = new SelectList(_context.Priorities, "Id", "Name");
            ViewData["TicketStatusId"] = new SelectList(_context.Statuses, "Id", "Name");
            ViewData["TicketTypeId"] = new SelectList(_context.TicketTypes, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
            
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,StartDate,EndDate,UserId,ProjectId,TicketTypeId,TicketStatusId,TicketPriorityId,IsActive")] Ticket ticket)
        {
            var userId = GetCurrentUserId();
            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", ticket.ProjectId);
            ViewData["TicketPriorityId"] = new SelectList(_context.Priorities, "Id", "Name", ticket.TicketPriorityId);
            ViewData["TicketStatusId"] = new SelectList(_context.Statuses, "Id", "Name", ticket.TicketStatusId);
            ViewData["TicketTypeId"] = new SelectList(_context.TicketTypes, "Id", "Name", ticket.TicketTypeId);
           
            if (User.IsInRole("Admin"))
            {
                ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", ticket.UserId);

            }
            else
            {
                ViewData["UserId"] = userId;
            }
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var userId = await GetCurrentUserId();
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            //I don't know how to check BEFORE I run the query - but this doesn't seem efficient
            if (ticket.UserId == userId || User.IsInRole("Admin"))
            {
                ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", ticket.ProjectId);
                ViewData["TicketPriorityId"] = new SelectList(_context.Priorities, "Id", "Name", ticket.TicketPriorityId);
                ViewData["TicketStatusId"] = new SelectList(_context.Statuses, "Id", "Name", ticket.TicketStatusId);
                ViewData["TicketTypeId"] = new SelectList(_context.TicketTypes, "Id", "Name", ticket.TicketTypeId);
                ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", ticket.UserId);
                return View(ticket);
            }
            else
            {
                return Forbid(); //only admins and users should see their own tickets
            }
            
            
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,StartDate,EndDate,UserId,ProjectId,TicketTypeId,TicketStatusId,TicketPriorityId,IsActive")] Ticket ticket)
        {
            var userId = await GetCurrentUserId();
            if (id != ticket.Id)
            {
                return NotFound();
            }
            //if userId doesn't match, or if not admin then don't allow for edit
            if (ticket.UserId == userId || User.IsInRole("Admin"))
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(ticket);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TicketExists(ticket.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", ticket.ProjectId);
                ViewData["TicketPriorityId"] = new SelectList(_context.Priorities, "Id", "Name", ticket.TicketPriorityId);
                ViewData["TicketStatusId"] = new SelectList(_context.Statuses, "Id", "Name", ticket.TicketStatusId);
                ViewData["TicketTypeId"] = new SelectList(_context.TicketTypes, "Id", "Name", ticket.TicketTypeId);
                ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", ticket.UserId);
                return View(ticket);
            }
            else
            {
                return Forbid();
            }
            
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var userId = await GetCurrentUserId();
            if (id == null)
            {
                return NotFound();
            }

            
            var ticket = await _context.Tickets
                .Include(t => t.Project)
                .Include(t => t.TicketPriority)
                .Include(t => t.TicketStatus)
                .Include(t => t.TicketType)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }
            //if userId doesn't match, or if not admin then don't allow for delete
            if (ticket.UserId == userId || User.IsInRole("Admin"))
            {
                return View(ticket);
            }
            else
            {
                return Forbid();
            }

            
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = await GetCurrentUserId();

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket.UserId == userId || User.IsInRole("Admin"))
            {
                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return Forbid();
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}
