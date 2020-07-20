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
    public class StatsController : Controller
    {
        private SIREONEntitiess db = new SIREONEntitiess();

        // GET: Stats
        public ActionResult Index()
        {
            var reservaciones = db.Reservaciones.Include(r => r.Cubiculo).Include(r => r.AspNetUser).Include(r => r.AspNetUser1);
            return View(reservaciones.ToList());
        }

        // GET: Stats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservacione reservacione = db.Reservaciones.Find(id);
            if (reservacione == null)
            {
                return HttpNotFound();
            }
            return View(reservacione);
        }

        // GET: Stats/Create
        public ActionResult Create()
        {
            ViewBag.ID_Cubiculo = new SelectList(db.Cubiculos, "ID_Cubiculo", "Descripcion");
            ViewBag.IdAspNetUsers = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.ID_Empleado = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Stats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Reservacion,ID_Empleado,Fecha,ID_Cubiculo,FechaSolicitada,HSolicitada,HEntrada,HSalida,Estatus,IdAspNetUsers")] Reservacione reservacione)
        {
            if (ModelState.IsValid)
            {
                db.Reservaciones.Add(reservacione);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Cubiculo = new SelectList(db.Cubiculos, "ID_Cubiculo", "Descripcion", reservacione.ID_Cubiculo);
            ViewBag.IdAspNetUsers = new SelectList(db.AspNetUsers, "Id", "Email", reservacione.IdAspNetUsers);
            ViewBag.ID_Empleado = new SelectList(db.AspNetUsers, "Id", "Email", reservacione.ID_Empleado);
            return View(reservacione);
        }

        // GET: Stats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservacione reservacione = db.Reservaciones.Find(id);
            if (reservacione == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Cubiculo = new SelectList(db.Cubiculos, "ID_Cubiculo", "Descripcion", reservacione.ID_Cubiculo);
            ViewBag.IdAspNetUsers = new SelectList(db.AspNetUsers, "Id", "Email", reservacione.IdAspNetUsers);
            ViewBag.ID_Empleado = new SelectList(db.AspNetUsers, "Id", "Email", reservacione.ID_Empleado);
            return View(reservacione);
        }

        // POST: Stats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Reservacion,ID_Empleado,Fecha,ID_Cubiculo,FechaSolicitada,HSolicitada,HEntrada,HSalida,Estatus,IdAspNetUsers")] Reservacione reservacione)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservacione).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Cubiculo = new SelectList(db.Cubiculos, "ID_Cubiculo", "Descripcion", reservacione.ID_Cubiculo);
            ViewBag.IdAspNetUsers = new SelectList(db.AspNetUsers, "Id", "Email", reservacione.IdAspNetUsers);
            ViewBag.ID_Empleado = new SelectList(db.AspNetUsers, "Id", "Email", reservacione.ID_Empleado);
            return View(reservacione);
        }

        // GET: Stats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservacione reservacione = db.Reservaciones.Find(id);
            if (reservacione == null)
            {
                return HttpNotFound();
            }
            return View(reservacione);
        }

        // POST: Stats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservacione reservacione = db.Reservaciones.Find(id);
            db.Reservaciones.Remove(reservacione);
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
