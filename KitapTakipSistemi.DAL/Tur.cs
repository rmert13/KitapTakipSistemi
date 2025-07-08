using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KitapTakipSistemi.DAL
{
    public class Tur
    {
        public int TurId { get; set; }

        [Required]
        [StringLength(100)]
        public string Ad { get; set; }

        public virtual ICollection<Kitap> Kitaplar { get; set; }
    }
}
