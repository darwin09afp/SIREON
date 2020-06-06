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
    public class FacultadesController : Controller
    {
        private UniversidadEntities db = new UniversidadEntities();

        // GET: Facultades
        public ActionResult Index()
        {
            return View(db.Facultades.ToList());
        }

        // GET: Facultades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facultade facultade = db.Facultades.Find(id);
            if (facultade == null)
            {
                return HttpNotFound();
            }
            return View(facultade);
        }

        // GET: Facultades/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Facultades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Facultad,Nombre")] Facultade facultade)
        {
            if (ModelState.IsValid)
            {
                db.Facultades.Add(facultade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(facultade);
        }

        // GET: Facultades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facultade facultade = db.Facultades.Find(id);
            if (facultade == null)
            {
                return HttpNotFound();
            }
            return View(facultade);
        }

        // POST: Facultades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Facultad,Nombre")] Facultade facultade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facultade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(facultade);
        }

        // GET: Facultades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facultade facultade = db.Facultades.Find(id);
            if (facultade == null)
            {
                return HttpNotFound();
            }
            return View(facultade);
        }

        // POST: Facultades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Facultade facultade = db.Facultades.Find(id);
            db.Facultades.Remove(facultade);
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
