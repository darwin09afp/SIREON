using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SIREON;
using System.Web.Security;
using System.Security.Authentication;

namespace SIREON.Controllers
{
    
    public class ReservacionePruebaController : Controller
    {
        private SIREONEntitiess db = new SIREONEntitiess();

        // GET: Reservaciones
        public ActionResult Index()
        {
            ViewBag.title = "Mi espacio | SIREON";
            var reservaciones = db.Reservaciones.Include(r => r.Cubiculo).Include(r => r.Sala).Include(r => r.IdAspNetUsers);
            return View(reservaciones.ToList());
        }

        // GET: Reservaciones
        public ActionResult Index2()
        {
            ViewBag.title = "Mi espacio | SIREON";
            var reservaciones = db.Reservaciones.Include(r => r.Cubiculo).Include(r => r.Sala).Include(r => r.IdAspNetUsers);
            return View(reservaciones.ToList());
        }

        // GET: Reservaciones
        public ActionResult Index3()
        {
            ViewBag.title = "Reservaciones | SIREON";
            var reservaciones = db.Reservaciones.Include(r => r.Cubiculo).Include(r => r.Sala).Include(r => r.IdAspNetUsers);
            return View(reservaciones.ToList());
        }

        // GET: Reservaciones
        public ActionResult Cola()
        {
            ViewBag.title = "En espera | SIREON";
            var reservaciones = db.Reservaciones.Include(r => r.Cubiculo).Include(r => r.Sala).Include(r => r.IdAspNetUsers);
            return View(reservaciones.ToList());
        }


        // GET: Reservaciones/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.title = "Mi espacio | SIREON";
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

