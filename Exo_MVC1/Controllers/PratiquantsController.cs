using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Exo_MVC1.Data;
using Exo_MVC1.Models;

namespace Exo_MVC1.Controllers
{
    public class PratiquantsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PratiquantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pratiquants
        public async Task<IActionResult> Index()
        {
            var pratiquants = await _context.Pratiquants
                .Include(p => p.Ativite)
                .Include(p => p.Categorie)
                .Include(p => p.Session)
                .ToListAsync();

            var categoriesForActivite = await _context.Categories.ToListAsync();

            ViewData["CategoriesForActivite"] = categoriesForActivite;

            return View(pratiquants);
        }

        public JsonResult GetCategories(int activiteId)
        {
            var categories = _context.Categories
                .Where(c => c.Id_activite == activiteId)
                .ToList();

            return Json(categories.Select(c => new { Id = c.Id, Name = c.Categories }));
        }

        // Other methods (Details, Create, Edit, Delete) remain unchanged

        // GET: Pratiquants/Create
        public IActionResult Create()
        {
            ViewBag.Activites = new SelectList(_context.Activites, "Id", "Nom");
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Categories");
            ViewData["Id_session"] = new SelectList(_context.Sessions, "Id", "Nom");
            return View();
        }

        // POST: Pratiquants/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Id_session,Nom,Sexe,Naissance,Payement,Consigne,Carte_fede,Etiquete,Courriel,Adresse,Telephone,Tel_urgence,Id_activite,Id_categorie,Evaluation,Mode_payement,Carte_payement,Groupe")] Pratiquant pratiquant)
   
        {
            if (ModelState.IsValid)
            {
                _context.Add(pratiquant);
                await _context.SaveChangesAsync();

              
                var categorie = await _context.Categories
                    .FirstOrDefaultAsync(c => c.Id == pratiquant.Id_categorie);

                if (categorie != null)
                {
                 
                    var currentDate = categorie.Datedebut;
                    while (currentDate <= categorie.Datefin)
                    {
                        var presence = new Presence
                        {
                            Id_pratiquant = pratiquant.Id,
                            Jour = currentDate.ToString(),
                            Present = false, 
                            Abscence = true,
                            Id_activite=pratiquant.Id_activite,
                            Id_categorie=pratiquant.Id_categorie,
                            Id_session=pratiquant.Id_session,
                           
                        };

                        _context.Presences.Add(presence);
                        currentDate = currentDate.AddDays(1);  
                    }
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["Id_activite"] = new SelectList(_context.Activites, "Id", "Nom", pratiquant.Id_activite);
            ViewData["Id_categorie"] = new SelectList(_context.Categories, "Id", "Nom", pratiquant.Id_categorie);
            ViewData["Id_session"] = new SelectList(_context.Sessions, "Id", "Nom", pratiquant.Id_session);
            return View(pratiquant);
        }

        // GET: Pratiquants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pratiquant = await _context.Pratiquants.FindAsync(id);
            if (pratiquant == null)
            {
                return NotFound();
            }
            ViewBag.Id_activite = new SelectList(_context.Activites, "Id", "Nom", pratiquant.Id_activite);
            ViewBag.Id_session = new SelectList(_context.Sessions, "Id", "Nom", pratiquant.Id_session);
            ViewBag.Id_categorie = new SelectList(Enumerable.Empty<SelectListItem>());
            return View(pratiquant);
        }

        // POST: Pratiquants/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Id_session,Nom,Sexe,Naissance,Payement,Consigne,Carte_fede,Etiquete,Courriel,Adresse,Telephone,Tel_urgence,Id_activite,Id_categorie,Evaluation,Mode_payement,Carte_payement,Groupe")] Pratiquant pratiquant)
        {
            if (id != pratiquant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pratiquant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PratiquantExists(pratiquant.Id))
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
            ViewData["Id_activite"] = new SelectList(_context.Activites, "Id", "Nom", pratiquant.Id_activite);
            ViewData["Id_categorie"] = new SelectList(_context.Categories, "Id", "Categories", pratiquant.Id_categorie);
            ViewData["Id_session"] = new SelectList(_context.Sessions, "Id", "Nom", pratiquant.Id_session);
            return View(pratiquant);
        }
        // GET: Pratiquants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pratiquant = await _context.Pratiquants
                .Include(p => p.Ativite)      
                .Include(p => p.Categorie)    
                .Include(p => p.Session)      
                .FirstOrDefaultAsync(m => m.Id == id);

            if (pratiquant == null)
            {
                return NotFound();
            }

            return View(pratiquant);
        }

        // GET: Pratiquants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pratiquant = await _context.Pratiquants
                .Include(p => p.Ativite)
                .Include(p => p.Categorie)
                .Include(p => p.Session)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pratiquant == null)
            {
                return NotFound();
            }

            return View(pratiquant);
        }

        // POST: Pratiquants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pratiquant = await _context.Pratiquants.FindAsync(id);
            if (pratiquant != null)
            {
                _context.Pratiquants.Remove(pratiquant);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PratiquantExists(int id)
        {
            return _context.Pratiquants.Any(e => e.Id == id);
        }
    }
}
