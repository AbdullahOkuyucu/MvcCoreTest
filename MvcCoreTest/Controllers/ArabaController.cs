using Microsoft.AspNetCore.Mvc;
using MvcCoreTest.Models;
using MvcCoreTest.Services.Base;

namespace MvcCoreTest.Controllers
{
    public class ArabaController : Controller
    {
        private readonly IArabaServis _arabaServis;

        public ArabaController(IArabaServis arabaServis)
        {
            _arabaServis = arabaServis;
        }
        public IActionResult Index()
        {
            List<ArabaModel> model = _arabaServis.GetList();
            return View(model);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest("İd gereklidir");
            }
            ArabaModel model = _arabaServis.GetDetails(id.Value);
            if(model == null)
            {
                return NotFound();
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
            if(id == null)
            {
                return View("MyError", "Id gereklidir!");
            }
            ArabaModel model = _arabaServis.GetDetails(id.Value);
            if(model == null)
                return View("MyError", "Araba bulunamadı!");
            return View(model);
        }
    }
}
