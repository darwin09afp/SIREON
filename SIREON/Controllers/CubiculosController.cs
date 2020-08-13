using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EO.Internal;
using Microsoft.AspNet.Identity;
using SIREON;
using SIREON.Models;

namespace SIREON.Controllers
{
    public class CubiculosController : Controller
    {
        private SIREONEntitiess db = new SIREONEntitiess();
        private UniversidadEntities db2 = new UniversidadEntities();

        // GET: Cubiculos
        public ActionResult Index()
        {
            return View(db.Cubiculos.ToList());
        }

        

        public ActionResult Disponibilidad(TimeSpan HEntrada)
        {
            var Fechaa = DateTime.Now;
            var Fecha = Fechaa.Date;
            var hora = Fechaa.TimeOfDay.Hours;
            //TimeSpan HEntrada = new TimeSpan(08, 0, 0);
            CustomModel2cs mymodel = new CustomModel2cs();

            var nodisp = db.Cubiculos.Where(x => x.Disponibilidads.Where(p => p.Fecha == Fecha && p.HoraInicial == HEntrada).Any());

            mymodel.cubiculos = db.Cubiculos.Except(nodisp).ToList();
            mymodel.disponibilidads = db.Cubiculos.Where(x => x.Disponibilidads.Where(p => p.Fecha == Fecha && p.HoraInicial == HEntrada).Any()).ToList();

            return View(mymodel);

        }

        public ActionResult Disp2()
        {

            var Fechaa = DateTime.Now;
            var Fecha = Fechaa.Date;
            var hora = Fechaa.TimeOfDay.Hours;
            TimeSpan HEntrada = new TimeSpan(10, 0, 0);
            var Busc = from table1 in db.Cubiculos
                       join table2 in db.Disponibilidads on table1.ID_Cubiculo equals table2.IdCubiculo
                       //where table2.Fecha == Fecha && table2.HoraInicial == HEntrada && table2.Estatus == "Reservado "
                       select new
                       {
                           Id = table1.ID_Cubiculo,
                           Desc = table1.Descripcion,
                           Cap = table1.Capacidad,
                           HoraInicial = table2.HoraInicial,
                           Est = table2.Estatus
                       };

            return Json(Busc.ToList(), JsonRequestBehavior.AllowGet);
            //return View(Busc.ToList());
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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Cubiculo,Descripcion,Capacidad,Estatus")] Cubiculo cubiculo)
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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Cubiculo,Descripcion,Capacidad,Estatus")] Cubiculo cubiculo)
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
