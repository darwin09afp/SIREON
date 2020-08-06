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
        public ActionResult Consultar(TimeSpan HEntrada)
        {
            string result2 = "";
            List<Cubiculo> cubs = new List<Cubiculo>();

            //Disponibilidad
            #region disponibilidad
            List<int> CubNoDisp = new List<int>();
            List<int> Cubs = new List<int>();
            var Fechaa = DateTime.Now;
            var Fecha = Fechaa.Date;

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
                        //sigue buscando en tabla disponibilidad
                    }

                }

            }
            var CubDisp = Cubs.Except(CubNoDisp).ToList();
            bool isEmpty = !CubDisp.Any();

            if (isEmpty == true)
            {
                result2 = "No hay cubículos disponibles para la hora seleccionada, aun así puede realizar su reservación y entrar a la lista de espera.";

            }
            else
            {
                
                result2 = "¡Cubículos disponibles!";
            }
            #endregion
            

            return Json(result2, JsonRequestBehavior.AllowGet);
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
            //var disp = db.Cubiculos.ToList()/*.Where(x => x.Estatus == "Libre")*/.FirstOrDefault().ID_Cubiculo;


            //Disponibilidad
            #region disponibilidad
            List<int> CubNoDisp = new List<int>();
            List<int> Cubs = new List<int>();
            var Fechaa = DateTime.Now;
            var Fecha = Fechaa.Date;

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
            bool isEmpty = !CubDisp.Any();



            #endregion




            //Agregar a tb reservaciones
            Reservacione model = new Reservacione();
            model.ID_Empleado = empleado;
            model.Fecha = SelFecha;

            if (isEmpty == true)
            {
                model.ID_Cubiculo = db.Cubiculos.ToList().FirstOrDefault().ID_Cubiculo;
                model.Estatus = "En espera";
                result = "Reservación realizada! Se colocó su reservación en la lista de espera para la hora seleccionada";
                
            }
            else
            {
                model.ID_Cubiculo = db.Cubiculos.ToList()
                            .Where(cubi => CubDisp.Contains(cubi.ID_Cubiculo))
                            .OrderBy(cubi => cubi.ID_Cubiculo)
                            .First().ID_Cubiculo;
                model.Estatus = "Activa";
                result = "Reservación realizada!";
            }

            model.FechaSolicitada = SelFecha;
            model.HSolicitada = SelHora;
            model.HEntrada = HEntrada;
            model.HSalida = HSalida;
            model.IdAspNetUsers = usuario;
            db.Reservaciones.Add(model);

            //Agregar a tb Disponibilidad
            Disponibilidad model2 = new Disponibilidad();
            model2.IdCubiculo = model.ID_Cubiculo;
            model2.HoraInicial = HEntrada;
            model2.HoraFinal = HSalida;
            model2.Fecha = SelFecha;
            model2.Estatus = "Reservado";
            db.Disponibilidads.Add(model2);
            
            

            //Agregar a Tb Reservaciones_usuarios
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
            db.SaveChanges();

            return Json(result, JsonRequestBehavior.AllowGet);
        }




        public ActionResult Disponibilidad2()
        {
            List<int> CubNoDisp = new List<int>();
            List<int> Cubs = new List<int>();
            var Fechaa = DateTime.Now;
            var Fecha = Fechaa.Date;
            TimeSpan HEntrada = new TimeSpan(10,0,0);

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
            //return View(CubNoDisp);
            return Json(CubDisp, JsonRequestBehavior.AllowGet);
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
