using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity; // Tür bilgisini Include etmek için
using KitapTakipSistemi.DAL;

namespace KitapTakipSistemi.Controllers
{
    public class HomeController : Controller
    {
        private KitapContext db = new KitapContext();

        public ActionResult Index()
        {
            // Kitapları tür bilgisiyle birlikte çekiyoruz
            var kitaplar = db.Kitaplar.Include("Tur").ToList();

            if (kitaplar == null)
            {
                kitaplar = new List<Kitap>();
            }

            return View(kitaplar);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}
