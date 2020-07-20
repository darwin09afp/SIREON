using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;
using SIREON;

namespace SIREON.Controllers
{
    public class Reservaciones_UsuariosController : Controller
    {
        private SIREONEntitiess db = new SIREONEntitiess();

        // GET: Reservaciones_Usuarios
        public ActionResult Index()
        {
            var reservaciones_Usuarios = db.Reservaciones_Usuarios.Include(r => r.AspNetUser).Include(r => r.Reservacione);
            return View(reservaciones_Usuarios.ToList());
        }

        // GET: Reservaciones_Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservaciones_Usuarios reservaciones_Usuarios = db.Reservaciones_Usuarios.Find(id);
            if (reservaciones_Usuarios == null)
            {
                return HttpNotFound();
            }
            return View(reservaciones_Usuarios);
        }

        // GET: Reservaciones_Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.IdAspNetUser = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.IdReservacion = new SelectList(db.Reservaciones, "ID_Reservacion", "ID_Empleado");
            return View();
        }

        // POST: Reservaciones_Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdAspNetUser,IdReservacion")] Reservaciones_Usuarios reservaciones_Usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Reservaciones_Usuarios.Add(reservaciones_Usuarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdAspNetUser = new SelectList(db.AspNetUsers, "Id", "Email", reservaciones_Usuarios.IdAspNetUser);
            ViewBag.IdReservacion = new SelectList(db.Reservaciones, "ID_Reservacion", "ID_Empleado", reservaciones_Usuarios.IdReservacion);
            return View(reservaciones_Usuarios);
        }




        public ActionResult Create2()
        {
            List<Reservacione> ListaReservaciones = db.Reservaciones.ToList();
            return View(ListaReservaciones);
        }


        


        // GET: Reservaciones_Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservaciones_Usuarios reservaciones_Usuarios = db.Reservaciones_Usuarios.Find(id);
            if (reservaciones_Usuarios == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAspNetUser = new SelectList(db.AspNetUsers, "Id", "Email", reservaciones_Usuarios.IdAspNetUser);
            ViewBag.IdReservacion = new SelectList(db.Reservaciones, "ID_Reservacion", "ID_Empleado", reservaciones_Usuarios.IdReservacion);
            return View(reservaciones_Usuarios);
        }

        // POST: Reservaciones_Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdAspNetUser,IdReservacion")] Reservaciones_Usuarios reservaciones_Usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservaciones_Usuarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdAspNetUser = new SelectList(db.AspNetUsers, "Id", "Email", reservaciones_Usuarios.IdAspNetUser);
            ViewBag.IdReservacion = new SelectList(db.Reservaciones, "ID_Reservacion", "ID_Empleado", reservaciones_Usuarios.IdReservacion);
            return View(reservaciones_Usuarios);
        }

        // GET: Reservaciones_Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservaciones_Usuarios reservaciones_Usuarios = db.Reservaciones_Usuarios.Find(id);
            if (reservaciones_Usuarios == null)
            {
                return HttpNotFound();
            }
            return View(reservaciones_Usuarios);
        }

        // POST: Reservaciones_Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservaciones_Usuarios reservaciones_Usuarios = db.Reservaciones_Usuarios.Find(id);
            db.Reservaciones_Usuarios.Remove(reservaciones_Usuarios);
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
