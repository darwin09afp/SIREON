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
    public class RSalasController : Controller
    {
        private SIREONEntities db = new SIREONEntities();

        // GET: RSalas
        public ActionResult Index()
        {
            var r_Salas_Usuarios = db.R_Salas_Usuarios.Include(r => r.Sala).Include(r => r.Usuario);
            return View(r_Salas_Usuarios.ToList());
        }

        // GET: RSalas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            R_Salas_Usuarios r_Salas_Usuarios = db.R_Salas_Usuarios.Find(id);
            if (r_Salas_Usuarios == null)
            {
                return HttpNotFound();
            }
            return View(r_Salas_Usuarios);
        }

        // GET: RSalas/Create
        public ActionResult Create()
        {
            ViewBag.ID_Sala = new SelectList(db.Salas, "ID_Sala", "ID_Sala");
            ViewBag.ID_Usuario = new SelectList(db.Usuarios, "ID_Usuario", "Usuario1");
            return View();
        }

        // POST: RSalas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_RSalas,ID_Sala,ID_Usuario")] R_Salas_Usuarios r_Salas_Usuarios)
        {
            if (ModelState.IsValid)
            {
                db.R_Salas_Usuarios.Add(r_Salas_Usuarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Sala = new SelectList(db.Salas, "ID_Sala", "ID_Sala", r_Salas_Usuarios.ID_Sala);
            ViewBag.ID_Usuario = new SelectList(db.Usuarios, "ID_Usuario", "Usuario1", r_Salas_Usuarios.ID_Usuario);
            return View(r_Salas_Usuarios);
        }

        // GET: RSalas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            R_Salas_Usuarios r_Salas_Usuarios = db.R_Salas_Usuarios.Find(id);
            if (r_Salas_Usuarios == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Sala = new SelectList(db.Salas, "ID_Sala", "ID_Sala", r_Salas_Usuarios.ID_Sala);
            ViewBag.ID_Usuario = new SelectList(db.Usuarios, "ID_Usuario", "Usuario1", r_Salas_Usuarios.ID_Usuario);
            return View(r_Salas_Usuarios);
        }

        // POST: RSalas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_RSalas,ID_Sala,ID_Usuario")] R_Salas_Usuarios r_Salas_Usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(r_Salas_Usuarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Sala = new SelectList(db.Salas, "ID_Sala", "ID_Sala", r_Salas_Usuarios.ID_Sala);
            ViewBag.ID_Usuario = new SelectList(db.Usuarios, "ID_Usuario", "Usuario1", r_Salas_Usuarios.ID_Usuario);
            return View(r_Salas_Usuarios);
        }

        // GET: RSalas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            R_Salas_Usuarios r_Salas_Usuarios = db.R_Salas_Usuarios.Find(id);
            if (r_Salas_Usuarios == null)
            {
                return HttpNotFound();
            }
            return View(r_Salas_Usuarios);
        }

        // POST: RSalas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            R_Salas_Usuarios r_Salas_Usuarios = db.R_Salas_Usuarios.Find(id);
            db.R_Salas_Usuarios.Remove(r_Salas_Usuarios);
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
