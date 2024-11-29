using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Exo_MVC1.Data;
using Exo_MVC1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http; 

namespace Exo_MVC1.Controllers
{
    public class AdminsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AdminsController(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: Admins
        public async Task<IActionResult> Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdminId")))
            {
                return RedirectToAction("Login");
            }

            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            return View(await _context.Admins.ToListAsync());
        }

        // GET: Admins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins.FirstOrDefaultAsync(m => m.Id == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // GET: Admins/Create
        //[Authorize(Policy = "RequireAdministratorRole")]
        public IActionResult Create()
        {
            return View();
        }

        // GET: Admins/Login
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: Admins/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Admin model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var admin = await _userManager.FindByNameAsync(model.Username);
                if (admin != null && BCrypt.Net.BCrypt.Verify(model.Motdepasse, admin.PasswordHash))
                {
                    var result = await _signInManager.PasswordSignInAsync(admin, model.Motdepasse, isPersistent: false, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        // Set session data
                        HttpContext.Session.SetString("AdminId", admin.Id.ToString());
                        HttpContext.Session.SetString("AdminName", admin.UserName); 

                        return RedirectToLocal(returnUrl);
                    }
                }
                ModelState.AddModelError("", "Invalid username or password.");
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult FunctionLogin(Admin model)
        {
            var admin = _context.Admins.FirstOrDefault(a => a.Username == model.Username);

            if (admin != null && BCrypt.Net.BCrypt.Verify(model.Motdepasse, admin.Motdepasse))
            {
                HttpContext.Session.SetString("AdminId", admin.Id.ToString());
                HttpContext.Session.SetString("AdminName", admin.Username);


                return RedirectToAction("Index", "Home");
            }
            else
            {
                //ModelState.AddModelError("", "Invalid username or password.");
                return RedirectToAction("Error", "Home");
            }

            return View(model);
        }
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            // Clear session data
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Admins");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Username,Motdepasse")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Hash the password
                    admin.Motdepasse = BCrypt.Net.BCrypt.HashPassword(admin.Motdepasse);
                    _context.Add(admin);
                    await _context.SaveChangesAsync();

        
                    TempData["SuccessMessage"] = "Admin created successfully!";
                  
                }
                catch (Exception ex)
                {
                   
                    TempData["ErrorMessage"] = "An error occurred while creating the admin.";
                  
                }
            }
            return View(admin);
        }


        // GET: Admins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        // POST: Admins/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Username,Motdepasse")] Admin admin)
        {
            if (id != admin.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Hash the new password if it has been changed
                    if (!string.IsNullOrEmpty(admin.Motdepasse))
                    {
                        admin.Motdepasse = BCrypt.Net.BCrypt.HashPassword(admin.Motdepasse);
                    }
                    _context.Update(admin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(admin.Id))
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
            return View(admin);
        }

        // GET: Admins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins.FirstOrDefaultAsync(m => m.Id == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var admin = await _context.Admins.FindAsync(id);
            if (admin != null)
            {
                _context.Admins.Remove(admin);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminExists(int id)
        {
            return _context.Admins.Any(e => e.Id == id);
        }
    }
}
