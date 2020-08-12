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
using Newtonsoft.Json;
using SIREON;
using SIREON.Models;

namespace SIREON.Controllers
{
    public class ReservacionesController : Controller
    {
        private SIREONEntitiess db = new SIREONEntitiess();
        private UniversidadEntities db2 = new UniversidadEntities();

        // GET: Reservaciones
        public ActionResult Index()
        {

            if (User.IsInRole("Operador"))
            {

                var fecha = DateTime.Now;
                var fechaa = fecha.Date;
                var hora = fecha.TimeOfDay;
                var reservaciones = db.Reservaciones
                    .Where(x => x.Estatus != "Completada" || x.Estatus != "Rechazada" || x.Estatus != "Cancelada" || x.Estatus != "En Espera")
                    .Where(x => x.HSalida >= hora)
                    .Where(x => x.Fecha == fechaa).ToList();
                
                return View(reservaciones.ToList());

            }
            else
            {
                var fecha = DateTime.Now;
                var fechaa = fecha.Date;
                var hora = fecha.TimeOfDay;
                var user = User.Identity.GetUserId();
                var reservaciones = db.Reservaciones
                    .Where(x => x.IdAspNetUsers == user)
                    .Where(x => x.Estatus == "Activa" || x.Estatus == "En espera")
                    .Where(x => x.HEntrada >= hora)
                    .Where(x => x.Fecha == fechaa).ToList();

                return View(reservaciones.ToList());
                
            }




        }


        // GET: Reservaciones
        public ActionResult Invitaciones2()
        {
           
            var fecha = DateTime.Now;
            var fechaa = fecha.Date;
            var hora = fecha.TimeOfDay;
            var user = User.Identity.GetUserId();

            var Salas = db.Reservaciones_Usuarios.Where(x => x.IdAspNetUser == user).ToList();
            
            var Inv = db.Reservaciones
                .Where(x => x.Reservaciones_Usuarios.All(p => p.IdAspNetUser == user))
                .Where(x => x.Estatus == "Activa" || x.Estatus == "En espera")
                .Where(x => x.HEntrada >= hora)
                .Where(x => x.Fecha == fechaa).ToList();


            return View(Inv);

        }


        // GET: Cola
        public ActionResult Cola()
        {
            var fecha = DateTime.Now;
            var fechaa = fecha.Date;
            var hora = fecha.TimeOfDay;
            var reservaciones = db.Reservaciones
                .Where(x => x.Estatus == "En Espera")
                //.Where(x => x.HEntrada >= hora)
                /*.Where(x => x.Fecha == fechaa)*/.ToList();
            
            return View(reservaciones.ToList());
            
        }

        // GET: historial
        public ActionResult Historial()
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


        public JsonResult Usuario()
        {
            //Metodo solo para presentar el nombre en la reservacion la matricula del estudiante loggeado 

            var usr = User.Identity.GetUserId();
            var usrname = User.Identity.GetUserName();
            var BuscarEmail = (from table1 in db.AspNetUsers
                               where table1.Id == usr
                               select new
                               {
                                   email = table1.Email
                               }).AsEnumerable();


            var query = from table2 in db2.Entidads
                        where table2.CorreoInst == usrname
                        select new
                        {
                            Nombr = table2.Nombre,
                            Apellid = table2.Apellido,
                            Matricul = table2.CodigoInst
                        };




            //var json = JsonConvert.SerializeObject(query);


            return Json(query, JsonRequestBehavior.AllowGet);

        }



        public JsonResult Usuario2(string IdAspNetUsers2)
        {
            //Metodo solo para presentar el nombre en la reservacion la matricula del estudiante loggeado 


            var BuscarNombre = db2.Entidads.Where(x => x.CodigoInst == IdAspNetUsers2).FirstOrDefault().Nombre;
            var BuscarApellido = db2.Entidads.Where(x => x.CodigoInst == IdAspNetUsers2).FirstOrDefault().Apellido;

            var result3 = new string[] { BuscarNombre, BuscarApellido };
            //string str1 = strArr[0], str2 = strArr[1];

            //var json = JsonConvert.SerializeObject(query);


            return Json(result3, JsonRequestBehavior.AllowGet);

        }

        public JsonResult Usuario3(string IdAspNetUser)
        {
            //Metodo solo para presentar el nombre en la reservacion la matricula del estudiante loggeado 
            var BuscarNombre = db2.Entidads.Where(x => x.CodigoInst == IdAspNetUser).FirstOrDefault().Nombre;
            if (BuscarNombre == null)
            {
                var result3 = "";
                return Json(result3, JsonRequestBehavior.AllowGet);

            }
            else
            {
                var BuscarApellido = db2.Entidads.Where(x => x.CodigoInst == IdAspNetUser).FirstOrDefault().Apellido;
                var result3 = new string[] { BuscarNombre, BuscarApellido };
                return Json(result3, JsonRequestBehavior.AllowGet);

            }

            //string str1 = strArr[0], str2 = strArr[1];

            //var json = JsonConvert.SerializeObject(query);




        }






