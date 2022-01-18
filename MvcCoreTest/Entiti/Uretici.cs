namespace MvcCoreTest.Entiti
{
    public class Uretici
    {
        public int Id { get; set; }
        public string FirmaAdi { get; set; }
        public string FirmaLokasyon { get; set; }
        public bool UretimDurumu { get; set; }
        public List<Model> Modeller { get; set; }
    }
}
