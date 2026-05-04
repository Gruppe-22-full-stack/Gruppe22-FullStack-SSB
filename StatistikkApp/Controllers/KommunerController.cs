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
    public class KommunerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KommunerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Kommuner
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kommuner.ToListAsync());
        }

        // GET: Kommuner/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kommune = await _context.Kommuner
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kommune == null)
            {
                return NotFound();
            }

            return View(kommune);
        }

        // GET: Kommuner/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kommuner/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Navn,KommuneNummer")] Kommune kommune)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kommune);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kommune);
        }

        // GET: Kommuner/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kommune = await _context.Kommuner.FindAsync(id);
            if (kommune == null)
            {
                return NotFound();
            }
            return View(kommune);
        }

        // POST: Kommuner/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Navn,KommuneNummer")] Kommune kommune)
        {
            if (id != kommune.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kommune);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KommuneExists(kommune.Id))
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
            return View(kommune);
        }

        // GET: Kommuner/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kommune = await _context.Kommuner
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kommune == null)
            {
                return NotFound();
            }

            return View(kommune);
        }

        // POST: Kommuner/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kommune = await _context.Kommuner.FindAsync(id);
            if (kommune != null)
            {
                _context.Kommuner.Remove(kommune);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KommuneExists(int id)
        {
            return _context.Kommuner.Any(e => e.Id == id);
        }
    }
}
