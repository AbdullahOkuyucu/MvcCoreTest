#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcCoreTest.Context;
using MvcCoreTest.Entiti;

namespace MvcCoreTest.Controllers
{
    public class UreticisController : Controller
    {
        private readonly ArabaContext _context;

        public UreticisController(ArabaContext context)
        {
            _context = context;
        }

        // GET: Ureticis
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ureticiler.ToListAsync());
        }

        // GET: Ureticis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uretici = await _context.Ureticiler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (uretici == null)
            {
                return NotFound();
            }

            return View(uretici);
        }

        // GET: Ureticis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ureticis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirmaAdi,FirmaLokasyon,UretimDurumu")] Uretici uretici)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uretici);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(uretici);
        }

        // GET: Ureticis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uretici = await _context.Ureticiler.FindAsync(id);
            if (uretici == null)
            {
                return NotFound();
            }
            return View(uretici);
        }

        // POST: Ureticis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirmaAdi,FirmaLokasyon,UretimDurumu")] Uretici uretici)
        {
            if (id != uretici.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uretici);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UreticiExists(uretici.Id))
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
            return View(uretici);
        }

        // GET: Ureticis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uretici = await _context.Ureticiler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (uretici == null)
            {
                return NotFound();
            }

            return View(uretici);
        }

        // POST: Ureticis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var uretici = await _context.Ureticiler.FindAsync(id);
            _context.Ureticiler.Remove(uretici);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UreticiExists(int id)
        {
            return _context.Ureticiler.Any(e => e.Id == id);
        }
    }
}
