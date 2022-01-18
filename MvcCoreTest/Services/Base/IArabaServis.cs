using MvcCoreTest.Models;

namespace MvcCoreTest.Services.Base
{
    public interface IArabaServis
    {
        List<ArabaModel> GetList();

        ArabaModel GetDetails(int id);

        void Add(ArabaModel model);
    }
}
