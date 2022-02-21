using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcCoreTest.Models
{
    public class ArabaModel
    {

        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        [DisplayName("Araba Adı")]
        public string? Adi { get; set; }

        [DisplayName("Üretim Yılı")]
        public short? UretimYili { get; set; }
      
        public double? Fiyat { get; set; }

        [DisplayName("Fiyat")]
        public string? FiyatModel { get; set; }

    }
}
