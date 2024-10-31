using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Exo_MVC1.Data;
using Exo_MVC1.Models;
using BCrypt.Net;
using System.Diagnostics;

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
        public async Task<IActionResult> Index(int? Id_compagnie)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdminId")))
            {
                return RedirectToAction("Login");
            }

            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");

            IQueryable<Compagnie_Utilisateur> applicationDbContext;

            if (Id_compagnie.HasValue)
            {
                Debug.WriteLine("tsy null lery");
                applicationDbContext = _context.Compagnie_Utilisateurs
                    .Include(c => c.Compagny)
                    .Include(c => c.Utilisateur)
                    .Where(a => a.Id_compagnie == Id_compagnie);
            }
            else
            {
                Debug.WriteLine("null lery");
                applicationDbContext = _context.Compagnie_Utilisateurs
                    .Include(c => c.Compagny)
                    .Include(c => c.Utilisateur);
            }

            // Utilisation de ToListAsync() pour une récupération asynchrone
            var resultList = await applicationDbContext.ToListAsync();

            // Récupération des compagnies de manière asynchrone
            var compagnies = await _context.Compagnyes.ToListAsync();

            ViewData["compagnies"] = compagnies;

            return View(resultList); // Utilisez resultList directement
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
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Username,Motdepasse,Id_utilisateur,Id_compagnie")] Compagnie_Utilisateur compagnie_Utilisateur)
        //{
        //    if (!IsAdminLoggedIn())
        //    {
        //        return RedirectToAction("Login", "Admins");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        // Hash the password before saving
        //        compagnie_Utilisateur.Motdepasse = BCrypt.Net.BCrypt.HashPassword(compagnie_Utilisateur.Motdepasse);

        //        _context.Add(compagnie_Utilisateur);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["Id_compagnie"] = new SelectList(_context.Compagnyes, "Id", "Compagnie", compagnie_Utilisateur.Id_compagnie);
        //    ViewData["Id_utilisateur"] = new SelectList(_context.Utilisateurs, "Id", "Nom", compagnie_Utilisateur.Id_utilisateur);
        //    return View(compagnie_Utilisateur);
        //}
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
               
                bool usernameExists = await _context.Compagnie_Utilisateurs
                    .AnyAsync(cu => cu.Username == compagnie_Utilisateur.Username);

                if (usernameExists)
                {
                    return View("Error");

                }
                else
                {
                    // Hash the password before saving
                    compagnie_Utilisateur.Motdepasse = BCrypt.Net.BCrypt.HashPassword(compagnie_Utilisateur.Motdepasse);

                    _context.Add(compagnie_Utilisateur);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            // Populate dropdowns if the model is not valid or username already exists
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

        // GET: Compagnie_Utilisateur/UtilisateurByCompagnie
        //public async Task<IActionResult> UtilisateurByCompagnie(int Id_compagnie)
        //{
        //    if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdminId")))
        //    {
        //        return RedirectToAction("Login");
        //    }

        //    ViewBag.AdminName = HttpContext.Session.GetString("AdminName");

        //    // Retrieve the list of companies
        //    var compagnies = _context.Compagnyes.ToList();
        //    ViewBag.listCompanies = compagnies;

        //    // Retrieve the list of users associated with the specified company ID
        //    var utilisateurs = await _context.Compagnie_Utilisateurs
        //        .Include(p => p.Utilisateur)
        //        .Where(p => p.Compagny.Id == Id_compagnie)
        //        .ToListAsync();

        //    Debug.WriteLine("===============User List================");
        //    ViewData["utilisateurs"]=utilisateurs;
        //    Debug.WriteLine("===============End of List================");

        //    return View("~/Views/Compagnie_Utilisateur/Index.cshtml");
        //}

        private bool Compagnie_UtilisateurExists(int id)
        {
            return _context.Compagnie_Utilisateurs.Any(e => e.Id == id);
        }
    }
}
