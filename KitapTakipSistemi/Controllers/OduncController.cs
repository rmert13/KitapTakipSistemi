using System;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using KitapTakipSistemi.DAL;
using Serilog;

namespace KitapTakipSistemi.Controllers
{
    public class OduncController : Controller
    {
        private KitapContext db = new KitapContext();

        // Ödünç alınan kitapları listele
        public ActionResult Index()
        {
            var oduncler = db.Oduncler.Include(o => o.Kitap).ToList();
            return View(oduncler);
        }

        // GET: Ödünç alma formu
        public ActionResult Create()
        {
            ViewBag.KitapId = new SelectList(db.Kitaplar.Where(k => k.Stok > 0), "KitapId", "Ad");
            return View();
        }

        // POST: Ödünç alma işlemi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Odunc odunc)
        {
            if (ModelState.IsValid)
            {
                var kitap = db.Kitaplar.Find(odunc.KitapId);
                if (kitap == null || kitap.Stok == 0)
                {
                    ModelState.AddModelError("", "Seçilen kitap stokta yok!");
                    ViewBag.KitapId = new SelectList(db.Kitaplar.Where(k => k.Stok > 0), "KitapId", "Ad", odunc.KitapId);
                    return View(odunc);
                }

                kitap.Stok -= 1;
                odunc.IadeEdildi = false;
                odunc.OduncTarihi = DateTime.Now;

                db.Oduncler.Add(odunc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KitapId = new SelectList(db.Kitaplar.Where(k => k.Stok > 0), "KitapId", "Ad", odunc.KitapId);
            return View(odunc);
        }

        // GET: Kitap iade formu (IadeEt)
        public ActionResult IadeEt(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var odunc = db.Oduncler.Include(o => o.Kitap).FirstOrDefault(o => o.OduncId == id);

            if (odunc == null)
                return HttpNotFound();

            return View(odunc);
        }

        // POST: Kitap iade işlemi
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult IadeEt(int OduncId)
        {
            var odunc = db.Oduncler.Include(o => o.Kitap).FirstOrDefault(o => o.OduncId == OduncId);

            if (odunc == null)
                return HttpNotFound();

            if (!odunc.IadeEdildi)
            {
                odunc.IadeEdildi = true;
                odunc.OduncIadeTarihi = DateTime.Now;
                odunc.Kitap.Stok += 1;
                db.SaveChanges();

                // ✅ Loglama
                Log.Information("📘 Kitap iade edildi: {KitapAd} | Kullanıcı: {Kullanici} | Tarih: {Tarih}",
                                odunc.Kitap.Ad, User.Identity.Name ?? "Bilinmiyor", DateTime.Now);

                // ✅ Sadece iade işlemi sonrası mesaj
                TempData["Success"] = $"📚 \"{odunc.Kitap.Ad}\" kitabı başarıyla iade edilmiştir!";
            }

            return RedirectToAction("Index");
        }


        // Silme işlemi (isteğe bağlı, genelde iade edilen kitaplar silinmez)
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Odunc odunc = db.Oduncler.Include(o => o.Kitap).FirstOrDefault(o => o.OduncId == id);
            if (odunc == null)
                return HttpNotFound();

            return View(odunc);
        }

        // POST: Silme onayı
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Odunc odunc = db.Oduncler.Find(id);
            if (odunc != null)
            {
                var kitap = db.Kitaplar.Find(odunc.KitapId);
                if (kitap != null)
                {
                    kitap.Stok += 1;
                }
                db.Oduncler.Remove(odunc);
                db.SaveChanges();
                
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}
