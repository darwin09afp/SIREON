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
    public class CubiculosController : Controller
    {
        private SIREONEntitiess db = new SIREONEntitiess();
        private UniversidadEntities db2 = new UniversidadEntities();

        // GET: Cubiculos
        public ActionResult Index()
        {
            return View(db.Cubiculos.ToList());
        }

        

        public ActionResult Disponibilidad()
        {
            
            List<int> TotCubs = new List<int>();
            List<int> CubNoDisp = new List<int>();
            List<int> Cubs = new List<int>();
            
            var Fechaa = DateTime.Now;
            var Fecha = Fechaa.Date;
            var hora = Fechaa.TimeOfDay.Hours;
            TimeSpan HEntrada = new TimeSpan(10, 0, 0);


            var els = 0;
            string[] prueba2 = new string[els];
            foreach (var item in db.Cubiculos)
            {
                var idcub = item.ID_Cubiculo;
                Cubs.Add(idcub);

                string[] prueba = { Convert.ToString(item.ID_Cubiculo), Convert.ToString(item.Descripcion), Convert.ToString(item.Capacidad) };
                els = els + 1;

                prueba2[10] = prueba[0];


            }
            //    foreach (var item2 in db.Disponibilidads)
            //    {

            //        if (idcub == item2.IdCubiculo && item2.Fecha == Fecha && item2.HoraInicial == HEntrada && item2.Estatus != "Disponible")
            //        {
            //            CubNoDisp.Add(idcub);
            //        }
            //        else
            //        {
            //            //sigue buscando
            //        }

            //    }

            //}
            //var CubDisp = Cubs.Except(CubNoDisp).ToList();
            
            //var a = 0;
            //foreach (var item in CubDisp)
            //{  
            //    TotCubs.Add(CubDisp.ElementAt(a));
            //    a = a + 1;
            //}
            //a = 0;
            //foreach (var item in CubNoDisp)
            //{
            //    TotCubs.Add(CubNoDisp.ElementAt(a));
            //    a = a + 1;
            //}

            //var Disponib = db.Cubiculos
            //                .Where(cubi => CubDisp.Contains(cubi.ID_Cubiculo))
            //                .OrderBy(cubi => cubi.ID_Cubiculo)
            //                .ToList();


            //var Busc = from table1 in db.Cubiculos.AsEnumerable()
            //           join table2 in db.Disponibilidads.AsEnumerable() on table1.ID_Cubiculo equals table2.IdCubiculo
            //           where table2.Fecha == Fecha && table2.HoraInicial == HEntrada && table2.Estatus == "Reservado "
            //           select new
            //           {
            //               Id = table1.ID_Cubiculo,
            //               Desc = table1.Descripcion,
            //               Cap = table1.Capacidad,
            //               HE = table2.HoraInicial,
            //               Est = table2.Estatus
            //           };

            return Json(prueba2.ToList(), JsonRequestBehavior.AllowGet);
            //return View();
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
