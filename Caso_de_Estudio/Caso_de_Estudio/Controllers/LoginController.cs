using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity; 
using System.Net; 
using Caso_de_Estudio.Models;

namespace Caso_de_Estudio.Controllers
{
    public class LoginController : Controller
    {

        private BDLabTICEntities3 db = new BDLabTICEntities3();
        // GET: Login
        public ActionResult Borrar()
        {
            Session["User"] = null;
            return  RedirectToAction("Login");
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string User, string Pass)
        {
            try
            {
                using (Models.BDLabTICEntities3 db = new Models.BDLabTICEntities3())
                {
                    var oUser = (from d in db.Usuario
                                 where d.email == User.Trim() && d.pwd == Pass.Trim()
                                 select d).FirstOrDefault();
                    if (oUser == null)
                    {
                        ViewBag.Error = "Usuario o Contraseña Incorrecto";
                        return View();
                    }

                    Session["User"] = oUser;
                    String temp = oUser.nombres + " " + oUser.apellidos;
                    System.Web.HttpContext.Current.Session["id"] = oUser.idUser;
                    System.Web.HttpContext.Current.Session["idrol"] = oUser.idRol;

                    string temp2 ="" +  oUser.idRol;


                    System.Web.HttpContext.Current.Session["Username"] = temp;
                    System.Web.HttpContext.Current.Session["idUser"] = temp2;
                    // ViewBag.Data = oUser.nombres + " " + oUser.apellidos;
                }
                return RedirectToAction("index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
        public ActionResult Create()
        {
            ViewBag.estado = new SelectList(db.Estado, "id", "nombreEst");
            ViewBag.idRol = new SelectList(db.Rol, "idrol", "rolName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idUser,username,email,pwd,estado,nombres,apellidos,idRol")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuario.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("./Login");
            }

            ViewBag.estado = new SelectList(db.Estado, "id", "nombreEst", usuario.estado);
            ViewBag.idRol = new SelectList(db.Rol, "idrol", "rolName", usuario.idRol);

            return RedirectToAction("./Login");
        }
    }
}