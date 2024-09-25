using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Exo_MVC1.Data;
using Exo_MVC1.Models;
using System.Diagnostics;

namespace Exo_MVC1.Controllers
{
    public class PresencesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PresencesController(ApplicationDbContext context)
        {
            _context = context;
        }
        private bool IsAdminLoggedIn()
        {
            return !string.IsNullOrEmpty(HttpContext.Session.GetString("AdminId"));
        }
        // GET: Presences
        public async Task<IActionResult> Index()
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login", "Admins");
            }
            var applicationDbContext = _context.Presences.Include(p => p.Ativite).Include(p => p.Categorie).Include(p => p.Pratiquant).Include(p => p.Session);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Presences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login", "Admins");
            }
            if (id == null)
            {
                return NotFound();
            }

            var presence = await _context.Presences
                .Include(p => p.Ativite)
                .Include(p => p.Categorie)
                .Include(p => p.Pratiquant)
                .Include(p => p.Session)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (presence == null)
            {
                return NotFound();
            }

            return View(presence);
        }

        // GET: Presences/Create
        public IActionResult Create()
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login", "Admins");
            }
            ViewData["Id_activite"] = new SelectList(_context.Activites, "Id", "Nom");
            ViewData["Id_categorie"] = new SelectList(_context.Categories, "Id", "Categories");
            ViewData["Id_pratiquant"] = new SelectList(_context.Pratiquants, "Id", "Nom");
            ViewData["Id_session"] = new SelectList(_context.Sessions, "Id", "Nom");
            return View();
        }

        // POST: Presences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Id_session,Id_activite,Id_categorie,Jour,Present,Abscence,Id_pratiquant")] Presence presence)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login", "Admins");
            }
            if (ModelState.IsValid)
            {
                _context.Add(presence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_activite"] = new SelectList(_context.Activites, "Id", "Nom", presence.Id_activite);
            ViewData["Id_categorie"] = new SelectList(_context.Categories, "Id", "Categories", presence.Id_categorie);
            ViewData["Id_pratiquant"] = new SelectList(_context.Pratiquants, "Id", "Nom", presence.Id_pratiquant);
            ViewData["Id_session"] = new SelectList(_context.Sessions, "Id", "Nom", presence.Id_session);
            return View(presence);
        }

        // GET: Presences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login", "Admins");
            }
            if (id == null)
            {
                return NotFound();
            }

            var presence = await _context.Presences.FindAsync(id);
            if (presence == null)
            {
                return NotFound();
            }
            ViewData["Id_activite"] = new SelectList(_context.Activites, "Id", "Nom", presence.Id_activite);
            ViewData["Id_categorie"] = new SelectList(_context.Categories, "Id", "Categories", presence.Id_categorie);
            ViewData["Id_pratiquant"] = new SelectList(_context.Pratiquants, "Id", "Nom", presence.Id_pratiquant);
            ViewData["Id_session"] = new SelectList(_context.Sessions, "Id", "Nom", presence.Id_session);
            return View(presence);
        }

        // POST: Presences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Id_session,Id_activite,Id_categorie,Jour,Present,Abscence,Id_pratiquant")] Presence presence)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login", "Admins");
            }
            if (id != presence.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(presence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PresenceExists(presence.Id))
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
            ViewData["Id_activite"] = new SelectList(_context.Activites, "Id", "Nom", presence.Id_activite);
            ViewData["Id_categorie"] = new SelectList(_context.Categories, "Id", "Categories", presence.Id_categorie);
            ViewData["Id_pratiquant"] = new SelectList(_context.Pratiquants, "Id", "Nom", presence.Id_pratiquant);
            ViewData["Id_session"] = new SelectList(_context.Sessions, "Id", "Nom", presence.Id_session);
            return View(presence);
        }

        // GET: Presences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login", "Admins");
            }
            if (id == null)
            {
                return NotFound();
            }

            var presence = await _context.Presences
                .Include(p => p.Ativite)
                .Include(p => p.Categorie)
                .Include(p => p.Pratiquant)
                .Include(p => p.Session)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (presence == null)
            {
                return NotFound();
            }

            return View(presence);
        }
        // GET: Presence/ByActiviteAndCompagnie
        public async Task<IActionResult> ByActiviteAndCompagnie(int Id_compagnie)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login", "Admins");
            }

            var compagnies = _context.Compagnyes.ToList();
            ViewBag.listCompanies= compagnies;
            var presences = await _context.Presences
                .Include(p => p.Ativite)      
                .ThenInclude(a => a.Compagny)
                .Include(p => p.Pratiquant)
                .Where(p => p.Ativite.Compagny.Id == Id_compagnie)
                .ToListAsync();

         
          
            return View(presences);
        }

        // POST: Presences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login", "Admins");
            }
            var presence = await _context.Presences.FindAsync(id);
            if (presence != null)
            {
                _context.Presences.Remove(presence);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PresenceExists(int id)
        {
            return _context.Presences.Any(e => e.Id == id);
        }
    }
}
