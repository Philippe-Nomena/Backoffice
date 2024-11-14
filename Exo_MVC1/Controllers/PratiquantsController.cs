using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using OfficeOpenXml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Exo_MVC1.Data;
using Exo_MVC1.Models;
using Microsoft.AspNetCore.Http; 
using System.Diagnostics;
using Microsoft.VisualStudio.TextTemplating;
using Newtonsoft.Json;

namespace Exo_MVC1.Controllers
{
    public class PratiquantsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PratiquantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Private method to check if Admin is logged in
        private bool IsAdminLoggedIn()
        {
            return !string.IsNullOrEmpty(HttpContext.Session.GetString("AdminId"));
        }

        // GET: Pratiquants

        //public async Task<IActionResult> Index()
        //{
        //    if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdminId")))
        //    {
        //        return RedirectToAction("Login");
        //    }

        //    ViewBag.AdminName = HttpContext.Session.GetString("AdminName");

        //    // Await the async operation here
        //    var pratiquants = await _context.Pratiquants.ToListAsync();
        //    return View(pratiquants);
        //}

        public async Task<IActionResult> Index(int? Id_compagnie)
        {

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdminId")))
            {
                return RedirectToAction("Login");
            }

            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");

            var listecompagnie = _context.Compagnyes.ToList();
            ViewData["liste"] = listecompagnie;

            var pratiquants = new List<Pratiquant>();

            if (Id_compagnie.HasValue)
            {
                var pratiquantsliste = await _context.Pratiquants
                    .Include(p => p.Ativite)
                    .Include(p => p.Categorie)
                    .Include(p => p.Session)
                    .Where(a => a.Ativite.Id_compagnie == Id_compagnie.Value)
                    .ToListAsync();
                pratiquants.AddRange(pratiquantsliste);
            }
            else
            {
                var pratiquantsliste = await _context.Pratiquants
                    .Include(p => p.Ativite)
                    .Include(p => p.Categorie)
                    .Include(p => p.Session)
                    .ToListAsync();
                pratiquants.AddRange(pratiquantsliste);
            }

            Debug.WriteLine("MIDINA");

            var categoriesForActivite = await _context.Categories.ToListAsync();
            ViewData["CategoriesForActivite"] = categoriesForActivite;

            return View(pratiquants);
        }

        // GET: Pratiquants/Details/5
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

