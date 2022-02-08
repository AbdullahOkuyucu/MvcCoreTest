using Microsoft.AspNetCore.Mvc;
using MvcCoreTest.Models;
using MvcCoreTest.Services.Base;

namespace MvcCoreTest.Controllers
{
    public class ArabaController : Controller
    {
        private readonly IArabaServis _arabaServis;
        private readonly IAciklamaServis _aciklamaServis;

        public ArabaController(IArabaServis arabaServis, IAciklamaServis aciklamaServis)
        {
            _arabaServis = arabaServis;
            _aciklamaServis = aciklamaServis;
        }
        public IActionResult Index()
        {
            List<ArabaModel> model = _arabaServis.Query().ToList();
            return View(model);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest("İd gereklidir");
            }
            ArabaModel model = _arabaServis.Query().SingleOrDefault(m => m.Id == id.Value);
            if (model == null)
            {
                return NotFound("Araba Bulunamadı");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Create(string Adi, short? UretimYili, double? Fiyat)
        {
            ArabaModel model = new ArabaModel()
            {
                Adi = Adi,
                UretimYili = UretimYili,
                Fiyat = Fiyat
            };
            _arabaServis.Add(model);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("MyError", "Id gereklidir!");
            }
            ArabaModel model = _arabaServis.Query().SingleOrDefault(m => m.Id == id.Value);
            if (model == null)
            {
                return View("MyError", "Araba bulunamadı!");
            }
                
            return View(model);

        }
        [HttpPost]
        public IActionResult Edit(ArabaModel model) 
        {
            if (ModelState.IsValid)
            {
                ResultStatus result = _arabaServis.Update(model);
                if (result == ResultStatus.Success)
                {
                    TempData["Message"] = "Araç bilgileri güncellendi..";
                    return RedirectToAction(nameof(Index));
                }
                if (result == ResultStatus.Exception)
                {
                    return View("MyError");
                }
            }
            
            return View(model);
        }
        public IActionResult Delete(int? id) 
        {
            if (id == null)
                return View("MyError", "Id Gereklidir");
            ResultStatus result = _arabaServis.Delete(id.Value);
            if (result == ResultStatus.Success)
            {
                TempData["Message"] = "Araba Silindi.";
                return RedirectToAction(nameof(Index));
            }
            if (result == ResultStatus.RelationalEntitiesExist)
            {
                TempData["Message"] = "Detay Olduğu İçin Araba Kaydı Silinemez!";
                return RedirectToAction(nameof(Index));
            }
            return View("MyError"); 
        }
    }
}
