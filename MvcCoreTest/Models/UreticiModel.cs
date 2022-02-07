using System.ComponentModel;

namespace MvcCoreTest.Models
{
    public class UreticiModel
    {
        public int Id { get; set; }

        [DisplayName("Firma Adı")]
        public string FirmaAdi { get; set; }

        [DisplayName("Firma Lokasyonu")]
        public string FirmaLokasyon { get; set; }

        [DisplayName("Üretim Durumu")]
        public bool UretimDurumu { get; set; }
        public List<ArabaModel> Modeller { get; set; }
    }
}