        // GET: Pratiquants/Create
        public IActionResult Create()
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login", "Admins");
            }

            ViewBag.Activites = new SelectList(_context.Activites, "Id", "Nom");
            //ViewBag.Categories = new SelectList(_context.Categories, "Id", "Categories");
            ViewBag.Categories= _context.Categories.ToList();
            ViewData["Id_session"] = new SelectList(_context.Sessions, "Id", "Nom");
            return View();
        }

        // POST: Pratiquants/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Id_session,Nom,Sexe,Naissance,Payement,Consigne,Carte_fede,Etiquete,Courriel,Adresse,Telephone,Tel_urgence,Id_activite,Id_categorie,Evaluation,Mode_payement,Carte_payement,Groupe")] Pratiquant pratiquant)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login", "Admins");
            }

            if (ModelState.IsValid)
            {
                _context.Add(pratiquant);
                await _context.SaveChangesAsync();

                var categorie = await _context.Categories
                    .FirstOrDefaultAsync(c => c.Id == pratiquant.Id_categorie);

                if (categorie != null)
                {
                   
                    DateOnly currentDate = categorie.Datedebut.Value;
                    DateOnly endDate = categorie.Datefin.Value;

                    while (currentDate <= endDate)
                    {
                        var presence = new Presence
                        {
                            Id_pratiquant = pratiquant.Id,
                            Jour = currentDate, 
                            Present = false,
                            Abscence = true,
                            Id_activite = pratiquant.Id_activite,
                            Id_categorie = pratiquant.Id_categorie,
                            Id_session = pratiquant.Id_session,
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
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login", "Admins");
            }

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
            //ViewBag.Id_categorie = new SelectList(Enumerable.Empty<SelectListItem>());
            ViewBag.Categories= _context.Categories.ToList();


            return View(pratiquant);
        }

        // POST: Pratiquants/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Id_session,Nom,Sexe,Naissance,Payement,Consigne,Carte_fede,Etiquete,Courriel,Adresse,Telephone,Tel_urgence,Id_activite,Id_categorie,Evaluation,Mode_payement,Carte_payement,Groupe")] Pratiquant pratiquant)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login", "Admins");
            }

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

            ViewBag.Id_activite = new SelectList(_context.Activites, "Id", "Nom", pratiquant.Id_activite);
            ViewBag.Id_session = new SelectList(_context.Sessions, "Id", "Nom", pratiquant.Id_session);

            //ViewBag.Id_categorie = new SelectList(Enumerable.Empty<SelectListItem>());
            return View(pratiquant);
        }

        // GET: Pratiquants/Delete/5
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
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login", "Admin");
            }

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

        // For dynamic category fetching based on selected activite
        //[HttpGet]
        //public JsonResult GetCategories(int activiteId)
        //{
        //    var categories = _context.Categories
        //        .Where(c => c.Id_activite == activiteId)
        //        .Select(c => new { Id = c.Id, Name = c.Categories }) // Ensure this matches your model
        //        .ToList();

        //    return Json(categories);
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile csvFile)
        {

            if (csvFile != null && csvFile.Length > 0)
            {

                if (Path.GetExtension(csvFile.FileName).ToLower() == ".csv")
                {


                    using (var streamReader = new StreamReader(csvFile.OpenReadStream()))
                    {

                        string line;
                        int row = 0;

                        while ((line = await streamReader.ReadLineAsync()) != null)
                        {

                            row++;


                            if (row == 1) continue;


                            var values = line.Split(',');


                            Pratiquant entite = new Pratiquant();
                            entite.Nom = string.IsNullOrWhiteSpace(values[1]) ? "" : values[0];

                            entite.Sexe = string.IsNullOrWhiteSpace(values[1]) ? "" : values[1];
                            //entite.Nom = string.IsNullOrWhiteSpace(values[2]) ? "" : values[2];
                            //entite.PersonneUrgence = string.IsNullOrWhiteSpace(values[3]) ? "" : values[3];
                            //entite.Adresse = string.IsNullOrWhiteSpace(values[4]) ? "" : values[4];
                            //entite.Ville = string.IsNullOrWhiteSpace(values[5]) ? "" : values[5];
                            //entite.CodePostal = string.IsNullOrWhiteSpace(values[6]) ? "" : values[6];
                            //entite.AdresseCourriel = string.IsNullOrWhiteSpace(values[7]) ? "" : values[7];
                            //entite.Sexe = string.IsNullOrWhiteSpace(values[8]) ? "" : values[8];

                            //// Gestion de la date de naissance
                            //if (DateTime.TryParse(values[9], out DateTime dateNaissance))
                            //{
                            //    entite.DateNaissance = dateNaissance;
                            //}
                            //else
                            //{
                            //    entite.DateNaissance = DateTime.MinValue; // Date par défaut si non valide
                            //}

                            //entite.Poids = string.IsNullOrWhiteSpace(values[10]) ? "" : values[10];
                            //entite.AutreNumero = string.IsNullOrWhiteSpace(values[11]) ? "" : values[11];

                            //// Gestion des cases à cocher avec "X"
                            //entite.Paiement = values[12] == "X";
                            //entite.Federation = values[13] == "X";

                            Debug.WriteLine($"IdGroupe: {entite.Nom}");
                            Debug.WriteLine($"Prenom: {entite.Sexe}");
                            //Debug.WriteLine($"Nom: {entite.Nom}");
                            //Debug.WriteLine($"PersonneUrgence: {entite.PersonneUrgence}");
                            //Debug.WriteLine($"Adresse: {entite.Adresse}");
                            //Debug.WriteLine($"Ville: {entite.Ville}");
                            //Debug.WriteLine($"CodePostal: {entite.CodePostal}");
                            //Debug.WriteLine($"AdresseCourriel: {entite.AdresseCourriel}");
                            //Debug.WriteLine($"Sexe: {entite.Sexe}");
                            //Debug.WriteLine($"DateNaissance: {entite.DateNaissance}");
                            //Debug.WriteLine($"Poids: {entite.Poids}");
                            //Debug.WriteLine($"AutreNumero: {entite.AutreNumero}");
                            //Debug.WriteLine($"Paiement: {entite.Paiement}");
                            //Debug.WriteLine($"Federation: {entite.Federation}");

                            // Sauvegarder l'entité
                            _context.Add(entite);
                            await _context.SaveChangesAsync();
                        }
                    }

                }

            }

            return View("Index");
        }
        [HttpGet]
        public async Task<IActionResult> ExportToExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Load pratiquants with related data
            var pratiquants = await _context.Pratiquants
                .Include(p => p.Session)
                .Include(p => p.Ativite)
                .Include(p => p.Categorie)
                .ToListAsync();

            using (var package = new ExcelPackage())
            {
                // Create a worksheet
                var worksheet = package.Workbook.Worksheets.Add("Pratiquants");

                // Add headers
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "ID Session";
                worksheet.Cells[1, 3].Value = "Nom";
                worksheet.Cells[1, 4].Value = "Sexe";
                worksheet.Cells[1, 5].Value = "Naissance";
                worksheet.Cells[1, 6].Value = "Payement";
                worksheet.Cells[1, 7].Value = "Consigne";
                worksheet.Cells[1, 8].Value = "Carte Fédérale";
                worksheet.Cells[1, 9].Value = "Etiquete";
                worksheet.Cells[1, 10].Value = "Courriel";
                worksheet.Cells[1, 11].Value = "Adresse";
                worksheet.Cells[1, 12].Value = "Téléphone";
                worksheet.Cells[1, 13].Value = "Téléphone Urgence";
                worksheet.Cells[1, 14].Value = "ID Activité";
                worksheet.Cells[1, 15].Value = "ID Catégorie";
                worksheet.Cells[1, 16].Value = "Evaluation";
                worksheet.Cells[1, 17].Value = "Mode de Payement";
                worksheet.Cells[1, 18].Value = "Carte de Payement";
                worksheet.Cells[1, 19].Value = "Groupe";

                // Add data rows
                for (int i = 0; i < pratiquants.Count; i++)
                {
                    var pratiquant = pratiquants[i];
                    worksheet.Cells[i + 2, 1].Value = pratiquant.Id;
                    worksheet.Cells[i + 2, 2].Value = pratiquant.Session?.Nom ?? "N/A";
                    worksheet.Cells[i + 2, 3].Value = pratiquant.Nom;
                    worksheet.Cells[i + 2, 4].Value = pratiquant.Sexe;
                    worksheet.Cells[i + 2, 5].Value = pratiquant.Naissance.Value.ToString("dd/MM/yyyy") ?? "N/A";
                    worksheet.Cells[i + 2, 6].Value = pratiquant.Payement;
                    worksheet.Cells[i + 2, 7].Value = pratiquant.Consigne;
                    worksheet.Cells[i + 2, 8].Value = pratiquant.Carte_fede;
                    worksheet.Cells[i + 2, 9].Value = pratiquant.Etiquete;
                    worksheet.Cells[i + 2, 10].Value = pratiquant.Courriel;
                    worksheet.Cells[i + 2, 11].Value = pratiquant.Adresse;
                    worksheet.Cells[i + 2, 12].Value = pratiquant.Telephone;
                    worksheet.Cells[i + 2, 13].Value = pratiquant.Tel_urgence;
                    worksheet.Cells[i + 2, 14].Value = pratiquant.Ativite?.Nom?? "N/A";
                    worksheet.Cells[i + 2, 15].Value = pratiquant.Categorie?.Categories ?? "N/A";
                    worksheet.Cells[i + 2, 16].Value = pratiquant.Evaluation;
                    worksheet.Cells[i + 2, 17].Value = pratiquant.Mode_payement;
                    worksheet.Cells[i + 2, 18].Value = pratiquant.Carte_payement;
                    worksheet.Cells[i + 2, 19].Value = pratiquant.Groupe;
                }

                // Auto-fit columns
                worksheet.Cells.AutoFitColumns();

                // Generate Excel file as a byte array
                var excelFile = package.GetAsByteArray();

                // Return the file
                return File(excelFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Pratiquants.xlsx");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetPratiquantsChartData()
        {
            // Group pratiquants by activite and count them
            var data = await _context.Pratiquants
    .GroupBy(p => new { p.Id_activite, p.Ativite.Nom }) 
    .Select(g => new
    {
        ActiviteId = g.Key.Id_activite,
        ActiviteName = g.Key.Nom, 
        Count = g.Count()
    })
    .ToListAsync();

            // Group pratiquants by category and count them
            var data1 = await _context.Pratiquants
                .GroupBy(p => new { p.Id_categorie, p.Categorie.Categories})
                .Select(a => new
                {
                    CategorieId = a.Key,
                    CategorieName=a.Key.Categories,
                    Count = a.Count()
                })
                .ToListAsync();

           
            var data2 = await _context.Pratiquants
             .Include(p => p.Ativite) 
             .ThenInclude(a => a.Compagny) 
             .GroupBy(p => new { p.Ativite.Id_compagnie, p.Ativite.Compagny.Compagnie }) 
             .Select(c => new
     {
         CompagnyId = c.Key.Id_compagnie,
         CompagnyName = c.Key.Compagnie,
         Count = c.Count()
     })
     .ToListAsync();

            // Combine the results into a single object
            var result = new
            {
                ActiviteData = data,
                CategorieData = data1,
                CompagnyData = data2
            };


            // Return the combined data in JSON format
            return Json(result);
        }



    }
}
