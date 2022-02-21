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
using MvcCoreTest.Models;
using MvcCoreTest.Services.Base;

namespace MvcCoreTest.Controllers
{
    public class UreticisController : Controller
    {

        private readonly IUreticiServis _ureticiServis;
        private readonly IArabaServis _arabaServis;

        public UreticisController(IUreticiServis ureticiServis, IArabaServis arabaServis)
        {
            _ureticiServis = ureticiServis;
            _arabaServis = arabaServis;
        }

        
        public IActionResult Index()
        {
            return View(_ureticiServis.Query().ToList());
        }

        
        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
                return View("MyError", "Id Gereklidir!");
            var model = _ureticiServis.Query().SingleOrDefault(d => d.Id == id.Value);
            if (model == null)
                return View("MyError", "Üretici bulunamadı!");
            return View(model);
        }
       
        public IActionResult Create()
        {           
            return View();
        }
        
        [HttpPost]
        //[ValidateAntiForgeryToken]
        
        public IActionResult Create(string FirmaAdi, string FirmaLokasyon, bool UretimDurumu)
        {
            UreticiModel model = new UreticiModel()
            {
                FirmaAdi = FirmaAdi,
                FirmaLokasyon = FirmaLokasyon,
                UretimDurumu = UretimDurumu
            };
            _ureticiServis.Add(model);
            return RedirectToAction(nameof(Index));           
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("MyError", "Id Gereklidir!");
            }
            UreticiModel model = _ureticiServis.Query().SingleOrDefault(d => d.Id == id.Value);
            if (model == null)
            {
                return View("MyError", "Üretici bulunamadı!");
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public IActionResult Edit(UreticiModel uretici)
        {
            if (ModelState.IsValid)
            {
                ResultStatus result = _ureticiServis.Update(uretici);
                if (result == ResultStatus.Success)
                {
                    TempData["Message"] = "Üretici Güncellendi.";
                    return RedirectToAction(nameof(Index));
                }
                if (result == ResultStatus.Exception)
                {
                    return View("MyError");
                }
            }           
            return View(uretici);
        }

       
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
                return View("MyError", "Id Gereklidir!");
            var model = _ureticiServis.Query().SingleOrDefault(d => d.Id == id.Value);
            if (model == null)
                return View("MyError", "Uretici Bulunamadı!");
            return View(model);
        }

        [HttpPost, ActionName("Delete")] 
        [ValidateAntiForgeryToken]
        
        public IActionResult DeleteConfirmed(int id)
        {
            var result = _ureticiServis.Delete(id);
            if (result == ResultStatus.Success)
            {
                TempData["Message"] = "Üretici Silindi.";
                return RedirectToAction(nameof(Index));
            }
            return View("MyError"); 
        }

        
    }
}
