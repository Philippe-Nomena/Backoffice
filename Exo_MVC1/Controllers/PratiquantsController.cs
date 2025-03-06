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

using SkiaSharp;

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
       
            ViewBag.Categories= _context.Categories.ToList();
            ViewData["Id_session"] = new SelectList(_context.Sessions, "Id", "Nom");
            return View();
        }

      

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
                try
                {
                  
                    string rootBarcodePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "barcodes");
                    BarcodeHelper.GenerateBarcode(pratiquant.Id, rootBarcodePath);
                }
                catch (Exception ex)
                {
                   
                    ModelState.AddModelError("", "Failed to generate barcode. Please try again.");
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["Id_activite"] = new SelectList(_context.Activites, "Id", "Nom", pratiquant.Id_activite);
            ViewData["Id_categorie"] = new SelectList(_context.Categories, "Id", "Nom", pratiquant.Id_categorie);
            ViewData["Id_session"] = new SelectList(_context.Sessions, "Id", "Nom", pratiquant.Id_session);
            return View(pratiquant);
        }

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

        // Custom function to convert various boolean-like values to true or false
        private bool ParseBoolean(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;

            value = value.Trim().ToLower();
            return value switch
            {
                "1" or "true" or "yes" => true,
                "0" or "false" or "no" => false,
                _ => false, 
            };
        }
        private DateOnly? ParseDate(string dateString)
        {
            // Define multiple possible formats to parse
            var formats = new[] { "yyyy-MM-dd", "MM/dd/yyyy", "dd-MM-yyyy", "dd/MM/yyyy" };

            // Trim input to remove extra spaces
            dateString = dateString.Trim();

            Console.WriteLine($"Attempting to parse date: '{dateString}'");

            if (DateTime.TryParseExact(dateString, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out var parsedDate))
            {
                Console.WriteLine($"Successfully parsed date: {parsedDate}");
                return DateOnly.FromDateTime(parsedDate);
            }

            // Log failure
            Console.WriteLine($"Failed to parse date: '{dateString}'");
            return null;
        }


        private int? TryParseInt(string value)
        {
            if (int.TryParse(value, out var result))
            {
                return result;
            }
            else
            {
                Console.WriteLine($"Invalid Session value: {value}. Defaulting to null.");
                return null;
            }
        }

        //// Generer le code barre de format CODE_39
        public class BarcodeHelper
        {
            public static string GenerateBarcode(int pratiquantId, string rootBarcode)
            {
                if (pratiquantId <= 0)
                    throw new ArgumentException("Pratiquant ID must be a positive integer.", nameof(pratiquantId));

                if (!Directory.Exists(rootBarcode))
                {
                    Directory.CreateDirectory(rootBarcode); 
                }

                string fileName = $"{pratiquantId}_barcode.png";
                string filePath = Path.Combine(rootBarcode, fileName);

                try
                {
                    var writer = new ZXing.SkiaSharp.BarcodeWriter
                    {
                        Format = ZXing.BarcodeFormat.CODE_39,
                        Options = new ZXing.Common.EncodingOptions
                        {
                            Height = 100,
                            Width = 300,
                            Margin = 2,
                            PureBarcode=true
                        }
                    };

                    // Créer le fichier
                    using (var image = writer.Write(pratiquantId.ToString()))
                    using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                    {
                        // Crée un FileStream pour écrire les données dans le fichier
                        using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                        {
                            data.SaveTo(stream);
                        }
                    }

                    return filePath;
                }
                catch (UnauthorizedAccessException ex)
                {
                    // Gérer les problèmes de permission
                    throw new InvalidOperationException($"Access denied: {ex.Message}", ex);
                }
                catch (IOException ex)
                {
                    // Gérer les erreurs d'IO (fichier déjà ouvert, etc.)
                    throw new InvalidOperationException($"File I/O error: {ex.Message}", ex);
                }
                catch (Exception ex)
                {
                    // Gérer les erreurs générales
                    throw new InvalidOperationException($"Error generating barcode: {ex.Message}", ex);
                }
            }
        }


        [HttpGet("GetBarcode/{pratiquantId}")]
    public IActionResult GetBarcode(int pratiquantId)
    {
        string rootBarcodePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "barcodes");

        // Valider et créer le dossier si nécessaire
        if (!Directory.Exists(rootBarcodePath))
        {
            Directory.CreateDirectory(rootBarcodePath);
        }

        string fileName = $"{pratiquantId}_barcode.png";
        string filePath = Path.Combine(rootBarcodePath, fileName);

        // Vérifier si le fichier existe
        if (!System.IO.File.Exists(filePath))
        {
            return NotFound("Barcode not found.");
        }

        // Lire et retourner le fichier
        byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
        return File(fileBytes, "image/png", fileName);
    }




        //// FUNCTION POUR IMPORTER LES DONNEES DES PRATIQUANTS EN FICHIER CSV
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile csvFile)
        {
            if (csvFile == null || csvFile.Length == 0)
            {
                ModelState.AddModelError("File", "Please upload a valid CSV file.");
                return RedirectToAction("Index");
            }

            if (Path.GetExtension(csvFile.FileName).ToLower() != ".csv")
            {
                ModelState.AddModelError("File", "Only CSV files are allowed.");
                return RedirectToAction("Index");
            }

            try
            {
                var pratiquants = new List<Pratiquant>();
                using (var streamReader = new StreamReader(csvFile.OpenReadStream()))
                {
                    string line;
                    int row = 0;

                    while ((line = await streamReader.ReadLineAsync()) != null)
                    {
                        row++;
                        if (row == 1) continue; // Skip the header row

                        var values = line.Split(',');

                        try
                        {
                            // Map values to Pratiquant
                            var session = _context.Sessions.FirstOrDefault(a => a.Nom.Equals(values[1]));
                            var activite = _context.Activites.FirstOrDefault(a => a.Nom.Equals(values[13]));
                            var categorie = _context.Categories.FirstOrDefault(a => a.Categories.Equals(values[14]));

                            var pratiquant = new Pratiquant
                            {
                                Id_session = session?.Id ?? 0,
                                Nom = values[2],
                                Sexe = values[3],
                                Naissance = TryParseWithDebug(() => ParseDate(values[4]), "Naissance"),
                                Payement = TryParseWithDebug(() => ParseBoolean(values[5]), "Payement"),
                                Consigne = TryParseWithDebug(() => ParseBoolean(values[6]), "Consigne"),
                                Carte_fede = TryParseWithDebug(() => ParseBoolean(values[7]), "Carte_fede"),
                                Etiquete = TryParseWithDebug(() => ParseBoolean(values[8]), "Etiquete"),
                                Courriel = values[9],
                                Adresse = values[10],
                                Telephone = values[11],
                                Tel_urgence = values[12],
                                Id_activite = activite?.Id ?? 0,
                                Id_categorie = categorie?.Id ?? 0,
                                Evaluation = values[15],
                                Mode_payement = values[16],
                                Carte_payement = values[17],
                                Groupe = TryParseWithDebug(() =>
                                {
                                    if (string.IsNullOrWhiteSpace(values[18]))
                                    {
                                        Debug.WriteLine("Groupe value is null or empty.");
                                        return "[]";
                                    }

                                    Debug.WriteLine($"Raw Groupe value: {values[18]}");

                                    try
                                    {

                                        var parsedValues = System.Text.Json.JsonSerializer.Deserialize<string[]>(values[18]);
                                        Debug.WriteLine($"Parsed Groupe value: {string.Join(",", parsedValues)}");
                                        return System.Text.Json.JsonSerializer.Serialize(parsedValues);
                                    }
                                    catch (System.Text.Json.JsonException ex)
                                    {
                                        Debug.WriteLine($"Error deserializing Groupe: {ex.Message}");
                                    }

                                    try
                                    {
                                        var cleanedValue = values[18].Replace("\"", "").Replace("[", "").Replace("]", "").Trim();
                                        Debug.WriteLine($"Fallback cleaned Groupe value: {cleanedValue}");
                                        var fallbackArray = cleanedValue.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                        return System.Text.Json.JsonSerializer.Serialize(fallbackArray);
                                    }
                                    catch (Exception ex)
                                    {
                                        Debug.WriteLine($"Unexpected error parsing Groupe: {ex.Message}");
                                    }

                                    return "[]";
                                }, "Groupe")
                        };

                            pratiquants.Add(pratiquant);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Error parsing row {row}: {ex.Message}");
                        }
                    }
                }


                using var transaction = await _context.Database.BeginTransactionAsync();

                try
                {
                    foreach (var pratiquant in pratiquants)
                    {
                        _context.Pratiquants.Add(pratiquant);
                        await _context.SaveChangesAsync();

                        // Create barcode for the Pratiquant
                        try
                        {
                            string rootBarcodePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "barcodes");
                            BarcodeHelper.GenerateBarcode(pratiquant.Id, rootBarcodePath);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Error generating barcode for Pratiquant {pratiquant.Id}: {ex.Message}");
                            // Optionally handle this error further, such as marking it for manual intervention
                        }

                        // Create Presence entries
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
                        }
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Debug.WriteLine($"Error during bulk insert: {ex.Message}");
                    ModelState.AddModelError("File", "An error occurred during the upload process.");
                    return RedirectToAction("Index");
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error uploading file: {ex.Message}");
                ModelState.AddModelError("File", "An error occurred during the upload process.");
                return RedirectToAction("Index");
            }
        }


        // Méthode utilitaire pour encapsuler les blocs de débogage et capturer les exceptions
        private T TryParseWithDebug<T>(Func<T> func, string fieldName)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur lors du traitement du champ '{fieldName}': {ex.Message}");
                throw; 
            }
        }



        //// FUNCTION POUR EXPORTER LES DONNEES PRATIQUANTS EN EXCEL

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
                worksheet.Cells[1, 20].Value = "Barcode";

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
                    string barcodePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "barcodes", $"{pratiquant.Id}_barcode.png");

                    if (System.IO.File.Exists(barcodePath))
                    {
                        var image = new FileInfo(barcodePath);
                        var picture = worksheet.Drawings.AddPicture($"Barcode_{i}", image);

                        // Positioning the image in the correct cell
                        picture.SetPosition(i + 1, 0, 16, 0); // Row index (i+1) and Column index (16 -> Col 17)
                        picture.SetSize(60, 60); // Resize image
                    }
                    else
                    {
                        worksheet.Cells[i + 2, 20].Value = "N/A"; // If barcode doesn't exist
                    }
                }

                // Auto-fit columns
                worksheet.Cells.AutoFitColumns();

                // Generate Excel file as a byte array
                var excelFile = package.GetAsByteArray();

                // Return the file
                return File(excelFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Pratiquants.xlsx");
            }
        }



        //// FUNCTION POUR AVOIR LES DONNEES A AFFICHER DANS LE CHART STATISTIQUE
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
