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
    public class CompagniesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompagniesController(ApplicationDbContext context)
        {
            _context = context;
        }
        private bool IsAdminLoggedIn()
        {
            return !string.IsNullOrEmpty(HttpContext.Session.GetString("AdminId"));
        }
        // GET: Compagnies
        public async Task<IActionResult> Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdminId")))
            {
                return RedirectToAction("Login");
            }

            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            return View(await _context.Compagnyes.ToListAsync());
        }

        // GET: Compagnies/Details/5
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

            var compagny = await _context.Compagnyes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (compagny == null)
            {
                return NotFound();
            }

            return View(compagny);
        }

        // GET: Compagnies/Create
        public IActionResult Create()
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login", "Admins");
            }
            return View();
        }

        // POST: Compagnies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Compagnie")] Compagny compagny)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login", "Admins");
            }
            if (ModelState.IsValid)
            {
                _context.Add(compagny);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(compagny);
        }

        // GET: Compagnies/Edit/5
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

            var compagny = await _context.Compagnyes.FindAsync(id);
            if (compagny == null)
            {
                return NotFound();
            }
            return View(compagny);
        }

        // POST: Compagnies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Compagnie")] Compagny compagny)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login", "Admins");
            }
            if (id != compagny.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compagny);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompagnyExists(compagny.Id))
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
            return View(compagny);
        }

        // GET: Compagnies/Delete/5
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

            var compagny = await _context.Compagnyes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (compagny == null)
            {
                return NotFound();
            }

            return View(compagny);
        }

        // POST: Compagnies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login", "Admins");
            }
            var compagny = await _context.Compagnyes.FindAsync(id);
            if (compagny != null)
            {
                _context.Compagnyes.Remove(compagny);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompagnyExists(int id)
        {
            return _context.Compagnyes.Any(e => e.Id == id);
        }
    }
}
