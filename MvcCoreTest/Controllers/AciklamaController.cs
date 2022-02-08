#nullable disable
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcCoreTest.Context;
using MvcCoreTest.Entiti;
using MvcCoreTest.Models;
using MvcCoreTest.Services.Base;

namespace MvcCoreTest.Controllers
{
    public class AciklamaController : Controller
    {
        private readonly ArabaContext _context;

        

        private readonly IAciklamaServis _aciklamaServis;
        private readonly IArabaServis _arabaServis;

        public AciklamaController(IAciklamaServis aciklamaServis, IArabaServis arabaServis)
        {
            _aciklamaServis = aciklamaServis;
            _arabaServis = arabaServis;
        }


        // GET: Aciklama
        //public async Task<IActionResult> Index()
        //{
        //    var arabaContext = _context.Aciklamalar.Include(a => a.Araba);
        //    return View(await arabaContext.ToListAsync());
        //}
        public IActionResult Index()
        {
            return View(_aciklamaServis.Query().ToList());
        }
        // GET: Aciklama/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var aciklama = await _context.Aciklamalar
        //        .Include(a => a.Araba)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (aciklama == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(aciklama);
        //}
        public IActionResult Details(int? id)
        {
            if (id == null)
                return View("MyError", "Id gereklidir!");
            var model = _aciklamaServis.Query().SingleOrDefault(r => r.Id == id.Value);
            if (model == null)
                return View("MyError", "Yorum bulunamadı!");
            return View(model);
        }
        // GET: Aciklama/Create
        //public IActionResult Create()
        //{
        //    ViewData["ArabaId"] = new SelectList(_context.Arabalar, "Id", "Id");
        //    return View();
        //}
        public IActionResult Create()
        {
            //ViewBag.Movies = new SelectList(_arabaServis.Query().ToList(), "Id", "Adi");
         
            //var model = new AciklamaModel()
            //{
            //    Id = 0,
            //    TarihModel = DateTime.Today.ToString("MM/dd/yyyy", new CultureInfo("en-US")),
                
            //};

            return View();
        }
        // POST: Aciklama/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Detay,Tarih,ArabaId")] Aciklama aciklama)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(aciklama);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["ArabaId"] = new SelectList(_context.Arabalar, "Id", "Id", aciklama.ArabaId);
        //    return View(aciklama);
        //}
        public IActionResult Create(string Detay, DateTime Tarih)
        {
            AciklamaModel model = new AciklamaModel()
            {
                Detay = Detay,
                Tarih = Tarih               
            };
            _aciklamaServis.Add(model);
            return RedirectToAction(nameof(Index));
            //if (ModelState.IsValid)
            //{
            //    var result = _aciklamaServis.Add(review);
            //    if (result == ResultStatus.Success)
            //    {
            //        TempData["Message"] = "Açıklama Oluşturuldu.";
            //        return RedirectToAction(nameof(Index));
            //    }
            //    return View("MyError"); // exception status result
            //}
            ////ViewBag.Araba = new SelectList(_arabaServis.Query().ToList(), "Id", "Adi", review.Araba);
            //return View(review);
        }
        // GET: Aciklama/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var aciklama = await _context.Aciklamalar.FindAsync(id);
        //    if (aciklama == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["ArabaId"] = new SelectList(_context.Arabalar, "Id", "Id", aciklama.ArabaId);
        //    return View(aciklama);
        //}
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return View("MyError", "Id gereklidir!");
            var model = _aciklamaServis.Query().SingleOrDefault(d => d.Id == id.Value);
            if (model == null)
                return View("MyError", "Yorum bulunamadı!");
            ViewBag.Movies = new SelectList(_arabaServis.Query().ToList(), "Id", "Adi", model.Araba);
            return View(model);
        }
        // POST: Aciklama/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Detay,Tarih,ArabaId")] Aciklama aciklama)
        //{
        //    if (id != aciklama.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(aciklama);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!AciklamaExists(aciklama.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["ArabaId"] = new SelectList(_context.Arabalar, "Id", "Id", aciklama.ArabaId);
        //    return View(aciklama);
        //}
        public IActionResult Edit(AciklamaModel aciklama)
        {
            if (ModelState.IsValid)
            {
                var result = _aciklamaServis.Update(aciklama);
                if (result == ResultStatus.Success)
                {
                    TempData["Message"] = "Açıklama Güncellendi.";
                    return RedirectToAction(nameof(Index));
                }
                return View("MyError"); // 
            }
            ViewBag.Movies = new SelectList(_arabaServis.Query().ToList(), "Id", "Adi", aciklama.Araba);
            return View(aciklama);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return View("MyError", "Id gereklidir!");
            var result = _aciklamaServis.Delete(id.Value);
            if (result == ResultStatus.Success)
            {
                TempData["Message"] = "Yorum Silindi.";
                return RedirectToAction(nameof(Index));
            }
            return View("MyError"); 
        }
        // GET: Aciklama/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var aciklama = await _context.Aciklamalar
        //        .Include(a => a.Araba)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (aciklama == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(aciklama);
        //}

        //// POST: Aciklama/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var aciklama = await _context.Aciklamalar.FindAsync(id);
        //    _context.Aciklamalar.Remove(aciklama);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool AciklamaExists(int id)
        //{
        //    return _context.Aciklamalar.Any(e => e.Id == id);
        //}
    }
}
