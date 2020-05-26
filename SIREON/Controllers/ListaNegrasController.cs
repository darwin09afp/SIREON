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
    public class ListaNegrasController : Controller
    {
        private SIREONEntities db = new SIREONEntities();

        // GET: ListaNegras
        public ActionResult Index()
        {
            var listaNegras = db.ListaNegras.Include(l => l.Usuario);
            return View(listaNegras.ToList());
        }

        // GET: ListaNegras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListaNegra listaNegra = db.ListaNegras.Find(id);
            if (listaNegra == null)
            {
                return HttpNotFound();
            }
            return View(listaNegra);
        }

        // GET: ListaNegras/Create
        public ActionResult Create()
        {
            ViewBag.ID_Usuario = new SelectList(db.Usuarios, "ID_Usuario", "Usuario1");
            return View();
        }

        // POST: ListaNegras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_ListaN,ID_Usuario,Descripcion")] ListaNegra listaNegra)
        {
            if (ModelState.IsValid)
            {
                db.ListaNegras.Add(listaNegra);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Usuario = new SelectList(db.Usuarios, "ID_Usuario", "Usuario1", listaNegra.ID_Usuario);
            return View(listaNegra);
        }

        // GET: ListaNegras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListaNegra listaNegra = db.ListaNegras.Find(id);
            if (listaNegra == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Usuario = new SelectList(db.Usuarios, "ID_Usuario", "Usuario1", listaNegra.ID_Usuario);
            return View(listaNegra);
        }

        // POST: ListaNegras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_ListaN,ID_Usuario,Descripcion")] ListaNegra listaNegra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(listaNegra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Usuario = new SelectList(db.Usuarios, "ID_Usuario", "Usuario1", listaNegra.ID_Usuario);
            return View(listaNegra);
        }

        // GET: ListaNegras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListaNegra listaNegra = db.ListaNegras.Find(id);
            if (listaNegra == null)
            {
                return HttpNotFound();
            }
            return View(listaNegra);
        }

        // POST: ListaNegras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ListaNegra listaNegra = db.ListaNegras.Find(id);
            db.ListaNegras.Remove(listaNegra);
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
