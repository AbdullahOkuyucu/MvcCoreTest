using MvcCoreTest.Models;

namespace MvcCoreTest.Services.Base
{
    public interface IAciklamaServis
    {
        IQueryable<AciklamaModel> Query();
        ResultStatus Add(AciklamaModel model);
        ResultStatus Update(AciklamaModel model);
        ResultStatus Delete(int id);
    }
}
