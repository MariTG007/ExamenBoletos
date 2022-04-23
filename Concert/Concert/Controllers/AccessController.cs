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

            Ticket ticket = await _context.Tickets
                .FirstOrDefaultAsync(t => t.Id == id);
            // _context.UpdateRange(ticket);
            //await _context.SaveChangesAsync();

            
                if (id < 0 || id > 5003)
                {
                    TempData["Message"] = "Error de boleta, no existe";
                    
                return RedirectToAction(nameof(CheckTicket));

                }
            
            

            if (ticket.WasUsed != false)
            {
                TempData["Message"] = "Ticket ya es usado.";
                TempData["Name"] = ticket.Name;
                TempData["Document"] = ticket.Document;
                TempData["Date"] = ticket.Date;
                //TODO : time and entrance
                return RedirectToAction(nameof(CheckTicket), new { Id = ticket.Id });
            }
            else{
                TempData["Message"] = "Ticket no ha sido usada.";
                return RedirectToAction(nameof(Register), new { Id = ticket.Id });
            }

            

            
          
            
        }

        private object Register()
        {
            throw new NotImplementedException();
        }


        /*
        public async Task<IActionResult> Register()
        {
            TicketViewModel model = new()
            {
                Id = Guid.Empty.ToString(),
                EntranceId = await _combosHelper.GetComboEntrancesAsync(),
               
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = Guid.Empty;

                if (model.ImageFile != null)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "users");
                }

                model.ImageId = imageId;
                User user = await _userHelper.AddUserAsync(model);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Este correo ya está siendo usado.");
                    model.Countries = await _combosHelper.GetComboCountriesAsync();
                    model.States = await _combosHelper.GetComboStatesAsync(model.CountryId);
                    model.Cities = await _combosHelper.GetComboCitiesAsync(model.StateId);
                    return View(model);
                }

                LoginViewModel loginViewModel = new()
                {
                    Password = model.Password,
                    RememberMe = false,
                    Username = model.Username
                };

                var result2 = await _userHelper.LoginAsync(loginViewModel);

                if (result2.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        */
        public async Task<IActionResult> DetailTicket(int? id)
        
        {
            if (id == null)
            {
                return NotFound();
            }

            Ticket ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

      
    }

}