        [HttpPost]
        public ActionResult SaveOrder(string IdAspNetUsers2, TimeSpan HEntrada, TimeSpan HSalida, Reservaciones_Usuarios[] reservaciones_Usuarios)
        {
            string result = "";
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
            if (User.IsInRole("Operador"))
            {
                model.ID_Empleado = usuario;
            }
            else
            {
                model.ID_Empleado = empleado;
            }
            model.Fecha = SelFecha;
            if (isEmpty == true)
            {
                model.ID_Cubiculo = db.Cubiculos.ToList().FirstOrDefault().ID_Cubiculo;
                model.Estatus = "En espera";
                result = "Se colocó su reservación en la lista de espera para la hora seleccionada";


                //Agregar a tb Disponibilidad
                Disponibilidad model2 = new Disponibilidad();
                model2.IdCubiculo = model.ID_Cubiculo;
                model2.HoraInicial = HEntrada;
                model2.HoraFinal = HSalida;
                model2.Fecha = SelFecha;
                model2.Estatus = "En Espera";
                db.Disponibilidads.Add(model2);

            }
            else
            {
                model.ID_Cubiculo = db.Cubiculos.ToList()
                            .Where(cubi => CubDisp.Contains(cubi.ID_Cubiculo))
                            .OrderBy(cubi => cubi.ID_Cubiculo)
                            .First().ID_Cubiculo;
                model.Estatus = "Activa";

                //Agregar a tb Disponibilidad
                Disponibilidad model2 = new Disponibilidad();
                model2.IdCubiculo = model.ID_Cubiculo;
                model2.HoraInicial = HEntrada;
                model2.HoraFinal = HSalida;
                model2.Fecha = SelFecha;
                model2.Estatus = "Reservado";
                db.Disponibilidads.Add(model2);


                result = "";
            }

            model.FechaSolicitada = SelFecha;
            model.HSolicitada = SelHora;
            model.HEntrada = HEntrada;
            model.HSalida = HSalida;
            if (User.IsInRole("Operador"))
            {
                //Metodo solo para enviar el nombre en la reservacion la matricula del estudiante ingresado 

                var UsrMat = IdAspNetUsers2;
                var BuscarEmail = db2.Entidads.Where(x => x.CodigoInst == UsrMat).FirstOrDefault().CorreoInst;
                var query = db.AspNetUsers.Where(x => x.Email == BuscarEmail).FirstOrDefault().Id;
                model.IdAspNetUsers = query;

            }
            else
            {
                model.IdAspNetUsers = usuario;
            }           
            db.Reservaciones.Add(model);
        

            //Agregar a Tb Reservaciones_usuarios
            foreach (var item in reservaciones_Usuarios)
            {
                Reservaciones_Usuarios Res = new Reservaciones_Usuarios();
                if (item.CedulaInvitado == null)
                {
                    var BuscarEmail = db2.Entidads.Where(x => x.CodigoInst == item.IdAspNetUser).FirstOrDefault().CorreoInst;
                    var query2 = db.AspNetUsers.Where(x => x.Email == BuscarEmail).FirstOrDefault().Id;

                    Res.IdAspNetUser = query2;
                    Res.IdReservacion = model.ID_Reservacion;
                    Res.NombreInvitado = item.NombreInvitado;
                    Res.CedulaInvitado = item.CedulaInvitado;
                    db.Reservaciones_Usuarios.Add(Res);
                    db.SaveChanges();
                }
                else
                {
                    Res.IdAspNetUser = item.IdAspNetUser;
                    Res.IdReservacion = model.ID_Reservacion;
                    Res.NombreInvitado = item.NombreInvitado;
                    Res.CedulaInvitado = item.CedulaInvitado;
                    db.Reservaciones_Usuarios.Add(Res);
                    db.SaveChanges();
                }

            }
            db.SaveChanges();

            return Json(result, JsonRequestBehavior.AllowGet);
        }



        #region 
        //public ActionResult Disponibilidad2()
        //{
        //    List<int> CubNoDisp = new List<int>();
        //    List<int> Cubs = new List<int>();
        //    var Fechaa = DateTime.Now;
        //    var Fecha = Fechaa.Date;
        //    TimeSpan HEntrada = new TimeSpan(10,0,0);

        //    foreach (var item in db.Cubiculos)
        //    {
        //        var idcub = item.ID_Cubiculo;
        //        Cubs.Add(idcub);
        //        foreach (var item2 in db.Disponibilidads)
        //        {

        //            if (idcub == item2.IdCubiculo && item2.Fecha == Fecha && item2.HoraInicial == HEntrada && item2.Estatus != "Disponible")
        //            {
        //                CubNoDisp.Add(idcub);
        //            }
        //            else 
        //            {
        //                //sigue buscando
        //            }

        //        }

        //    }
        //    var CubDisp = Cubs.Except(CubNoDisp).ToList();
        //    //return View(CubNoDisp);
        //    return Json(CubDisp, JsonRequestBehavior.AllowGet);
        //}




        //public ActionResult ActStatus()
        //{
        //    var a = 10;
        //    List<int> Reservaciones = new List<int>();
        //    List<int> Cubs = new List<int>();
        //    var Fechaa = DateTime.Now;
        //    var Fecha = Fechaa.Date;
        //    TimeSpan HEntrada = new TimeSpan(a, 0, 0);
        //    TimeSpan HSalida = new TimeSpan(a, 15, 0)  ;


        //    var dif = HEntrada + HSalida;
        //    var msg = "";
        //    if (dif == null)
        //    {
        //        msg = "Rechazada";
        //    }
        //    else
        //    {
        //        msg = "Nothing";
        //    }

        //    return Json(HEntrada, JsonRequestBehavior.AllowGet);
        //}
        #endregion




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
