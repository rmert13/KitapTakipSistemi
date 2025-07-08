using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KitapTakipSistemi.DAL;
using Serilog;

namespace KitapTakipSistemi.Controllers
{
    public class KitapController : Controller
    {
        private KitapContext db = new KitapContext();

        // GET: Kitap
        public ActionResult Index(string arama, string yazarFiltre, int? turFiltre, string stokDurumu)
        {
            var kitaplar = db.Kitaplar.Include(k => k.Tur).AsQueryable();

            if (!string.IsNullOrEmpty(arama))
                kitaplar = kitaplar.Where(k => k.Ad.Contains(arama));

            if (!string.IsNullOrEmpty(yazarFiltre))
                kitaplar = kitaplar.Where(k => k.Yazar.Contains(yazarFiltre));

            if (turFiltre.HasValue)
                kitaplar = kitaplar.Where(k => k.TurId == turFiltre);

            if (!string.IsNullOrEmpty(stokDurumu))
            {
                if (stokDurumu == "stokta")
                    kitaplar = kitaplar.Where(k => k.Stok > 0);
                else if (stokDurumu == "yok")
                    kitaplar = kitaplar.Where(k => k.Stok == 0);
            }

            ViewBag.Turler = new SelectList(db.Turler, "TurId", "Ad");
            return View(kitaplar.ToList());
        }

        // GET: Kitap/Create
        public ActionResult Create()
        {
            ViewBag.TurId = new SelectList(db.Turler, "TurId", "Ad");
            return View();
        }

        // POST: Kitap/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KitapId,Ad,Yazar,YayinYili,TurId,Stok")] Kitap kitap, HttpPostedFileBase Foto)
        {
            if (ModelState.IsValid)
            {
                if (Foto != null && Foto.ContentLength > 0)
                {
                    string dosyaAdi = Path.GetFileName(Foto.FileName);
                    string yuklemeYolu = Path.Combine(Server.MapPath("~/Uploads/KitapFoto"), dosyaAdi);
                    Foto.SaveAs(yuklemeYolu);
                    kitap.FotoUrl = "/Uploads/KitapFoto/" + dosyaAdi;
                }

                db.Kitaplar.Add(kitap);
                db.SaveChanges();

                Log.Information("Yeni kitap eklendi: {KitapAd} - {Yazar}", kitap.Ad, kitap.Yazar);

                return RedirectToAction("Index");
            }

            ViewBag.TurId = new SelectList(db.Turler, "TurId", "Ad", kitap.TurId);
            return View(kitap);
        }

        // GET: Kitap/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var kitap = db.Kitaplar.Find(id);
            if (kitap == null)
                return HttpNotFound();

            ViewBag.TurId = new SelectList(db.Turler, "TurId", "Ad", kitap.TurId);
            return View(kitap);
        }

        // POST: Kitap/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KitapId,Ad,Yazar,YayinYili,TurId,Stok,FotoUrl")] Kitap kitap, HttpPostedFileBase Foto)
        {
            if (ModelState.IsValid)
            {
                if (Foto != null && Foto.ContentLength > 0)
                {
                    string dosyaAdi = Path.GetFileName(Foto.FileName);
                    string yuklemeYolu = Path.Combine(Server.MapPath("~/Uploads/KitapFoto"), dosyaAdi);
                    Foto.SaveAs(yuklemeYolu);
                    kitap.FotoUrl = "/Uploads/KitapFoto/" + dosyaAdi;
                }

                db.Entry(kitap).State = EntityState.Modified;
                db.SaveChanges();

                Log.Information("Kitap güncellendi: {KitapAd} - {Yazar}", kitap.Ad, kitap.Yazar);

                return RedirectToAction("Index");
            }

            ViewBag.TurId = new SelectList(db.Turler, "TurId", "Ad", kitap.TurId);
            return View(kitap);
        }

        // GET: Kitap/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var kitap = db.Kitaplar.Include(k => k.Tur).FirstOrDefault(k => k.KitapId == id);
            if (kitap == null)
                return HttpNotFound();

            return View(kitap);
        }

        // POST: Kitap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var kitap = db.Kitaplar.Find(id);
            if (kitap != null)
            {
                db.Kitaplar.Remove(kitap);
                db.SaveChanges();

                Log.Information("Kitap silindi: {KitapAd} - {Yazar}", kitap.Ad, kitap.Yazar);
            }

            return RedirectToAction("Index");
        }

        // GET: Kitap/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var kitap = db.Kitaplar.Include(k => k.Tur).FirstOrDefault(k => k.KitapId == id);
            if (kitap == null)
                return HttpNotFound();

            Log.Information("Kitap detay görüntülendi: {KitapAd} - {Yazar}", kitap.Ad, kitap.Yazar);

            return View(kitap);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}
