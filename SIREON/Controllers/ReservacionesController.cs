using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.MappingViews;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using SIREON;
using SIREON.Models;

namespace SIREON.Controllers
{
    [Authorize]
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
                    .Where(x => x.Estatus != "Completada" && x.Estatus != "Rechazada" && x.Estatus != "Cancelada" && x.Estatus != "En Espera")
                    //.Where(x => x.HSalida >= hora)
                    .Where(x => x.Fecha == fechaa).ToList().OrderBy(x => x.HEntrada);
                
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
                    //.Where(x => x.HEntrada >= hora)
                    .Where(x => x.Fecha == fechaa).ToList().OrderBy(x => x.HEntrada);

                return View(reservaciones.ToList());
                
            }

        }

        public ActionResult Reporte1(string ReportType)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/Reservaciones_Dia.rdlc");
            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSet1";
            var fecha = DateTime.Now;
            reportDataSource.Value = db.ResCubs.Where(x => x.Fecha == fecha.Date && x.Estatus != "EnCurso").ToList();
            localReport.DataSources.Add(reportDataSource);
            string RType = ReportType;
            string mimeType;
            string encoding;
            string fileNameExtension;
            if (ReportType == "PDF")
            {
                fileNameExtension = "pdf";

            }
            else
            {
                fileNameExtension = "pdf";

            }

            string deviceInfo = "<DeviceInfo>" +
                    "  <OutputFormat>PDF</OutputFormat>" +
                    "  <PageWidth>8.27in</PageWidth>" +
                    "  <PageHeight>11.69in</PageHeight>" +
                    "  <MarginTop>0.25in</MarginTop>" +
                    "  <MarginLeft>0.4in</MarginLeft>" +
                    "  <MarginRight>0in</MarginRight>" +
                    "  <MarginBottom>0.25in</MarginBottom>" +
                    "  <EmbedFonts>None</EmbedFonts>" +
                    "</DeviceInfo>";

            string[] streams;
            Warning[] warnings;
            byte[] renderedByte;
            renderedByte = localReport.Render("PDF", deviceInfo, out mimeType, out encoding, out fileNameExtension,out streams,out warnings);
            Response.AddHeader("content.disposition", "attachment;filename= Reservaciones_del_dia." + fileNameExtension);
            return File(renderedByte, mimeType);
            //byte[] renderedBytes = localReport.Render("PDF", deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            //Response.AddHeader("content.disposition", "attachment:filename= Reservaciones_del_dia." + fileNameExtension);

        }

        public ActionResult Reporte2(string ReportType)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/Reservaciones_Men.rdlc");
            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSet1";
            var fecha = DateTime.Now;
            reportDataSource.Value = db.ResCubs.Where(x => x.Fecha.Month == fecha.Date.Month && x.Estatus != "EnCurso" && x.Estatus != "En espera" && x.Estatus != "Activa").OrderBy(x => x.Fecha).ToList();
            localReport.DataSources.Add(reportDataSource);
            string RType = ReportType;
            string mimeType;
            string encoding;
            string fileNameExtension;
            if (ReportType == "PDF")
            {
                fileNameExtension = "pdf";

            }
            else
            {
                fileNameExtension = "pdf";

            }

            string deviceInfo = "<DeviceInfo>" +
                    "  <OutputFormat>PDF</OutputFormat>" +
                    "  <PageWidth>8.27in</PageWidth>" +
                    "  <PageHeight>11.69in</PageHeight>" +
                    "  <MarginTop>0.25in</MarginTop>" +
                    "  <MarginLeft>0.4in</MarginLeft>" +
                    "  <MarginRight>0in</MarginRight>" +
                    "  <MarginBottom>0.25in</MarginBottom>" +
                    "  <EmbedFonts>None</EmbedFonts>" +
                    "</DeviceInfo>";

            string[] streams;
            Warning[] warnings;
            byte[] renderedByte;
            renderedByte = localReport.Render("PDF", deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            Response.AddHeader("content.disposition", "attachment;filename= Reservaciones_del_Mes." + fileNameExtension);
            return File(renderedByte, mimeType);
            //byte[] renderedBytes = localReport.Render("PDF", deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            //Response.AddHeader("content.disposition", "attachment:filename= Reservaciones_del_dia." + fileNameExtension);

        }

        public ActionResult Reporte3(string ReportType)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/Reservaciones_Anual.rdlc");
            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSet1";
            var fechaa = DateTime.Now;
            var fecha = fechaa.Date.Year;
            reportDataSource.Value = db.ResCubs.Where(x => x.Fecha.Year == fecha /*&& x.Estatus != "EnCurso" && x.Estatus != "En espera" && x.Estatus != "Activa"*/).OrderBy(x => x.Fecha).ToList();
            localReport.DataSources.Add(reportDataSource);
            string RType = ReportType;
            string mimeType;
            string encoding;
            string fileNameExtension;
            if (ReportType == "PDF")
            {
                fileNameExtension = "pdf";

            }
            else
            {
                fileNameExtension = "pdf";

            }

            string deviceInfo = "<DeviceInfo>" +
                    "  <OutputFormat>PDF</OutputFormat>" +
                    "  <PageWidth>8.27in</PageWidth>" +
                    "  <PageHeight>11.69in</PageHeight>" +
                    "  <MarginTop>0.25in</MarginTop>" +
                    "  <MarginLeft>0.4in</MarginLeft>" +
                    "  <MarginRight>0in</MarginRight>" +
                    "  <MarginBottom>0.25in</MarginBottom>" +
                    "  <EmbedFonts>None</EmbedFonts>" +
                    "</DeviceInfo>";

            string[] streams;
            Warning[] warnings;
            byte[] renderedByte;
            renderedByte = localReport.Render("PDF", deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            Response.AddHeader("content.disposition", "attachment;filename= Reservaciones_del_Mes." + fileNameExtension);
            return File(renderedByte, mimeType);
            //byte[] renderedBytes = localReport.Render("PDF", deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            //Response.AddHeader("content.disposition", "attachment:filename= Reservaciones_del_dia." + fileNameExtension);

        }

        public ActionResult Reporte4(string ReportType)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/ListaNegra_Dia.rdlc");
            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "ListaNe";
            var fecha = DateTime.Now;
            reportDataSource.Value = db.ListaNs.Where(x => x.FechaEntrada == fecha.Date).ToList();
            localReport.DataSources.Add(reportDataSource);
            string RType = ReportType;
            string mimeType;
            string encoding;
            string fileNameExtension;
            if (ReportType == "PDF")
            {
                fileNameExtension = "pdf";

            }
            else
            {
                fileNameExtension = "pdf";

            }

            string deviceInfo = "<DeviceInfo>" +
                    "  <OutputFormat>PDF</OutputFormat>" +
                    "  <PageWidth>8.27in</PageWidth>" +
                    "  <PageHeight>11.69in</PageHeight>" +
                    "  <MarginTop>0.25in</MarginTop>" +
                    "  <MarginLeft>0.4in</MarginLeft>" +
                    "  <MarginRight>0in</MarginRight>" +
                    "  <MarginBottom>0.25in</MarginBottom>" +
                    "  <EmbedFonts>None</EmbedFonts>" +
                    "</DeviceInfo>";

            string[] streams;
            Warning[] warnings;
            byte[] renderedByte;
            renderedByte = localReport.Render("PDF", deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            Response.AddHeader("content.disposition", "attachment;filename= Reservaciones_del_Mes." + fileNameExtension);
            return File(renderedByte, mimeType);
            //byte[] renderedBytes = localReport.Render("PDF", deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            //Response.AddHeader("content.disposition", "attachment:filename= Reservaciones_del_dia." + fileNameExtension);

        }

        public ActionResult Reporte5(string ReportType)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/ListaNegra_Men.rdlc");
            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "ListaNe";
            var fecha = DateTime.Now;
            reportDataSource.Value = db.ListaNs.Where(x => x.FechaEntrada.Month == fecha.Month).ToList();
            localReport.DataSources.Add(reportDataSource);
            string RType = ReportType;
            string mimeType;
            string encoding;
            string fileNameExtension;
            if (ReportType == "PDF")
            {
                fileNameExtension = "pdf";

            }
            else
            {
                fileNameExtension = "pdf";

            }

            string deviceInfo = "<DeviceInfo>" +
                    "  <OutputFormat>PDF</OutputFormat>" +
                    "  <PageWidth>8.27in</PageWidth>" +
                    "  <PageHeight>11.69in</PageHeight>" +
                    "  <MarginTop>0.25in</MarginTop>" +
                    "  <MarginLeft>0.4in</MarginLeft>" +
                    "  <MarginRight>0in</MarginRight>" +
                    "  <MarginBottom>0.25in</MarginBottom>" +
                    "  <EmbedFonts>None</EmbedFonts>" +
                    "</DeviceInfo>";

            string[] streams;
            Warning[] warnings;
            byte[] renderedByte;
            renderedByte = localReport.Render("PDF", deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            Response.AddHeader("content.disposition", "attachment;filename= Reservaciones_del_Mes." + fileNameExtension);
            return File(renderedByte, mimeType);
            //byte[] renderedBytes = localReport.Render("PDF", deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            //Response.AddHeader("content.disposition", "attachment:filename= Reservaciones_del_dia." + fileNameExtension);

        }

        public ActionResult Reporte6(string ReportType)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/ListaNegra_Anual.rdlc");
            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "ListaNe";
            var fecha = DateTime.Now;
            reportDataSource.Value = db.ListaNs.Where(x => x.FechaEntrada.Year == fecha.Year).ToList();
            localReport.DataSources.Add(reportDataSource);
            string RType = ReportType;
            string mimeType;
            string encoding;
            string fileNameExtension;
            if (ReportType == "PDF")
            {
                fileNameExtension = "pdf";

            }
            else
            {
                fileNameExtension = "pdf";

            }

            string deviceInfo = "<DeviceInfo>" +
                    "  <OutputFormat>PDF</OutputFormat>" +
                    "  <PageWidth>8.27in</PageWidth>" +
                    "  <PageHeight>11.69in</PageHeight>" +
                    "  <MarginTop>0.25in</MarginTop>" +
                    "  <MarginLeft>0.4in</MarginLeft>" +
                    "  <MarginRight>0in</MarginRight>" +
                    "  <MarginBottom>0.25in</MarginBottom>" +
                    "  <EmbedFonts>None</EmbedFonts>" +
                    "</DeviceInfo>";

            string[] streams;
            Warning[] warnings;
            byte[] renderedByte;
            renderedByte = localReport.Render("PDF", deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            Response.AddHeader("content.disposition", "attachment;filename= Reservaciones_del_Mes." + fileNameExtension);
            return File(renderedByte, mimeType);
            //byte[] renderedBytes = localReport.Render("PDF", deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            //Response.AddHeader("content.disposition", "attachment:filename= Reservaciones_del_dia." + fileNameExtension);

        }



        public ActionResult Index2()
        {

            if (User.IsInRole("Operador"))
            {

                var fecha = DateTime.Now;
                var fechaa = fecha.Date;
                var hora = fecha.TimeOfDay;
                var reservaciones = db.Reservaciones.Where(x => x.Estatus != "Completada" && x.Estatus != "Rechazada" && x.Estatus != "Cancelada" && x.Estatus != "En Espera")/*.Where(x => x.HSalida >= hora)*/.Where(x => x.Fecha == fechaa).ToList();
                //var solicitante = db.Reservaciones.Contains(reservaciones).

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
                    //.Where(x => x.HEntrada >= hora)
                    .Where(x => x.Fecha == fechaa).ToList();

                return View(reservaciones.ToList());

            }

        }



        // GET: Reservaciones
        public ActionResult Invitaciones()
        {
           
            var fecha = DateTime.Now;
            var fechaa = fecha.Date;
            var hora = fecha.TimeOfDay;
            var user = User.Identity.GetUserId();

            var Salas = db.Reservaciones.Where(x => x.Reservaciones_Usuarios.Where(p => p.IdAspNetUser == user && p.Estatus != "Rechazada" && p.Estatus != "Aceptada").Any())
                //.Where(x => x.Fecha == fechaa)
                .Where(x => x.Estatus == "Activa" || x.Estatus == "En espera")
                .Where(x => x.Reservaciones_Usuarios.Any(p => p.IdAspNetUser == user))
                .ToList();

            return View(Salas);

        }

        public ActionResult Invitaciones2()
        {

            var user = User.Identity.GetUserId();

            var Salas = db.Reservaciones_Usuarios.Where(x => x.IdAspNetUser == user).ToList();

            var Inv = db.Reservaciones
                .Where(x => x.Estatus == "Completada" || x.Estatus == "Cancelada" || x.Estatus == "Rechazada")
                .Where(x => x.Reservaciones_Usuarios.Any(p => p.IdAspNetUser == user))
                .ToList();


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
            var usr = User.Identity.GetUserId();
            if (User.IsInRole("Operador"))
            {
                var reservaciones = db.Reservaciones.Include(r => r.Cubiculo).Include(r => r.AspNetUser).Include(r => r.AspNetUser1).OrderByDescending(x => x.Fecha);
                return View(reservaciones.ToList());
            }
            else
            {
                var reservaciones = db.Reservaciones.Where(x => x.IdAspNetUsers == usr && x.Estatus != "EnCurso" && x.Estatus != "Activa").OrderByDescending(x => x.Fecha).ToList() ;
                return View(reservaciones.ToList());
            }
            
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

        //Aceptar invitacion
        public ActionResult Aceptar(int? id)
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
            //ViewBag.ID_Cubiculo = new SelectList(db.Reservaciones_Usuarios, "ID_Cubiculo", "Descripcion", reservacione.ID_Cubiculo);
            //ViewBag.IdAspNetUsers = new SelectList(db.AspNetUsers, "Id", "Email", reservacione.IdAspNetUsers);
            //ViewBag.ID_Empleado = new SelectList(db.AspNetUsers, "Id", "Email", reservacione.ID_Empleado);
            var usr = User.Identity.GetUserId();
            var ResUsr = db.Reservaciones_Usuarios.Where(x => x.IdReservacion == reservacione.ID_Reservacion && x.IdAspNetUser == usr).FirstOrDefault().Id;
            var res = db.Reservaciones_Usuarios.Where(x => x.Id == ResUsr).FirstOrDefault();

            if (res.Estatus == "Rechazada")
            {  
                var prueba = "Esta invitación fue rechazada previamente, no podrá realizar esta acción";
                return RedirectToAction("Invitaciones");
            }
            else
            {
                res.Estatus = "Aceptada";
                var prueba = "Invitacion Aceptada";
                
                db.SaveChanges();
                return RedirectToAction("Invitaciones");
            }
            
            
            
            
            
        }
        //Rechazar invitacion       
        public ActionResult Rechazar(int? id)
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
            //ViewBag.ID_Cubiculo = new SelectList(db.Reservaciones_Usuarios, "ID_Cubiculo", "Descripcion", reservacione.ID_Cubiculo);
            //ViewBag.IdAspNetUsers = new SelectList(db.AspNetUsers, "Id", "Email", reservacione.IdAspNetUsers);
            //ViewBag.ID_Empleado = new SelectList(db.AspNetUsers, "Id", "Email", reservacione.ID_Empleado);
            var usr = User.Identity.GetUserId();
            var ResUsr = db.Reservaciones_Usuarios.Where(x => x.IdReservacion == reservacione.ID_Reservacion && x.IdAspNetUser == usr).FirstOrDefault().Id;
            var res = db.Reservaciones_Usuarios.Where(x => x.Id == ResUsr).FirstOrDefault();
            
            if (res.Estatus == "Aceptada")
            {
                var prueba = "Esta invitación fue Aceptada previamente, no podrá realizar esta acción";
                return RedirectToAction("Invitaciones");
            }
            else
            {

                res.Estatus = "Rechazada";
                var prueba = "Invitación Rechazada";
                db.SaveChanges();


                //var Resid = db.Reservaciones_Usuarios.Where(x => x.IdReservacion == reservacione.ID_Reservacion && x.IdAspNetUser == usr).FirstOrDefault().IdReservacion;
                var reser = db.Reservaciones_Usuarios.Where(x => x.IdReservacion == reservacione.ID_Reservacion && x.Estatus != "Rechazada" && x.Estatus != "NA").Count();
                var reserva = db.Reservaciones.Where(x => x.ID_Reservacion == reservacione.ID_Reservacion).First();
                if (reser < 1)
                {

                    reserva.Estatus = "Rechazada";
                    db.SaveChanges();

                }

                return RedirectToAction("Invitaciones");
            }
           
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


        // GET: Reservaciones1/Details/5
        public ActionResult Details2(string id)
        {            
            var res = db.Reservaciones.Where(x => x.IdAspNetUsers == id).OrderByDescending(p => p.Fecha).ToList();
            return View(res);
        }

        [HttpPost]
        public JsonResult ConsInvitados(int idres)
        {

            db.Configuration.ProxyCreationEnabled = false;
            List<string[]> Usuarios = new List<string[]>();
            var result3 = new string[] { };
            CustomModel3 mymodel = new CustomModel3();
            var i = 0;
            var user = db.AspNetUsers.Where(x => x.Reservaciones_Usuarios.Where(p => p.IdReservacion == idres && p.IdAspNetUser != null).Any()).ToList();

            foreach (var item in user)
            {
                var BuscarCorreo = db.AspNetUsers.Where(x => x.Id == item.Id).FirstOrDefault().Email;
                var BuscarNombre = db2.Entidads.Where(x => x.CorreoInst == BuscarCorreo).FirstOrDefault().Nombre;
                var BuscarApellido = db2.Entidads.Where(x => x.CorreoInst == BuscarCorreo).FirstOrDefault().Apellido;
                var BuscarMart = db2.Entidads.Where(x => x.CorreoInst == BuscarCorreo).FirstOrDefault().CodigoInst;
                var estatus = db.Reservaciones_Usuarios.Where(p => p.IdReservacion == idres && p.IdAspNetUser == item.Id).First().Estatus;
                var result2 = new string[] { BuscarNombre + BuscarApellido, BuscarMart, estatus};

                Usuarios.Add(result2);

            }

            var invitados = db.Reservaciones_Usuarios.Where(x => x.IdReservacion == idres && x.IdAspNetUser == null).ToList();
            foreach (var item in invitados)
            {
                var nombre = item.NombreInvitado;
                var cedula = item.CedulaInvitado;
                var estatus = item.Estatus;
                var result2 = new string[] { nombre, cedula, estatus };
                
                Usuarios.Add(result2);

            }
            //var result3 = new string[] { result2 };




            //mymodel.RU1 = Usuarios;
            //mymodel.RU2 = db.Reservaciones_Usuarios.Where(x => x.IdReservacion == idres && x.IdAspNetUser == null).ToList();


            //var json = JsonConvert.SerializeObject(Usuarios);
            return Json(Usuarios, JsonRequestBehavior.AllowGet);

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

        #region Entradar/Rechazar/Completar/Cancelar
        [HttpPost]
        public ActionResult DarEntrada(Int32? idres, Int32? idcub, TimeSpan? HEntrada, DateTime? Fecha)
        {

            var query = db.Reservaciones.Where(x => x.ID_Reservacion == idres).FirstOrDefault();
            var cub = db.Disponibilidads.Where(p => p.Fecha == Fecha && p.HoraInicial == HEntrada && p.IdCubiculo == idcub && p.Estatus == "Reservado").First();
            query.Estatus = "EnCurso";
            cub.Estatus = "Ocupado";
            query.ID_Empleado = User.Identity.GetUserId();
            db.SaveChanges();
            var res = "Listo";

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Rechazar(Int32? idres, Int32? idcub, TimeSpan? HEntrada, DateTime? Fecha)
        {

            var query = db.Reservaciones.Where(x => x.ID_Reservacion == idres).FirstOrDefault();
            var cub = db.Disponibilidads.Where(p => p.Fecha == Fecha && p.HoraInicial == HEntrada && p.IdCubiculo == idcub && p.Estatus == "Reservado").First();
            query.Estatus = "Rechazada";
            cub.Estatus = "Disponible";
            query.ID_Empleado = User.Identity.GetUserId();
            db.SaveChanges();
            var res = "Listo";

            return Json(res, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Cancelar(Int32? idres, Int32? idcub, TimeSpan? HEntrada, DateTime? Fecha)
        {

            var query = db.Reservaciones.Where(x => x.ID_Reservacion == idres).FirstOrDefault();
            var cub = db.Disponibilidads.Where(p => p.Fecha == Fecha && p.HoraInicial == HEntrada && p.IdCubiculo == idcub && p.Estatus == "Reservado").First();
            query.Estatus = "Cancelada";
            cub.Estatus = "Disponible";
            db.SaveChanges();
            var res = "Listo";

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Completar(Int32? idres, Int32? idcub, TimeSpan? HEntrada, DateTime? Fecha)
        {

            var query = db.Reservaciones.Where(x => x.ID_Reservacion == idres).FirstOrDefault();
            var cub = db.Disponibilidads.Where(p => p.Fecha == Fecha && p.HoraInicial == HEntrada && p.IdCubiculo == idcub && p.Estatus == "Ocupado").First();
            query.Estatus = "Completada";
            cub.Estatus = "Disponible";
            db.SaveChanges();
            var res = "Listo";

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Usuario
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




            var json = JsonConvert.SerializeObject(query);


            return Json(query, JsonRequestBehavior.AllowGet);

        }
        public JsonResult Usuario5()
        {
            //Metodo solo para presentar el nombre en la reservacion la matricula del estudiante loggeado 

            var IdAspNetUsers2 = User.Identity.GetUserId();
            var BuscarCorreo = db.AspNetUsers.Where(x => x.Id == IdAspNetUsers2).FirstOrDefault().Email;
            var BuscarNombre = db2.Entidads.Where(x => x.CorreoInst == BuscarCorreo).FirstOrDefault().Nombre;
            var BuscarApellido = db2.Entidads.Where(x => x.CorreoInst == BuscarCorreo).FirstOrDefault().Apellido;
            var BuscarMart = db2.Entidads.Where(x => x.CorreoInst == BuscarCorreo).FirstOrDefault().CodigoInst;
            var msg2 = "";

            var lista = db.ListaNegras.Where(x => x.IdAspNetUsers == IdAspNetUsers2 && x.Estatus == "Bloqueado").FirstOrDefault();
            if (lista != null)
            {
                msg2 = "El usuario tiene el uso limitado, no podrá realizar la reservación.";

            }
            else
            {
                msg2 = "El usuario no tiene límites de uso";
            }


            var result3 = new string[] { BuscarNombre, BuscarApellido, BuscarMart, msg2 };
            //string str1 = strArr[0], str2 = strArr[1];

            //var json = JsonConvert.SerializeObject(query);


            return Json(result3, JsonRequestBehavior.AllowGet);
            
        }

        public JsonResult Usuario2(string IdAspNetUsers2)
        {
            //Metodo solo para presentar el nombre en la reservacion la matricula del estudiante loggeado 
            //Espera una Matricula

            var BuscarNombre = db2.Entidads.Where(x => x.CodigoInst == IdAspNetUsers2).FirstOrDefault().Nombre;
            var BuscarApellido = db2.Entidads.Where(x => x.CodigoInst == IdAspNetUsers2).FirstOrDefault().Apellido;
            var BuscarCorreo = db2.Entidads.Where(x => x.CodigoInst == IdAspNetUsers2).FirstOrDefault().CorreoInst;
            var idu = db.AspNetUsers.Where(x => x.Email == BuscarCorreo).FirstOrDefault().Id;
            var msg2 = "";
            
            var lista = db.ListaNegras.Where(x => x.IdAspNetUsers == idu && x.Estatus == "Bloqueado").FirstOrDefault();
            if (lista != null)
            {
                msg2 = "El usuario tiene el uso limitado, no podrá realizar la reservación.";
            
            }
            else
            {
                msg2 = "El usuario no tiene límites de uso";
            }


            var result3 = new string[] { BuscarNombre, BuscarApellido, BuscarCorreo , idu, msg2 };
            //string str1 = strArr[0], str2 = strArr[1];

            //var json = JsonConvert.SerializeObject(query);


            return Json(result3, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult Usuario3(string IdAspNetUser)
        {
            //Metodo solo para presentar el nombre en la reservacion la matricula del estudiante loggeado 
            //Espera una Matricula
            var BuscarNombre = db2.Entidads.Where(x => x.CodigoInst == IdAspNetUser).FirstOrDefault().Nombre;
            if (BuscarNombre == null)
            {
                var result3 = "Vacio";
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

        public JsonResult Usuario4(string IdAspNetUsers2)
        {
            //Metodo solo para presentar el nombre en la reservacion la matricula del estudiante loggeado 
            //Espera un ID
            var BuscarCorreo = db.AspNetUsers.Where(x => x.Id == IdAspNetUsers2).FirstOrDefault().Email;
            var BuscarNombre = db2.Entidads.Where(x => x.CorreoInst == BuscarCorreo).FirstOrDefault().Nombre;
            var BuscarApellido = db2.Entidads.Where(x => x.CorreoInst == BuscarCorreo).FirstOrDefault().Apellido;

            var result3 = new string[] { BuscarNombre, BuscarApellido };
            //string str1 = strArr[0], str2 = strArr[1];

            //var json = JsonConvert.SerializeObject(query);


            return Json(result3, JsonRequestBehavior.AllowGet);

        }
        #endregion


        #region reservaciones
        public JsonResult AsignarCubiculo(string IdAspNetUsers2, Int32 idres, TimeSpan HEntrada, TimeSpan HSalida)
        {
            //Disponibilidad
            //#region disponibilidad
            List<int> CubNoDisp = new List<int>();
            List<int> Cubs = new List<int>();
            var Fechaa = DateTime.Now;
            var Fecha = Fechaa.Date;
            var SelHora = Fechaa.TimeOfDay;

            var cubiculoo = db.Disponibilidads.Where(x => x.Fecha == Fecha && x.HoraInicial == HEntrada && x.Estatus == "Disponible").First();
            var res = db.Reservaciones.Where(x => x.ID_Reservacion == idres).First();
            
            res.ID_Cubiculo = cubiculoo.IdCubiculo; ;
            res.Estatus = "EnCurso";
            res.ID_Empleado = User.Identity.GetUserId();
            cubiculoo.Estatus = "Ocupado";
            db.SaveChanges();

            var result2 = "a";

            return Json(result2, JsonRequestBehavior.AllowGet);

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
            var usr = "";
            var usr2 = "";
            
            if (User.IsInRole("Operador"))
            {
                usr = IdAspNetUsers2;
                var BuscarNombre = db2.Entidads.Where(x => x.CodigoInst == usr).FirstOrDefault().Nombre;
                var BuscarApellido = db2.Entidads.Where(x => x.CodigoInst == IdAspNetUsers2).FirstOrDefault().Apellido;
                var BuscarCorreo = db2.Entidads.Where(x => x.CodigoInst == IdAspNetUsers2).FirstOrDefault().CorreoInst;
                usr2 = db.AspNetUsers.Where(x => x.Email == BuscarCorreo).FirstOrDefault().Id;

            }
            else
            {
                usr2 = usuario;
            }




            var cant = db.Reservaciones.Where(x => x.Fecha == SelFecha && x.IdAspNetUsers == usr2 && x.Estatus != "Cancelada" && x.Estatus != "Rechazada").Count();
            var Parm = db.Parametros.Where(x => x.ID == 2).First().Valor;
            
            if (cant <= Parm)
            {
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
                    model2.Estatus = "Reservado";
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
                        Res.Estatus = "Pendiente";
                        db.Reservaciones_Usuarios.Add(Res);
                        db.SaveChanges();
                    }
                    else
                    {

                        Res.IdAspNetUser = item.IdAspNetUser;
                        Res.IdReservacion = model.ID_Reservacion;
                        Res.NombreInvitado = item.NombreInvitado;
                        Res.CedulaInvitado = item.CedulaInvitado;
                        Res.Estatus = "NA";
                        db.Reservaciones_Usuarios.Add(Res);
                        db.SaveChanges();
                    }

                }
                db.SaveChanges();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                result = "Usted ha alcanzado el máximo de reservaciones para el día de hoy";
                return Json(result, JsonRequestBehavior.AllowGet);
            }


            
        }
        #endregion


        #region default
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
        #endregion

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
