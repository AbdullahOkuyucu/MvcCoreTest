namespace MvcCoreTest.Entiti
{
    public class Aciklama
    {
        public int Id { get; set; }
        public string Detay { get; set; }
        public DateTime Tarih { get; set; }
        public int ArabaId { get; set; }
        public Araba Araba { get; set; }
    }
}
