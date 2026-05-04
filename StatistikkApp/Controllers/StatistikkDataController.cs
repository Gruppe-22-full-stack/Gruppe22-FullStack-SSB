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

        public StatistikkDataController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StatistikkData
        public async Task<IActionResult> Index(int? kommuneId, int? kategoriId)
{
    ViewData["KommuneId"] = new SelectList(_context.Kommuner, "Id", "Navn", kommuneId);
    ViewData["KategoriId"] = new SelectList(_context.StatistikkKategorier, "Id", "Navn", kategoriId);

    var statistikk = _context.StatistikkData
        .Include(s => s.Kommune)
        .Include(s => s.StatistikkKategori)
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

        private bool StatistikkDataExists(int id)
        {
            return _context.StatistikkData.Any(e => e.Id == id);
        }
    }
}
