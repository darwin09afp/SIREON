using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIREON.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.title = "Bienvenido";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.title = "Sobre nosotros";
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.title = "Contactos";
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Help()
        {
            ViewBag.title = "Ayuda";
            ViewBag.Message = "Your Help page.";

            return View();
        }
    }
}