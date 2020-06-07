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
    public class DirectoresController : Controller
    {
        private UniversidadEntities db = new UniversidadEntities();

        // GET: Directores
        public ActionResult Index()
        {
            var directores = db.Directores.Include(d => d.Entidad);
            return View(directores.ToList());
        }

        // GET: Directores/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Directore directore = db.Directores.Find(id);
            if (directore == null)
            {
                return HttpNotFound();
            }
            return View(directore);
        }

        // GET: Directores/Create
        public ActionResult Create()
        {
            ViewBag.ID_Entidad = new SelectList(db.Entidads, "ID_Entidad", "Nombre");
            return View();
        }

        // POST: Directores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Director,ID_Entidad")] Directore directore)
        {
            if (ModelState.IsValid)
            {
                db.Directores.Add(directore);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Entidad = new SelectList(db.Entidads, "ID_Entidad", "Nombre", directore.ID_Entidad);
            return View(directore);
        }

        // GET: Directores/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Directore directore = db.Directores.Find(id);
            if (directore == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Entidad = new SelectList(db.Entidads, "ID_Entidad", "Nombre", directore.ID_Entidad);
            return View(directore);
        }

        // POST: Directores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Director,ID_Entidad")] Directore directore)
        {
            if (ModelState.IsValid)
            {
                db.Entry(directore).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Entidad = new SelectList(db.Entidads, "ID_Entidad", "Nombre", directore.ID_Entidad);
            return View(directore);
        }

        // GET: Directores/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Directore directore = db.Directores.Find(id);
            if (directore == null)
            {
                return HttpNotFound();
            }
            return View(directore);
        }

        // POST: Directores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Directore directore = db.Directores.Find(id);
            db.Directores.Remove(directore);
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
