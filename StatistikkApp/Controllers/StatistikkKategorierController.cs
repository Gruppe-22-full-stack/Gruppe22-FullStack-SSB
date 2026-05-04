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
    public class StatistikkKategorierController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatistikkKategorierController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StatistikkKategorier
        public async Task<IActionResult> Index()
        {
            return View(await _context.StatistikkKategorier.ToListAsync());
        }

        // GET: StatistikkKategorier/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statistikkKategori = await _context.StatistikkKategorier
                .FirstOrDefaultAsync(m => m.Id == id);
            if (statistikkKategori == null)
            {
                return NotFound();
            }

            return View(statistikkKategori);
        }

        // GET: StatistikkKategorier/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StatistikkKategorier/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Navn")] StatistikkKategori statistikkKategori)
        {
            if (ModelState.IsValid)
            {
                _context.Add(statistikkKategori);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(statistikkKategori);
        }

        // GET: StatistikkKategorier/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statistikkKategori = await _context.StatistikkKategorier.FindAsync(id);
            if (statistikkKategori == null)
            {
                return NotFound();
            }
            return View(statistikkKategori);
        }

        // POST: StatistikkKategorier/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Navn")] StatistikkKategori statistikkKategori)
        {
            if (id != statistikkKategori.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(statistikkKategori);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatistikkKategoriExists(statistikkKategori.Id))
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
            return View(statistikkKategori);
        }

        // GET: StatistikkKategorier/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statistikkKategori = await _context.StatistikkKategorier
                .FirstOrDefaultAsync(m => m.Id == id);
            if (statistikkKategori == null)
            {
                return NotFound();
            }

            return View(statistikkKategori);
        }

        // POST: StatistikkKategorier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var statistikkKategori = await _context.StatistikkKategorier.FindAsync(id);
            if (statistikkKategori != null)
            {
                _context.StatistikkKategorier.Remove(statistikkKategori);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatistikkKategoriExists(int id)
        {
            return _context.StatistikkKategorier.Any(e => e.Id == id);
        }
    }
}
