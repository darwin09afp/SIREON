using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SIREON;
using SIREON.Model.edmx;

namespace SIREON.Controllers
{
    public class EscuelasController : Controller
    {
        private UniversidadEntities db = new UniversidadEntities();

        // GET: Escuelas
        public ActionResult Index()
        {
            var escuelas = db.Escuelas.Include(e => e.Directore).Include(e => e.Facultade);
            return View(escuelas.ToList());
        }

        // GET: Escuelas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Escuela escuela = db.Escuelas.Find(id);
            if (escuela == null)
            {
                return HttpNotFound();
            }
            return View(escuela);
        }

        // GET: Escuelas/Create
        public ActionResult Create()
        {
            ViewBag.ID_Director = new SelectList(db.Directores, "ID_Director", "ID_Director");
            ViewBag.ID_Facultad = new SelectList(db.Facultades, "ID_Facultad", "Nombre");
            return View();
        }

        // POST: Escuelas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Escuela,ID_Director,ID_Facultad,Nombre")] Escuela escuela)
        {
            if (ModelState.IsValid)
            {
                db.Escuelas.Add(escuela);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Director = new SelectList(db.Directores, "ID_Director", "ID_Director", escuela.ID_Director);
            ViewBag.ID_Facultad = new SelectList(db.Facultades, "ID_Facultad", "Nombre", escuela.ID_Facultad);
            return View(escuela);
        }

        // GET: Escuelas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Escuela escuela = db.Escuelas.Find(id);
            if (escuela == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Director = new SelectList(db.Directores, "ID_Director", "ID_Director", escuela.ID_Director);
            ViewBag.ID_Facultad = new SelectList(db.Facultades, "ID_Facultad", "Nombre", escuela.ID_Facultad);
            return View(escuela);
        }

        // POST: Escuelas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Escuela,ID_Director,ID_Facultad,Nombre")] Escuela escuela)
        {
            if (ModelState.IsValid)
            {
                db.Entry(escuela).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Director = new SelectList(db.Directores, "ID_Director", "ID_Director", escuela.ID_Director);
            ViewBag.ID_Facultad = new SelectList(db.Facultades, "ID_Facultad", "Nombre", escuela.ID_Facultad);
            return View(escuela);
        }

        // GET: Escuelas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Escuela escuela = db.Escuelas.Find(id);
            if (escuela == null)
            {
                return HttpNotFound();
            }
            return View(escuela);
        }

        // POST: Escuelas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Escuela escuela = db.Escuelas.Find(id);
            db.Escuelas.Remove(escuela);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
