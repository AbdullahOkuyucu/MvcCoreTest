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
        public IActionResult Index()
        {
            return View(_aciklamaServis.Query().ToList());
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
                return View("MyError", "Id gereklidir!");
            var model = _aciklamaServis.Query().SingleOrDefault(r => r.Id == id.Value);
            if (model == null)
                return View("MyError", "Yorum bulunamadı!");
            return View(model);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string Detay, DateTime Tarih)
        {
            AciklamaModel model = new AciklamaModel()
            {
                Detay = Detay,
                Tarih = Tarih               
            };
            _aciklamaServis.Add(model);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return View("MyError", "Id gereklidir!");
            var model = _aciklamaServis.Query().SingleOrDefault(d => d.Id == id.Value);
            if (model == null)
                return View("MyError", "Yorum bulunamadı!");
            ViewBag.Araba = new SelectList(_arabaServis.Query().ToList(), "Id", "Adi", model.Araba);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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
            ViewBag.Araba = new SelectList(_arabaServis.Query().ToList(), "Id", "Adi", aciklama.Araba);
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
    }
}
