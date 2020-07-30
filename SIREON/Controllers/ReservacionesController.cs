using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SIREON;
using SIREON.Models;

namespace SIREON.Controllers
{
    public class ReservacionesController : Controller
    {
        private SIREONEntitiess db = new SIREONEntitiess();

        // GET: Reservaciones
        public ActionResult Index()
        {
            var reservaciones = db.Reservaciones.Include(r => r.Cubiculo).Include(r => r.AspNetUser).Include(r => r.AspNetUser1);
            return View(reservaciones.ToList());
        }


        // GET: Reservaciones
        public ActionResult Invitaciones()
        {
            var user = User.Identity.GetUserId();
            //var reservaciones = (db.Reservaciones.Include(r => r.Cubiculo).Include(r => r.AspNetUser).Include(r => r.AspNetUser1));
            var invitaciones = db.Reservaciones_Usuarios.Where(x => x.IdAspNetUser == user);
            return View(invitaciones.ToList());
        }


        // GET: Cola
        public ActionResult Cola()
        {
            var reservaciones = db.Reservaciones.Include(r => r.Cubiculo).Include(r => r.AspNetUser).Include(r => r.AspNetUser1);
            return View(reservaciones.ToList());
        }



        public ActionResult Create2()
        {
            List<Reservaciones_Usuarios> ListaInvitados = db.Reservaciones_Usuarios.ToList();
            return View(ListaInvitados);
        }

        public ActionResult ObtenerLista(int? id)
        {

            //var idres = db.Reservaciones.FirstOrDefault().ID_Reservacion;
            var Participantes = db.Reservaciones.GroupJoin(db.Reservaciones_Usuarios, ru => ru.ID_Reservacion,
                usr => usr.IdReservacion, (ru, usr) => new { ru, usr }).Where(x => x.ru.ID_Reservacion == 9);


            return View(db.Reservaciones_Usuarios.ToList());

        }





        // GET: Reservaciones1/Details/5
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

        [HttpPost]
        public ActionResult SaveOrder(TimeSpan HSolicitada, TimeSpan HEntrada, TimeSpan HSalida, Reservaciones_Usuarios[] reservaciones_Usuarios)
        {
            string result = "Error! Datos no completados!";
            List<Cubiculo> cubs = new List<Cubiculo>();
            var FechayHora = DateTime.Now;
            var SelFecha = FechayHora.Date;
            var SelHora = FechayHora.TimeOfDay;
            var usuario = User.Identity.GetUserId();
            var empleado = db.AspNetUsers.ToList().FirstOrDefault().Id;
            var disponibilidad = db.Cubiculos.ToList()/*.Where(x => x.Estatus == "Libre")*/.FirstOrDefault().ID_Cubiculo;
            Reservacione model = new Reservacione();
            model.ID_Empleado = empleado;
            model.Fecha = SelFecha;
            model.ID_Cubiculo = disponibilidad;
            model.FechaSolicitada = SelFecha;
            model.HSolicitada = HSolicitada;
            model.HEntrada = HEntrada;
            model.HSalida = HSalida;
            model.Estatus = "Activa";
            model.IdAspNetUsers = usuario;
            db.Reservaciones.Add(model);
            db.SaveChanges();

            foreach (var item in reservaciones_Usuarios)
            {
                Reservaciones_Usuarios Res = new Reservaciones_Usuarios();
                Res.IdAspNetUser = item.IdAspNetUser;
                Res.IdReservacion = model.ID_Reservacion;
                Res.NombreInvitado = item.NombreInvitado;
                Res.CedulaInvitado = item.CedulaInvitado;
                db.Reservaciones_Usuarios.Add(Res);
                db.SaveChanges();
            }

            result = "Exito! Datos guardados!";
            return Json(result, JsonRequestBehavior.AllowGet);
        }





        [HttpPost]
        public ActionResult SaveOrder2(string IdAspNetUsers, TimeSpan HSolicitada, TimeSpan HEntrada, TimeSpan HSalida, Reservaciones_Usuarios[] reservaciones_Usuarios)
        {
            string result = "Error! Datos no completados!";
            List<Cubiculo> cubs = new List<Cubiculo>();
            var FechayHora = DateTime.Now;
            var SelFecha = FechayHora.Date;
            var SelHora = FechayHora.TimeOfDay;
            var empleado = db.AspNetUsers.ToList().FirstOrDefault().Id;
            var disponibilidad = db.Cubiculos.ToList()/*.Where(x => x.Estatus == "Libre")*/.FirstOrDefault().ID_Cubiculo;
            Reservacione model = new Reservacione();
            model.ID_Empleado = empleado;
            model.Fecha = SelFecha;
            model.ID_Cubiculo = disponibilidad;
            model.FechaSolicitada = SelFecha;
            model.HSolicitada = HSolicitada;
            model.HEntrada = HEntrada;
            model.HSalida = HSalida;
            model.Estatus = "Activa";
            model.IdAspNetUsers = IdAspNetUsers;
            db.Reservaciones.Add(model);
            db.SaveChanges();

            foreach (var item in reservaciones_Usuarios)
            {
                Reservaciones_Usuarios Res = new Reservaciones_Usuarios();
                Res.IdAspNetUser = item.IdAspNetUser;
                Res.IdReservacion = model.ID_Reservacion;
                db.Reservaciones_Usuarios.Add(Res);
                db.SaveChanges();
            }

            result = "Exito! Datos guardados!";
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        // GET: Reservaciones1/Create
        public ActionResult Create()
        {
            ViewBag.ID_Cubiculo = new SelectList(db.Cubiculos, "ID_Cubiculo", "Descripcion");
            ViewBag.IdAspNetUsers = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.ID_Empleado = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }


        // POST: Reservaciones1/Create
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

        // GET: Reservaciones/CreateOp
        public ActionResult CreateOp()
        {
            ViewBag.ID_Cubiculo = new SelectList(db.Cubiculos, "ID_Cubiculo", "Descripcion");
            ViewBag.IdAspNetUsers = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.ID_Empleado = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }


        // POST: Reservaciones/CreateOp
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOp([Bind(Include = "ID_Reservacion,ID_Empleado,Fecha,ID_Cubiculo,FechaSolicitada,HSolicitada,HEntrada,HSalida,Estatus,IdAspNetUsers")] Reservacione reservacione)
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


        public JsonResult SetRes(Reservacione reservacione)
        {
            //insertar una reservacion
            return Json(reservacione.ID_Reservacion, JsonRequestBehavior.AllowGet);
        }



        // GET: Reservaciones1/Edit/5
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

        // POST: Reservaciones1/Edit/5
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

        // GET: Reservaciones1/Delete/5
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

        // POST: Reservaciones1/Delete/5
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
