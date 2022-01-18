using Microsoft.EntityFrameworkCore;
using MvcCoreTest.Entiti;
using MvcCoreTest.Models;
using MvcCoreTest.Services.Base;
using System.Globalization;

namespace MvcCoreTest.Services
{
    public class ArabaServis : IArabaServis
    {
        private readonly DbContext _db;

        public ArabaServis(DbContext db)
        {
            _db = db;
        }

        public void Add(ArabaModel model)
        {
            try
            {
                Araba entity = new Araba()
                {
                    Adi = model.Adi.Trim(),
                    UretimYili = model.UretimYili,
                    Fiyat = model.Fiyat
                };
                _db.Set<Araba>().Add(entity);
                _db.SaveChanges();
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }

        public ArabaModel GetDetails(int id)
        {
            try
            {
                ArabaModel model = null;
                Araba entity = _db.Set<Araba>().Include(a => a.Aciklamalar).Include(a => a.Modeller).ThenInclude(mm => mm.Uretici).SingleOrDefault(a => a.Id == id);
                if (entity != null)
                {
                    model = new ArabaModel()
                    {
                        Id = entity.Id,
                        Adi = entity.Adi,
                        UretimYili = entity.UretimYili,
                        Fiyat = entity.Fiyat,
                        FiyatModel = (entity.Fiyat ?? 0).ToString("C2", new CultureInfo("tr-TR")),
 
                    };
                }
                return model;
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }

        public List<ArabaModel> GetList()
        {
            try
            {
                return _db.Set<Araba>().OrderBy(a => a.Adi).Select(a => new ArabaModel()
                {
                    Id = a.Id,
                    Adi = a.Adi,
                    UretimYili = a.UretimYili,
                    Fiyat = a.Fiyat,
                    FiyatModel = (a.Fiyat ?? 0).ToString("C2", new CultureInfo("tr-TR"))
                }).ToList();
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
    }
}
