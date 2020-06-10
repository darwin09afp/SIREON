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
            ViewBag.title = "Bienvenido | SIREON";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.title = "Sobre nosotros | SIREON";
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.title = "Contactos | SIREON";
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Help()
        {
            ViewBag.title = "Ayuda | SIREON";
            ViewBag.Message = "Your Help page.";

            return View();
        }
    }
}