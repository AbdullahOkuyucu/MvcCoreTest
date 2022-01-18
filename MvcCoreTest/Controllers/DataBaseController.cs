using Microsoft.AspNetCore.Mvc;
using MvcCoreTest.Context;
using MvcCoreTest.Entiti;

namespace MvcCoreTest.Controllers
{
    public class DataBaseController : Controller
    {
        public IActionResult Seed()
        {
            using (ArabaContext db = new ArabaContext())
            {
                List<Araba> arabaList = new List<Araba>
                {
                    new Araba()
                    {
                        Adi = "Mercedes",
                        UretimYili = 2010,
                        Fiyat = 1000000
                    },
                    new Araba()
                    {
                        Adi = "Bmw",
                        UretimYili = 2011,
                        Fiyat = 2000000
                    },
                    new Araba()
                    {
                        Adi = "Range Rover",
                        UretimYili = 2012,
                        Fiyat = 3000000
                    },
                };
                List<Uretici> ureticiList = new List<Uretici>
                {
                    new Uretici()
                    {
                        FirmaAdi = "Mers",
                        FirmaLokasyon = "ANKARA",
                        UretimDurumu = true
                    },
                    new Uretici()
                    {
                        FirmaAdi = "Bemeve",
                        FirmaLokasyon = "İZMİR",
                        UretimDurumu = false
                    },
                    new Uretici()
                    {
                        FirmaAdi = "RRevor",
                        FirmaLokasyon = "İSTANBUL",
                        UretimDurumu = false
                    },
                };
                List<Aciklama> aciklamList = new List<Aciklama>
                {
                    new Aciklama()
                    {
                        Detay = "20. Yıl Özel Seri",
                        Tarih = DateTime.Parse("16.01.2022"),
                        Araba = arabaList[0]
                    },
                    new Aciklama()
                    {
                        Detay = "30. Yıl Özel Seri",
                        Tarih = DateTime.Parse("15.01.2022"),
                        Araba = arabaList[1]
                    },
                    new Aciklama()
                    {
                        Detay = "40. Yıl Özel Seri",
                        Tarih = DateTime.Parse("14.01.2022"),
                        Araba = arabaList[2]
                    },
                };
                List<Model> modelList = new List<Model>()
                {
                    new Model()
                    {
                        Araba = arabaList[0],
                        Uretici = ureticiList[0]
                    },
                    new Model()
                    {
                        Araba = arabaList[1],
                        Uretici = ureticiList[1]
                    },
                    new Model()
                    {
                        Araba = arabaList[2],
                        Uretici = ureticiList[2]
                    },
                };
                var model = db.Modellers.ToList();
                db.Modellers.RemoveRange(model);
                var aciklamalar = db.Aciklamalar.ToList();
                db.Aciklamalar.RemoveRange(aciklamalar);
                var arabalar = db.Arabalar.ToList();
                db.Arabalar.RemoveRange(arabalar);
                var uretici = db.Ureticiler.ToList();
                db.Ureticiler.RemoveRange(uretici);
                db.SaveChanges();

                db.Modellers.AddRange(modelList);
                db.Aciklamalar.AddRange(aciklamList);
                db.SaveChanges();
            };
            return Content("<label style=\"color:red;\"><b>Database seed successful.</b></label>", "text/html");
        }
    }
}
