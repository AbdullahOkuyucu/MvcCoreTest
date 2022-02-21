using Microsoft.EntityFrameworkCore;
using MvcCoreTest.Entiti;
using MvcCoreTest.Models;
using MvcCoreTest.Services.Base;

namespace MvcCoreTest.Services
{
    public class UreticiServis : IUreticiServis
    {
        private readonly DbContext _db;

        public UreticiServis(DbContext db)
        {
            _db = db;
        }

        public IQueryable<UreticiModel> Query()
        {
            return _db.Set<Uretici>().Include(d => d.Modeller).OrderBy(d => d.FirmaAdi).Select(d => new UreticiModel()
            {
                Id = d.Id,
                FirmaAdi = d.FirmaAdi,
                FirmaLokasyon = d.FirmaLokasyon,
                UretimDurumu = d.UretimDurumu,

                Modeller = d.Modeller.Select(md => new ArabaModel()
                {
                    Id = md.Uretici.Id,
                    Adi = md.Uretici.FirmaAdi,
                    UretimYili = md.Araba.UretimYili
                }).ToList(),
            });
        }
        public ResultStatus Add(UreticiModel model)
        {
            try
            {
                if (_db.Set<Uretici>().Any(d => d.FirmaAdi.ToLower() == model.FirmaAdi.ToLower().Trim() && d.FirmaLokasyon.ToLower() == model.FirmaLokasyon.ToLower().Trim()))
                    return ResultStatus.EntityExists;

                Uretici entity = new Uretici()
                {
                    FirmaAdi = model.FirmaAdi.Trim(),
                    FirmaLokasyon = model.FirmaLokasyon.Trim(),
                    UretimDurumu = model.UretimDurumu,

                    
                };

                _db.Set<Uretici>().Add(entity);
                _db.SaveChanges();
                return ResultStatus.Success;
            }
            catch
            {
                return ResultStatus.Exception;
            }
        }
        public ResultStatus Update(UreticiModel model)
        {
            try
            {
                if (_db.Set<Uretici>().Any(d => d.FirmaAdi.ToLower() == model.FirmaAdi.ToLower().Trim() && d.FirmaLokasyon.ToLower() == model.FirmaLokasyon.ToLower().Trim() && d.Id != model.Id))
                    return ResultStatus.EntityExists;

                Uretici entity = _db.Set<Uretici>().SingleOrDefault(d => d.Id == model.Id);
                entity.FirmaAdi = model.FirmaAdi.Trim();
                entity.FirmaLokasyon = model.FirmaLokasyon.Trim();
                entity.UretimDurumu = model.UretimDurumu;

                _db.Set<Uretici>().Update(entity);
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
                Uretici entity = _db.Set<Uretici>().Include(d => d.Modeller).SingleOrDefault(d => d.Id == id);

                _db.Set<Model>().RemoveRange(entity.Modeller);
                _db.Set<Uretici>().Remove(entity);
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
