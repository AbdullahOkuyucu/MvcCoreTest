using MvcCoreTest.Models;

namespace MvcCoreTest.Services.Base
{
    public interface IUreticiServis
    {
        IQueryable<UreticiModel> Query();
        ResultStatus Add(UreticiModel model);
        ResultStatus Update(UreticiModel model);
        ResultStatus Delete(int id);
    }
}
