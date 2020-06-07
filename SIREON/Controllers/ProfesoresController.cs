using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SIREON;


namespace SIREON.Controllers
{
    public class ProfesoresController : Controller
    {
        private UniversidadEntities db = new UniversidadEntities();

        // GET: Profesores
        public ActionResult Index()
        {
            var profesores = db.Profesores.Include(p => p.Entidad).Include(p => p.Escuela);
            return View(profesores.ToList());
        }

        // GET: Profesores/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profesore profesore = db.Profesores.Find(id);
            if (profesore == null)
            {
                return HttpNotFound();
            }
            return View(profesore);
        }

        // GET: Profesores/Create
        public ActionResult Create()
        {
            ViewBag.ID_Entidad = new SelectList(db.Entidads, "ID_Entidad", "Nombre");
            ViewBag.ID_Escuela = new SelectList(db.Escuelas, "ID_Escuela", "ID_Director");
            return View();
        }

        // POST: Profesores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Profesor,ID_Entidad,ID_Escuela")] Profesore profesore)
        {
            if (ModelState.IsValid)
            {
                db.Profesores.Add(profesore);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Entidad = new SelectList(db.Entidads, "ID_Entidad", "Nombre", profesore.ID_Entidad);
            ViewBag.ID_Escuela = new SelectList(db.Escuelas, "ID_Escuela", "ID_Director", profesore.ID_Escuela);
            return View(profesore);
        }

        // GET: Profesores/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profesore profesore = db.Profesores.Find(id);
            if (profesore == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Entidad = new SelectList(db.Entidads, "ID_Entidad", "Nombre", profesore.ID_Entidad);
            ViewBag.ID_Escuela = new SelectList(db.Escuelas, "ID_Escuela", "ID_Director", profesore.ID_Escuela);
            return View(profesore);
        }

        // POST: Profesores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Profesor,ID_Entidad,ID_Escuela")] Profesore profesore)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profesore).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Entidad = new SelectList(db.Entidads, "ID_Entidad", "Nombre", profesore.ID_Entidad);
            ViewBag.ID_Escuela = new SelectList(db.Escuelas, "ID_Escuela", "ID_Director", profesore.ID_Escuela);
            return View(profesore);
        }

        // GET: Profesores/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profesore profesore = db.Profesores.Find(id);
            if (profesore == null)
            {
                return HttpNotFound();
            }
            return View(profesore);
        }

        // POST: Profesores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Profesore profesore = db.Profesores.Find(id);
            db.Profesores.Remove(profesore);
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
