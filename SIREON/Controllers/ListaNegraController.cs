using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SIREON;

namespace SIREON.Controllers
{
    public class ListaNegraController : Controller
    {
        private SIREONEntitiess db = new SIREONEntitiess();

        // GET: ListaNegra
        public ActionResult Index()
        {
            var listaNegras = db.ListaNegras/*.Include(l => l.AspNetUser).Include(l => l.AspNetUser1)*/;
            return View(listaNegras.ToList());
        }



        [HttpPost]
        public ActionResult Agregar(string iduser, DateTime FechaSalida, string Motivo)
        {
            var fechaa = DateTime.Now;
            var fecha = fechaa.Date;

            ListaNegra listaNegra = new ListaNegra();
            listaNegra.IdAspNetUsers = iduser;
            listaNegra.Id_Empleado = User.Identity.GetUserId();
            listaNegra.FechaEntrada = fecha;
            listaNegra.FechaSalida = FechaSalida;
            listaNegra.Descripcion = Motivo;
            listaNegra.Estatus = "Bloqueado";
            db.ListaNegras.Add(listaNegra);
            db.SaveChanges();
            var res = "Listo";

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Perdonar(int? id)
        {
            var fechaa = DateTime.Now;
            var fecha = fechaa.Date;
            var user = User.Identity.GetUserId();

            var listaNegra = db.ListaNegras.Where(x => x.ID_ListaN == id).FirstOrDefault();
            listaNegra.Id_Empleado = user;
            listaNegra.FechaSalida = fecha;
            listaNegra.Estatus = "Perdonado";
            db.SaveChanges();


            return RedirectToAction("Index");
        }

        // GET: ListaNegra/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListaNegra listaNegra = db.ListaNegras.Find(id);
            if (listaNegra == null)
            {
                return HttpNotFound();
            }
            return View(listaNegra);
        }

        // GET: ListaNegra/Create
        public ActionResult Create()
        {
            ViewBag.IdAspNetUsers = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.Id_Empleado = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: ListaNegra/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_ListaN,Descripcion,IdAspNetUsers,FechaEntrada,FechaSalida,Estatus,Id_Empleado")] ListaNegra listaNegra)
        {
            if (ModelState.IsValid)
            {
                db.ListaNegras.Add(listaNegra);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdAspNetUsers = new SelectList(db.AspNetUsers, "Id", "Email", listaNegra.IdAspNetUsers);
            ViewBag.Id_Empleado = new SelectList(db.AspNetUsers, "Id", "Email", listaNegra.Id_Empleado);
            return View(listaNegra);
        }

        // GET: ListaNegra/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListaNegra listaNegra = db.ListaNegras.Find(id);
            if (listaNegra == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAspNetUsers = new SelectList(db.AspNetUsers, "Id", "Email", listaNegra.IdAspNetUsers);
            ViewBag.Id_Empleado = new SelectList(db.AspNetUsers, "Id", "Email", listaNegra.Id_Empleado);
            return View(listaNegra);
        }

        // POST: ListaNegra/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_ListaN,Descripcion,IdAspNetUsers,FechaEntrada,FechaSalida,Estatus,Id_Empleado")] ListaNegra listaNegra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(listaNegra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdAspNetUsers = new SelectList(db.AspNetUsers, "Id", "Email", listaNegra.IdAspNetUsers);
            ViewBag.Id_Empleado = new SelectList(db.AspNetUsers, "Id", "Email", listaNegra.Id_Empleado);
            return View(listaNegra);
        }

        // GET: ListaNegra/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListaNegra listaNegra = db.ListaNegras.Find(id);
            if (listaNegra == null)
            {
                return HttpNotFound();
            }
            return View(listaNegra);
        }

        // POST: ListaNegra/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ListaNegra listaNegra = db.ListaNegras.Find(id);
            db.ListaNegras.Remove(listaNegra);
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
