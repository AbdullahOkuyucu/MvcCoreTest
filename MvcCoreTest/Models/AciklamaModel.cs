using MvcCoreTest.Entiti;
using System.ComponentModel;

namespace MvcCoreTest.Models
{
    public class AciklamaModel
    {
        public int Id { get; set; }
        public string Detay { get; set; }
        public DateTime Tarih { get; set; }

        [DisplayName("Araba")]
        public int ArabaId { get; set; }
        public Araba? Araba { get; set; }

        [DisplayName("Tarih")]
        public string? TarihModel { get; set; }
    }
}
