using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalWilly.Models;

namespace FinalWilly.Controllers
{
    public class ContactosController : Controller
    {
        private Model1 db = new Model1();

        // GET: Agenda
        public ActionResult Index()
        {
            var contact = db.Contactos.Include(a => a.Usuario);
            return View(contact.ToList());
        }

        [HttpPost]
        public ActionResult Index(string nombre)
        {
            var contactos = from tabla in db.Contactos select tabla;

            if (!String.IsNullOrEmpty(nombre))
            {
                int uid = (int)Session["UserID"];
                contactos = contactos.Where(tabla => tabla.Nombre.Contains(nombre) && tabla.IdUsuario == uid);
            }
            else
            {
                return RedirectToAction("Agenda");
            }

            return View(contactos.ToList());
        }

        public ActionResult Agenda()
        {
            if (Session["UserID"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            int uid = (int)Session["UserID"];
            var agenda = db.Contactos.Where(a => a.IdUsuario == uid ).ToList();
            
            return View("Index", agenda);
        }

        // GET: Contactos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contactos contactos = db.Contactos.Find(id);
            if (contactos == null)
            {
                return HttpNotFound();
            }
            return View(contactos);
        }

        // GET: Contactos/Create
        public ActionResult Create()
        {
            ViewBag.IdUsuario = new SelectList(db.Usuario, "Id", "Email");
            return View();
        }

        // POST: Contactos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdUsuario,Nombre,Telefono,Correo")] Contactos contactos)
        {
            if (ModelState.IsValid)
            {
                db.Contactos.Add(contactos);
                db.SaveChanges();
                return RedirectToAction("Agenda");
            }

            ViewBag.IdUsuario = new SelectList(db.Usuario, "Id", "Email", contactos.IdUsuario);
            return View(contactos);
        }

        // GET: Contactos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contactos contactos = db.Contactos.Find(id);
            if (contactos == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUsuario = new SelectList(db.Usuario, "Id", "Email", contactos.IdUsuario);
            return View(contactos);
        }

        // POST: Contactos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdUsuario,Nombre,Telefono,Correo")] Contactos contactos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contactos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Agenda");
            }
            ViewBag.IdUsuario = new SelectList(db.Usuario, "Id", "Email", contactos.IdUsuario);
            return View(contactos);
        }

        // GET: Contactos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contactos contactos = db.Contactos.Find(id);
            if (contactos == null)
            {
                return HttpNotFound();
            }
            return View(contactos);
        }

        // POST: Contactos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contactos contactos = db.Contactos.Find(id);
            db.Contactos.Remove(contactos);
            db.SaveChanges();
            return RedirectToAction("Agenda");
        }


        public ActionResult Correo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contactos contactos = db.Contactos.Find(id);
            if (contactos == null)
            {
                return HttpNotFound();
            }
            return View(contactos);
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
