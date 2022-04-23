using Concert.Data;
using Concert.Data.Entities;
using Concert.Helpers;
using Concert.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Concert.Controllers
{
    public class AccessController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;

        public AccessController(DataContext context, ICombosHelper combosHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tickets.ToListAsync());
        }

        public IActionResult CheckTicket()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckTicket(int? id)
        {
            if (id == null)
            {
                return NotFound();
               
            }

            var ticket = await _context.Tickets
                .FirstOrDefaultAsync(t => t.Id == id);
            if (ticket == null)
            {
                ModelState.AddModelError(string.Empty, "Ticket no existe.");
            }
            if (ticket.WasUsed != false)
            {
                ModelState.AddModelError(string.Empty, "Ticket ya es usado.");
                return RedirectToAction(nameof(Index), new { Id = ticket.Id });
            }
            return RedirectToAction(nameof(Index), new { Id = ticket.Id });
        }


    }

}

