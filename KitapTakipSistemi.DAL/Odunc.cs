using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KitapTakipSistemi.DAL
{
    public class Odunc
    {
        public int OduncId { get; set; }

        [Required]
        [StringLength(100)]
        public string KullaniciAdi { get; set; }

        [Required]
        public int KitapId { get; set; }

        public DateTime OduncTarihi { get; set; } = DateTime.Now;

        public bool IadeEdildi { get; set; } = false;

        public DateTime? OduncIadeTarihi { get; set; } // ✅ Bu satırı ekledik

        // Navigation Property
        [ForeignKey("KitapId")]
        public virtual Kitap Kitap { get; set; }
    }
}
