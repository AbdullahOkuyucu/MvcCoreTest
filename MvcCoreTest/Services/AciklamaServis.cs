using Microsoft.EntityFrameworkCore;
using MvcCoreTest.Entiti;
using MvcCoreTest.Models;
using MvcCoreTest.Services.Base;
using System.Globalization;

namespace MvcCoreTest.Services
{
    public class AciklamaServis : IAciklamaServis
    {
        private readonly DbContext _db;

        public AciklamaServis(DbContext db)
        {
            _db = db;
        }

        public IQueryable<AciklamaModel> Query()
        {
            return _db.Set<Aciklama>().Include(r => r.Araba).OrderByDescending(r => r.Tarih).ThenBy(r => r.Detay)
                .ThenBy(r => r.Araba.Adi).Select(r => new AciklamaModel()
                {
                    Id = r.Id,
                    Detay = r.Detay,
                    Tarih = r.Tarih,
                    ArabaId = r.ArabaId,
                    

                    Araba = new Araba()
                    {
                        Id = r.Araba.Id,
                        Adi = r.Araba.Adi
                    },

                    TarihModel = r.Tarih.ToString("MM/dd/yyyy", new CultureInfo("en-US")),
                    
                });
        }

        public ResultStatus Add(AciklamaModel model)
        {
            try
            {
                Aciklama entity = new Aciklama()
                {
                    Tarih = DateTime.Parse(model.TarihModel, new CultureInfo("en-US")),
                    Detay = model.Detay?.Trim(),
                    ArabaId = model.ArabaId,
                   
                };
                _db.Set<Aciklama>().Add(entity);
                _db.SaveChanges();
                return ResultStatus.Success;
            }
            catch
            {
                return ResultStatus.Exception;
            }
        }

        public ResultStatus Update(AciklamaModel model)
        {
            try
            {
                Aciklama entity = _db.Set<Aciklama>().Find(model.Id);

                entity.Tarih = DateTime.Parse(model.TarihModel, new CultureInfo("en-US"));
                entity.Detay = model.Detay.Trim();
                entity.ArabaId = model.ArabaId;


                _db.Set<Aciklama>().Update(entity);
                _db.SaveChanges();
                return ResultStatus.Success;
            }
            catch
            {
                return ResultStatus.Exception;
            }
        }

        public ResultStatus Delete(int id)
        {
            try
            {
                var entity = _db.Set<Aciklama>().Find(id);
                _db.Set<Aciklama>().Remove(entity);
                _db.SaveChanges();
                return ResultStatus.Success;
            }
            catch
            {
                return ResultStatus.Exception;
            }
        }
    }
}
