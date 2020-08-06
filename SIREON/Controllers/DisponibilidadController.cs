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
    public class DisponibilidadController : Controller
    {
        private SIREONEntitiess db = new SIREONEntitiess();

        // GET: Disponibilidad
        public ActionResult Index()
        {
            var disponibilidads = db.Disponibilidads.Include(d => d.Cubiculo);
            return View(disponibilidads.ToList());
        }


        // GET: Disponibilidad
        public ActionResult Disponibilidad2()
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



            var Disponib = db.Cubiculos.ToList()
                            .Where(cubi => CubDisp.Contains(cubi.ID_Cubiculo))
                            .OrderBy(cubi => cubi.ID_Cubiculo)
                            .First().ID_Cubiculo;



            //return View(CubNoDisp);
            return Json(CubDisp, JsonRequestBehavior.AllowGet);
        }





        public ActionResult Disponibilidad(TimeSpan? HoraInicial, TimeSpan? HoraFinal , DateTime? Fecha)
        {
           

            List<Cubiculo> cubs = new List<Cubiculo>();
            List<Disponibilidad> disp  = new List<Disponibilidad>();
            List<int> CubNoDisp = new List<int>();
            
            
            foreach (var item in cubs)
            {
                var idcub = item.ID_Cubiculo;
                foreach (var item2 in disp)
                {
                    var ConsDisp = idcub.CompareTo(item2.IdCubiculo);
                    if (Convert.ToBoolean(ConsDisp) == true && item2.Fecha == Fecha && item2.HoraInicial == HoraInicial && item2.Estatus != "Disponible")
                    {
                        CubNoDisp.Add(idcub);
                    }
                    else
                    {
                        //sigue buscando
                    }


                }


            }
            
            return View(CubNoDisp);
        }




        // GET: Disponibilidad/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disponibilidad disponibilidad = db.Disponibilidads.Find(id);
            if (disponibilidad == null)
            {
                return HttpNotFound();
            }
            return View(disponibilidad);
        }

        // GET: Disponibilidad/Create
        public ActionResult Create()
        {
            ViewBag.IdCubiculo = new SelectList(db.Cubiculos, "ID_Cubiculo", "Descripcion");
            return View();
        }

        // POST: Disponibilidad/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdCubiculo,HoraInicial,HoraFinal,Fecha,Estatus")] Disponibilidad disponibilidad)
        {
            if (ModelState.IsValid)
            {
                db.Disponibilidads.Add(disponibilidad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCubiculo = new SelectList(db.Cubiculos, "ID_Cubiculo", "Descripcion", disponibilidad.IdCubiculo);
            return View(disponibilidad);
        }

        // GET: Disponibilidad/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disponibilidad disponibilidad = db.Disponibilidads.Find(id);
            if (disponibilidad == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCubiculo = new SelectList(db.Cubiculos, "ID_Cubiculo", "Descripcion", disponibilidad.IdCubiculo);
            return View(disponibilidad);
        }

        // POST: Disponibilidad/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdCubiculo,HoraInicial,HoraFinal,Fecha,Estatus")] Disponibilidad disponibilidad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(disponibilidad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCubiculo = new SelectList(db.Cubiculos, "ID_Cubiculo", "Descripcion", disponibilidad.IdCubiculo);
            return View(disponibilidad);
        }

        // GET: Disponibilidad/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disponibilidad disponibilidad = db.Disponibilidads.Find(id);
            if (disponibilidad == null)
            {
                return HttpNotFound();
            }
            return View(disponibilidad);
        }

        // POST: Disponibilidad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Disponibilidad disponibilidad = db.Disponibilidads.Find(id);
            db.Disponibilidads.Remove(disponibilidad);
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
