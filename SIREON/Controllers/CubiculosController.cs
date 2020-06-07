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
    public class CubiculosController : Controller
    {
        private SIREONEntities db = new SIREONEntities();

        // GET: Cubiculos
        public ActionResult Index()
        {
            return View(db.Cubiculos.ToList());
        }

        // GET: Cubiculos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cubiculo cubiculo = db.Cubiculos.Find(id);
            if (cubiculo == null)
            {
                return HttpNotFound();
            }
            return View(cubiculo);
        }

        // GET: Cubiculos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cubiculos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Cubiculo,Descripcion,Capacidad")] Cubiculo cubiculo)
        {
            if (ModelState.IsValid)
            {
                db.Cubiculos.Add(cubiculo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cubiculo);
        }

        // GET: Cubiculos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cubiculo cubiculo = db.Cubiculos.Find(id);
            if (cubiculo == null)
            {
                return HttpNotFound();
            }
            return View(cubiculo);
        }

        // POST: Cubiculos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Cubiculo,Descripcion,Capacidad")] Cubiculo cubiculo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cubiculo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cubiculo);
        }

        // GET: Cubiculos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cubiculo cubiculo = db.Cubiculos.Find(id);
            if (cubiculo == null)
            {
                return HttpNotFound();
            }
            return View(cubiculo);
        }

        // POST: Cubiculos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cubiculo cubiculo = db.Cubiculos.Find(id);
            db.Cubiculos.Remove(cubiculo);
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
