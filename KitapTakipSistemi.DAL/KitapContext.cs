using System.Data.Entity;

namespace KitapTakipSistemi.DAL
{
    public class KitapContext : DbContext
    {
        public KitapContext() : base("name=KitapDBBaglanti")
        {
            // Lazy loading'i kapatıyoruz
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Tur> Turler { get; set; }
        public DbSet<Kitap> Kitaplar { get; set; }
        public DbSet<Odunc> Oduncler { get; set; }  // Buraya ekledik
    }
}
