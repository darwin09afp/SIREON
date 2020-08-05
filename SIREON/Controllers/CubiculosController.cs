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
        private SIREONEntitiess db = new SIREONEntitiess();

        // GET: Cubiculos
        public ActionResult Index()
        {
            return View(db.Cubiculos.ToList());
        }

        public ActionResult Disponibilidad()
        {
            List<int> CubNoDisp = new List<int>();
            List<int> Cubs = new List<int>();
            var Fechaa = DateTime.Now;
            var Fecha = Fechaa.Date;
            TimeSpan HEntrada = new TimeSpan(10, 0, 0);

            foreach (var item in db.Cubiculos)
            {
                var idcub = item.ID_Cubiculo;
                Cubs.Add(idcub);
                foreach (var item2 in db.Disponibilidads)
                {

                    if (idcub == item2.IdCubiculo && item2.Fecha == Fecha && item2.HoraInicial == HEntrada && item2.Estatus != "Disponible")
                    {
                        CubNoDisp.Add(idcub);
                    }
                    else
                    {
                        //sigue buscando
                    }

                }

            }
            var CubDisp = Cubs.Except(CubNoDisp).ToList();
            

            var Disponib = db.Cubiculos
                            .Where(cubi => CubDisp.Contains(cubi.ID_Cubiculo))
                            .OrderBy(cubi => cubi.ID_Cubiculo)
                            .ToList();

            return View(Disponib);
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