        // GET: Reservaciones/Create
        public ActionResult Create()
        {
            ViewBag.title = "Mi espacio | SIREON";
            ViewBag.ID_Cubiculo = new SelectList(db.Cubiculos, "ID_Cubiculo", "Descripcion");
            ViewBag.ID_Sala = new SelectList(db.Salas, "ID_Sala", "ID_Sala");
            ViewBag.ID_Usuario = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Reservaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Reservacion,IdAspNetUsers,ID_Sala,ID_Empleado,Fecha,ID_Cubiculo,FechaSolicitada,HSolicitada,HEntrada,HSalida,Estatus")] Reservacione reservacione)
        {
            ViewBag.title = "Mi espacio | SIREON";
            if (ModelState.IsValid)
            {
                ViewBag.title = "Mi espacio | SIREON";
                db.Reservaciones.Add(reservacione);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Cubiculo = new SelectList(db.Cubiculos, "ID_Cubiculo", "Descripcion", reservacione.ID_Cubiculo);
            ViewBag.ID_Sala = new SelectList(db.Salas, "ID_Sala", "ID_Sala", reservacione.ID_Sala);
            ViewBag.ID_Usuario = new SelectList(db.AspNetUsers, "Id", "Email", reservacione.IdAspNetUsers);
            return View(reservacione);
        
        }




        // GET: Reservaciones/CreateOp
        public ActionResult CreateOp()
        {
            ViewBag.title = "Nueva reservación | SIREON";
            ViewBag.ID_Cubiculo = new SelectList(db.Cubiculos, "ID_Cubiculo", "Descripcion");
            ViewBag.ID_Sala = new SelectList(db.Salas, "ID_Sala", "ID_Sala");
            ViewBag.ID_Usuario = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Reservaciones/CreateOp
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOp([Bind(Include = "ID_Reservacion,IdAspNetUsers,ID_Sala,ID_Empleado,Fecha,ID_Cubiculo,FechaSolicitada,HSolicitada,HEntrada,HSalida,Estatus")] Reservacione reservacione)
        {
            ViewBag.title = "Nueva reservación | SIREON";
            if (ModelState.IsValid)
            {
                ViewBag.title = "Nueva reservación | SIREON";
                db.Reservaciones.Add(reservacione);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Cubiculo = new SelectList(db.Cubiculos, "ID_Cubiculo", "Descripcion", reservacione.ID_Cubiculo);
            ViewBag.ID_Sala = new SelectList(db.Salas, "ID_Sala", "ID_Sala", reservacione.ID_Sala);
            ViewBag.ID_Usuario = new SelectList(db.AspNetUsers, "Id", "Email", reservacione.IdAspNetUsers);
            return View(reservacione);

        }






        //CreateAjax
        // GET: Reservaciones/Createajax
        public ActionResult CreateAjax()
        {
            ViewBag.title = "Mi espacio | SIREON";
            ViewBag.ID_Cubiculo = new SelectList(db.Cubiculos, "ID_Cubiculo", "Descripcion");
            ViewBag.ID_Sala = new SelectList(db.Salas, "ID_Sala", "ID_Sala");
            ViewBag.ID_Usuario = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Reservaciones/Createajax
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAjax([Bind(Include = "ID_Sala")] Sala sala, [Bind(Include = "ID_RSalas,ID_Sala,ID_Usuario")] R_Salas_Usuarios r_Salas_Usuarios, [Bind(Include = "ID_Reservacion,ID_Usuario,ID_Sala,ID_Empleado,Fecha,ID_Cubiculo,FechaSolicitada,HSolicitada,HEntrada,HSalida,Estatus")] Reservacione reservacione)
        {
            ViewBag.title = "Mi espacio | SIREON";
            if (ModelState.IsValid)
            {
                db.Salas.Add(sala);
                db.SaveChanges();
                
            }
            reservacione.ID_Sala = db.Salas.Last().ID_Sala;

            if (ModelState.IsValid)
            {
                db.R_Salas_Usuarios.Add(r_Salas_Usuarios);
                db.SaveChanges();

            }

            if (ModelState.IsValid)
            {
                ViewBag.title = "Mi espacio | SIREON";
                db.Reservaciones.Add(reservacione);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            



            ViewBag.ID_Cubiculo = new SelectList(db.Cubiculos, "ID_Cubiculo", "Descripcion", reservacione.ID_Cubiculo);
            ViewBag.ID_Sala = new SelectList(db.Salas, "ID_Sala", "ID_Sala", reservacione.ID_Sala);
            ViewBag.ID_Usuario = new SelectList(db.AspNetUsers, "ID_Usuario", "Usuario1", reservacione.IdAspNetUsers);
            return View(reservacione);

        }












        // GET: Reservaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.title = "Mi espacio | SIREON";
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
            ViewBag.ID_Sala = new SelectList(db.Salas, "ID_Sala", "ID_Sala", reservacione.ID_Sala);
            ViewBag.ID_Usuario = new SelectList(db.AspNetUsers, "ID_Usuario", "Usuario1", reservacione.IdAspNetUsers);
            return View(reservacione);
        }

        // POST: Reservaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Reservacion,IdAspNetUsers,ID_Sala,ID_Empleado,Fecha,ID_Cubiculo,FechaSolicitada,HSolicitada,HEntrada,HSalida,Estatus")] Reservacione reservacione)
        {
            ViewBag.title = "Mi espacio | SIREON";
            if (ModelState.IsValid)
            {
                db.Entry(reservacione).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Cubiculo = new SelectList(db.Cubiculos, "ID_Cubiculo", "Descripcion", reservacione.ID_Cubiculo);
            ViewBag.ID_Sala = new SelectList(db.Salas, "ID_Sala", "ID_Sala", reservacione.ID_Sala);
            ViewBag.ID_Usuario = new SelectList(db.AspNetUsers, "ID_Usuario", "Usuario1", reservacione.IdAspNetUsers);
            return View(reservacione);
        }

        // GET: Reservaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.title = "Mi espacio | SIREON";
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

        // POST: Reservaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.title = "Mi espacio | SIREON";
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
