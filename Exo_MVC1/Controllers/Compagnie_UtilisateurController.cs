using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Exo_MVC1.Data;
using Exo_MVC1.Models;
using BCrypt.Net; // Add BCrypt.Net for password hashing

namespace Exo_MVC1.Controllers
{
    public class Compagnie_UtilisateurController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Compagnie_UtilisateurController(ApplicationDbContext context)
        {
            _context = context;
        }

        private bool IsAdminLoggedIn()
        {
            return !string.IsNullOrEmpty(HttpContext.Session.GetString("AdminId"));
        }

        // GET: Compagnie_Utilisateur
        public async Task<IActionResult> Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdminId")))
            {
                return RedirectToAction("Login");
            }

            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            var applicationDbContext = _context.Compagnie_Utilisateurs.Include(c => c.Compagny).Include(c => c.Utilisateur);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Compagnie_Utilisateur/Create
        public IActionResult Create()
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login", "Admins");
            }
            ViewData["Id_compagnie"] = new SelectList(_context.Compagnyes, "Id", "Compagnie");
            ViewData["Id_utilisateur"] = new SelectList(_context.Utilisateurs, "Id", "Nom");
            return View();
        }

        // POST: Compagnie_Utilisateur/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,Motdepasse,Id_utilisateur,Id_compagnie")] Compagnie_Utilisateur compagnie_Utilisateur)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login", "Admins");
            }

            if (ModelState.IsValid)
            {
                // Hash the password before saving
                compagnie_Utilisateur.Motdepasse = BCrypt.Net.BCrypt.HashPassword(compagnie_Utilisateur.Motdepasse);

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
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login", "Admins");
            }
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Motdepasse,Id_utilisateur,Id_compagnie")] Compagnie_Utilisateur compagnie_Utilisateur)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login", "Admins");
            }
            if (id != compagnie_Utilisateur.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Hash the new password if it was changed
                    if (!string.IsNullOrEmpty(compagnie_Utilisateur.Motdepasse))
                    {
                        compagnie_Utilisateur.Motdepasse = BCrypt.Net.BCrypt.HashPassword(compagnie_Utilisateur.Motdepasse);
                    }

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

        // POST: Compagnie_Utilisateur/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login", "Admins");
            }
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
