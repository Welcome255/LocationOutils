using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Backend.ContexteDb;
using Backend.Entites;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EquipementsController : Controller
    {
        private readonly MagasinContext _context;

        public EquipementsController(MagasinContext context)
        {
            _context = context;
        }

        // GET: Equipements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Equipement>>> TousLesEquipements()
        {
             return await _context.Equipements
                 .Include(l => l.Locations)
                .AsNoTracking().ToListAsync();
        }

        [HttpGet("Location/{IdEquipement}/{nombreDeJourPourReservation}")]
        public async Task<IActionResult> ReserverEquipement(int IdEquipement, int nombreDeJourPourReservation)
        {
            Equipement? equipement = await _context.Equipements
                .Include(l => l.Locations)
                .AsNoTracking()
                .FirstOrDefaultAsync(equipement => equipement.Id == IdEquipement);

            if (equipement == null) { 
                return RedirectToAction(nameof(TousLesEquipements));
            }

            // Création d'une location pour l'équipement
            DateOnly dateActuelle = DateOnly.FromDateTime(DateTime.Now);
            DateOnly dateRetourLocation = DateOnly.FromDateTime(DateTime.Now.AddDays(nombreDeJourPourReservation));

            Location location = new Location() { Active = true, DateDe = dateActuelle,
                DateA = dateRetourLocation, EquipementId = IdEquipement,
            };

            await _context.Locations.AddAsync(location);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(TousLesEquipements));
        }

        [HttpGet("Annuler/{IdLocation}")]
        public async Task<bool> AnnulerReservation(int IdLocation)
        {

            throw new NotImplementedException();
        }

    }
}
