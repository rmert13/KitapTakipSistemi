using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using KitapTakipSistemi.DAL;

namespace KitapTakipSistemi.Controllers
{
    public class TurController : Controller
    {
        private KitapContext db = new KitapContext();

        // GET: Tur
        public ActionResult Index()
        {
            var turler = db.Turler.ToList();
            return View(turler);
        }

        // GET: Tur/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tur/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tur tur)
        {
            if (ModelState.IsValid)
            {
                db.Turler.Add(tur);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tur);
        }

        // GET: Tur/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var tur = db.Turler.Find(id);
            if (tur == null)
                return HttpNotFound();

            return View(tur);
        }

        // POST: Tur/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tur tur)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tur).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tur);
        }

        // GET: Tur/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var tur = db.Turler.Find(id);
            if (tur == null)
                return HttpNotFound();

            return View(tur);
        }

        // POST: Tur/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var tur = db.Turler.Find(id);
            db.Turler.Remove(tur);
            db.SaveChanges();
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
