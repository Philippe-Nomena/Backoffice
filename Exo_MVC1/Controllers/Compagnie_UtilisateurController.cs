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
    public class Compagnie_UtilisateurController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Compagnie_UtilisateurController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Compagnie_Utilisateur
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Compagnie_Utilisateurs.Include(c => c.Compagny).Include(c => c.Utilisateur);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Compagnie_Utilisateur/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compagnie_Utilisateur = await _context.Compagnie_Utilisateurs
                .Include(c => c.Compagny)
                .Include(c => c.Utilisateur)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (compagnie_Utilisateur == null)
            {
                return NotFound();
            }

            return View(compagnie_Utilisateur);
        }

        // GET: Compagnie_Utilisateur/Create
        public IActionResult Create()
        {
            ViewData["Id_compagnie"] = new SelectList(_context.Compagnyes, "Id", "Compagnie");
            ViewData["Id_utilisateur"] = new SelectList(_context.Utilisateurs, "Id", "Nom");
            return View();
        }

        // POST: Compagnie_Utilisateur/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,Motdepasse,Id_utilisateur,Id_compagnie")] Compagnie_Utilisateur compagnie_Utilisateur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(compagnie_Utilisateur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_compagnie"] = new SelectList(_context.Compagnyes, "Id", "Compagnie", compagnie_Utilisateur.Id_compagnie);
            ViewData["Id_utilisateur"] = new SelectList(_context.Utilisateurs, "Id", "Nom", compagnie_Utilisateur.Id_utilisateur);
            return View(compagnie_Utilisateur);
        }

        // GET: Compagnie_Utilisateur/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compagnie_Utilisateur = await _context.Compagnie_Utilisateurs.FindAsync(id);
            if (compagnie_Utilisateur == null)
            {
                return NotFound();
            }
            ViewData["Id_compagnie"] = new SelectList(_context.Compagnyes, "Id", "Compagnie", compagnie_Utilisateur.Id_compagnie);
            ViewData["Id_utilisateur"] = new SelectList(_context.Utilisateurs, "Id", "Nom", compagnie_Utilisateur.Id_utilisateur);
            return View(compagnie_Utilisateur);
        }

        // POST: Compagnie_Utilisateur/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Motdepasse,Id_utilisateur,Id_compagnie")] Compagnie_Utilisateur compagnie_Utilisateur)
        {
            if (id != compagnie_Utilisateur.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compagnie_Utilisateur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Compagnie_UtilisateurExists(compagnie_Utilisateur.Id))
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
            ViewData["Id_compagnie"] = new SelectList(_context.Compagnyes, "Id", "Compagnie", compagnie_Utilisateur.Id_compagnie);
            ViewData["Id_utilisateur"] = new SelectList(_context.Utilisateurs, "Id", "Nom", compagnie_Utilisateur.Id_utilisateur);
            return View(compagnie_Utilisateur);
        }

        // GET: Compagnie_Utilisateur/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compagnie_Utilisateur = await _context.Compagnie_Utilisateurs
                .Include(c => c.Compagny)
                .Include(c => c.Utilisateur)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (compagnie_Utilisateur == null)
            {
                return NotFound();
            }

            return View(compagnie_Utilisateur);
        }

        // POST: Compagnie_Utilisateur/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var compagnie_Utilisateur = await _context.Compagnie_Utilisateurs.FindAsync(id);
            if (compagnie_Utilisateur != null)
            {
                _context.Compagnie_Utilisateurs.Remove(compagnie_Utilisateur);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Compagnie_UtilisateurExists(int id)
        {
            return _context.Compagnie_Utilisateurs.Any(e => e.Id == id);
        }
    }
}
