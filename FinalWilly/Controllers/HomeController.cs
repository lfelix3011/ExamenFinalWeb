using FinalWilly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalWilly.Controllers
{
    public class HomeController : Controller
    {
        private Model1 db = new Model1();

        public ActionResult Index()
        {
            ViewBag.notificacion = "Usuario Registrado";
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario login)
        {
            if (db.Usuario.Where(n => n.Email == login.Email && n.Contraseña == login.Contraseña).ToList().Count > 0)
            {
                Usuario s = db.Usuario.Where(n => n.Email == login.Email).First();
                Session["UserEmail"] = s.Email;
                Session["UserID"] = s.Id;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = "Usuario y/o Contraseña Incorrecta";
            }
            return View();
        }
        

        public ActionResult Logout()
        {

            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }

        public ActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrarse([Bind(Include = "Email, Contraseña")] Usuario reg)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Usuario.Add(reg);
                    db.SaveChanges();

                    ViewBag.notificacion = "Usuario Registrado";
                    return View("Index");
                }
            }
            catch (Exception e)
            {
                ViewBag.notificacion = "El usuario ya existe";
                Console.WriteLine(e);
            }

            return View();
        }


    }
}