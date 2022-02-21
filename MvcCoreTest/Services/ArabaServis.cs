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

        public IQueryable<ArabaModel> Query()
        {
            return _db.Set<Araba>().Include(m => m.Aciklamalar).Include(m => m.Modeller).ThenInclude(md => md.Uretici).OrderBy(m => m.Adi).Select(m => new ArabaModel()
            {
                Id = m.Id,
                Adi = m.Adi,
                UretimYili = m.UretimYili,
                Fiyat = m.Fiyat,
                FiyatModel = m.Fiyat.HasValue ? m.Fiyat.Value.ToString(new CultureInfo("tr-TR")) : "",
            });
        }

        public ResultStatus Add(ArabaModel model)
        {
            try
            {
                if (_db.Set<Araba>().Any(m => m.Adi.ToUpper() == model.Adi.ToUpper().Trim()))
                    return ResultStatus.EntityExists;

                Araba entity = new Araba()
                {
                    Adi = model.Adi.Trim(),
                    UretimYili = model.UretimYili,
                    Fiyat = model.Fiyat

                };

                if (!string.IsNullOrWhiteSpace(model.FiyatModel))
                {
                    double fiyat;
                    if (double.TryParse(model.FiyatModel.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out fiyat))
                    {
                        entity.Fiyat = fiyat;
                    }
                    else
                    {
                        return ResultStatus.StringToDoubleConversionFailed;
                    }
                }
                _db.Set<Araba>().Add(entity);
                _db.SaveChanges();
                return ResultStatus.Success;
            }

            catch

            {
                return ResultStatus.Exception;
            }
        }

        public ResultStatus Update(ArabaModel model)
        {
            try
            {
                if (_db.Set<Araba>().Any(m => m.Adi.ToUpper() == model.Adi.ToUpper().Trim() && m.Id != model.Id))
                    return ResultStatus.EntityExists;

                Araba entity = _db.Set<Araba>().SingleOrDefault(m => m.Id == model.Id);

                //_db.Set<Model>().RemoveRange(entity.Modeller);

                entity.Adi = model.Adi.Trim();
                entity.UretimYili = model.UretimYili;
                entity.Fiyat = model.Fiyat;
                if (!string.IsNullOrWhiteSpace(model.FiyatModel))
                {
                    double fiyat;
                    if (double.TryParse(model.FiyatModel.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out fiyat))
                    {
                        entity.Fiyat = fiyat;
                    }
                    else
                    {
                        return ResultStatus.StringToDoubleConversionFailed;
                    }
                    
                }
                
                _db.Set<Araba>().Update(entity);
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
                Araba entity = _db.Set<Araba>().Include(m => m.Modeller).Include(m => m.Aciklamalar).SingleOrDefault(m => m.Id == id);

                _db.Set<Model>().RemoveRange(entity.Modeller);

                if (entity.Aciklamalar != null && entity.Aciklamalar.Count > 0)
                {
                    return ResultStatus.RelationalEntitiesExist;
                }
                _db.Set<Araba>().Remove(entity);
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
