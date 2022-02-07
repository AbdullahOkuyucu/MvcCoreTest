using MvcCoreTest.Models;

namespace MvcCoreTest.Services.Base
{
    public interface IArabaServis
    {
        IQueryable<ArabaModel> Query(); 
        ResultStatus Add(ArabaModel model);
        ResultStatus Update(ArabaModel model);
        ResultStatus Delete(int id);
    }
}
