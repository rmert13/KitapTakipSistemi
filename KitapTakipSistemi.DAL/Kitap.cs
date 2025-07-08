using System.ComponentModel.DataAnnotations;

namespace KitapTakipSistemi.DAL
{
    public class Kitap
    {
        public int KitapId { get; set; }

        [Required]
        [StringLength(150)]
        public string Ad { get; set; }

        [Required]
        [StringLength(100)]
        public string Yazar { get; set; }

        [Range(1900, 2025)]
        public int YayinYili { get; set; }

        [Range(0, int.MaxValue)]
        public int Stok { get; set; }

        public string FotoUrl { get; set; } // Fotoğraf yolu, opsiyonel


        public int TurId { get; set; }

        public virtual Tur Tur { get; set; }
    }
}
