using StatistikkApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StatistikkApp.Data;
using StatistikkApp.Models;

namespace StatistikkApp.Controllers
{
    public class StatistikkDataController : Controller
    {
        private readonly ApplicationDbContext _context;
private readonly SsbService _ssbService;

public StatistikkDataController(ApplicationDbContext context, SsbService ssbService)
{
    _context = context;
    _ssbService = ssbService;
}

        // GET: StatistikkData
        public async Task<IActionResult> Index(int? kommuneId, int? kategoriId)
{
    ViewData["KommuneId"] = new SelectList(_context.Kommuner, "Id", "Navn", kommuneId);
    ViewData["KategoriId"] = new SelectList(_context.StatistikkKategorier, "Id", "Navn", kategoriId);

    var statistikk = _context.StatistikkData
    .Include(s => s.Kommune)
    .Include(s => s.StatistikkKategori)
    .OrderBy(s => s.Aar)
    .AsQueryable();

    if (kommuneId.HasValue)
    {
        statistikk = statistikk.Where(s => s.KommuneId == kommuneId.Value);
    }

    if (kategoriId.HasValue)
    {
        statistikk = statistikk.Where(s => s.StatistikkKategoriId == kategoriId.Value);
    }

    return View(await statistikk.ToListAsync());
}

        // GET: StatistikkData/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statistikkData = await _context.StatistikkData
                .Include(s => s.Kommune)
                .Include(s => s.StatistikkKategori)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (statistikkData == null)
            {
                return NotFound();
            }

            return View(statistikkData);
        }

        // GET: StatistikkData/Create
        public IActionResult Create()
        {
            ViewData["KommuneId"] = new SelectList(_context.Kommuner, "Id", "Navn");
            ViewData["StatistikkKategoriId"] = new SelectList(_context.StatistikkKategorier, "Id", "Navn");
            return View();
        }

        // POST: StatistikkData/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Aar,Verdi,KommuneId,StatistikkKategoriId")] StatistikkData statistikkData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(statistikkData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KommuneId"] = new SelectList(_context.Kommuner, "Id", "KommuneNummer", statistikkData.KommuneId);
            ViewData["StatistikkKategoriId"] = new SelectList(_context.StatistikkKategorier, "Id", "Navn", statistikkData.StatistikkKategoriId);
            return View(statistikkData);
        }

        // GET: StatistikkData/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statistikkData = await _context.StatistikkData.FindAsync(id);
            if (statistikkData == null)
            {
                return NotFound();
            }
            ViewData["KommuneId"] = new SelectList(_context.Kommuner, "Id", "KommuneNummer", statistikkData.KommuneId);
            ViewData["StatistikkKategoriId"] = new SelectList(_context.StatistikkKategorier, "Id", "Navn", statistikkData.StatistikkKategoriId);
            return View(statistikkData);
        }

        // POST: StatistikkData/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Aar,Verdi,KommuneId,StatistikkKategoriId")] StatistikkData statistikkData)
        {
            if (id != statistikkData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(statistikkData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatistikkDataExists(statistikkData.Id))
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
            ViewData["KommuneId"] = new SelectList(_context.Kommuner, "Id", "KommuneNummer", statistikkData.KommuneId);
            ViewData["StatistikkKategoriId"] = new SelectList(_context.StatistikkKategorier, "Id", "Navn", statistikkData.StatistikkKategoriId);
            return View(statistikkData);
        }

        // GET: StatistikkData/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statistikkData = await _context.StatistikkData
                .Include(s => s.Kommune)
                .Include(s => s.StatistikkKategori)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (statistikkData == null)
            {
                return NotFound();
            }

            return View(statistikkData);
        }

        // POST: StatistikkData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var statistikkData = await _context.StatistikkData.FindAsync(id);
            if (statistikkData != null)
            {
                _context.StatistikkData.Remove(statistikkData);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ImportFromSsb(int? kommuneId, int? kategoriId)
        {
            // 1. Sjekk at vi har valgt en kommune og en kategori
            if (!kommuneId.HasValue || !kategoriId.HasValue)
            {
                TempData["Error"] = "Vennligst velg både kommune og kategori før du oppdaterer.";
                return RedirectToAction(nameof(Index));
            }

            // 2. Finn kommunen i databasen for å få tak i KommuneNummeret (f.eks. "4601")
            var kommune = await _context.Kommuner.FindAsync(kommuneId.Value);
            var kategori = await _context.StatistikkKategorier.FindAsync(kategoriId.Value);

            if (kommune == null || kategori == null) return NotFound();

            // 3. Hent data fra SSB (Her bør SsbService ideelt sett ta inn kommune.KommuneNummer)
            // Siden jeg ikke ser SsbService-koden, antar vi at den henter data.
            // Hvis SsbService.GetPopulationData() kun henter Oslo, må den tjenesten også oppdateres.
            var ssbJson = await _ssbService.GetPopulationData();

            // 4. Opprett det nye data-punktet dynamisk
            // Merk: For en fullverdig løsning bør verdiene under hentes ut fra ssbJson-variabelen
            var statistikk = new StatistikkData
            {
                Aar = 2025, 
                Verdi = 275000, // Eksempelverdi for Bergen - bør egentlig parses fra ssbJson
                KommuneId = kommune.Id,
                StatistikkKategoriId = kategori.Id
            };

            // 5. Sjekk om dataen finnes fra før for å unngå duplikater
            var exists = await _context.StatistikkData.AnyAsync(s =>
                s.Aar == statistikk.Aar &&
                s.KommuneId == statistikk.KommuneId &&
                s.StatistikkKategoriId == statistikk.StatistikkKategoriId
            );

            if (!exists)
            {
                _context.StatistikkData.Add(statistikk);
                await _context.SaveChangesAsync();
            }

            // Returner til Index med de samme filterne valgt
            return RedirectToAction(nameof(Index), new { kommuneId = kommuneId, kategoriId = kategoriId });
        }

        private bool StatistikkDataExists(int id)
        {
            return _context.StatistikkData.Any(e => e.Id == id);
        }
    }
}
