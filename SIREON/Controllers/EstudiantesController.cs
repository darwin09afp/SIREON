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
    public class EstudiantesController : Controller
    {
        private UniversidadEntities db = new UniversidadEntities();

        // GET: Estudiantes
        public ActionResult Index()
        {
            var estudiantes = db.Estudiantes.Include(e => e.Carrera).Include(e => e.Entidad);
            return View(estudiantes.ToList());
        }

        // GET: Estudiantes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudiante estudiante = db.Estudiantes.Find(id);
            if (estudiante == null)
            {
                return HttpNotFound();
            }
            return View(estudiante);
        }

        // GET: Estudiantes/Create
        public ActionResult Create()
        {
            ViewBag.ID_Carrera = new SelectList(db.Carreras, "ID_Carrera", "Nombre");
            ViewBag.ID_Entidad = new SelectList(db.Entidads, "ID_Entidad", "Nombre");
            return View();
        }

        // POST: Estudiantes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Estudiante,ID_Entidad,ID_Carrera")] Estudiante estudiante)
        {
            if (ModelState.IsValid)
            {
                db.Estudiantes.Add(estudiante);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Carrera = new SelectList(db.Carreras, "ID_Carrera", "Nombre", estudiante.ID_Carrera);
            ViewBag.ID_Entidad = new SelectList(db.Entidads, "ID_Entidad", "Nombre", estudiante.ID_Entidad);
            return View(estudiante);
        }

        // GET: Estudiantes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudiante estudiante = db.Estudiantes.Find(id);
            if (estudiante == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Carrera = new SelectList(db.Carreras, "ID_Carrera", "Nombre", estudiante.ID_Carrera);
            ViewBag.ID_Entidad = new SelectList(db.Entidads, "ID_Entidad", "Nombre", estudiante.ID_Entidad);
            return View(estudiante);
        }

        // POST: Estudiantes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Estudiante,ID_Entidad,ID_Carrera")] Estudiante estudiante)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estudiante).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Carrera = new SelectList(db.Carreras, "ID_Carrera", "Nombre", estudiante.ID_Carrera);
            ViewBag.ID_Entidad = new SelectList(db.Entidads, "ID_Entidad", "Nombre", estudiante.ID_Entidad);
            return View(estudiante);
        }

        // GET: Estudiantes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudiante estudiante = db.Estudiantes.Find(id);
            if (estudiante == null)
            {
                return HttpNotFound();
            }
            return View(estudiante);
        }

        // POST: Estudiantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Estudiante estudiante = db.Estudiantes.Find(id);
            db.Estudiantes.Remove(estudiante);
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
