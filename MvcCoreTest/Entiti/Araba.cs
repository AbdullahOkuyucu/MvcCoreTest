namespace MvcCoreTest.Entiti
{
    public class Araba
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public short? UretimYili { get; set; }
        public double? Fiyat { get; set; }
        public List<Model> Modeller { get; set; }
        public List<Aciklama> Aciklamalar { get; set; }
    }
}
