using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Caso_de_Estudio.Models;

namespace Caso_de_Estudio.Controllers
{
    public class Reservacions1Controller : Controller
    {
        private BDLabTICEntities3 db = new BDLabTICEntities3();

        // GET: Reservacions1
        public ActionResult Index()
        {
            var reservacion = db.Reservacion.Include(r => r.Computadora).Include(r => r.Usuario).Include(r => r.TipoReservacion);
            return View(reservacion.ToList());
        }

        // GET: Reservacions1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservacion reservacion = db.Reservacion.Find(id);
            if (reservacion == null)
            {
                return HttpNotFound();
            }
            return View(reservacion);
        }

        // GET: Reservacions1/Create
        public ActionResult Create()
        {
            ViewBag.pcReservacion = new SelectList(db.Computadora, "id", "nombre");
            ViewBag.idpersona = new SelectList(db.Usuario, "idUser", "username");
            ViewBag.tipoR = new SelectList(db.TipoReservacion, "id", "nombreTipoR");
            return View();
        }

        // POST: Reservacions1/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,idpersona,tipoR,pcReservacion,fecha,horaEntrada,horaSalida,observacion")] Reservacion reservacion)
        {
            if (ModelState.IsValid)
            {
                db.Reservacion.Add(reservacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.pcReservacion = new SelectList(db.Computadora, "id", "nombre", reservacion.pcReservacion);
            ViewBag.idpersona = new SelectList(db.Usuario, "idUser", "username", reservacion.idpersona);
            ViewBag.tipoR = new SelectList(db.TipoReservacion, "id", "nombreTipoR", reservacion.tipoR);
            return View(reservacion);
        }

        // GET: Reservacions1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservacion reservacion = db.Reservacion.Find(id);
            if (reservacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.pcReservacion = new SelectList(db.Computadora, "id", "nombre", reservacion.pcReservacion);
            ViewBag.idpersona = new SelectList(db.Usuario, "idUser", "username", reservacion.idpersona);
            ViewBag.tipoR = new SelectList(db.TipoReservacion, "id", "nombreTipoR", reservacion.tipoR);
            return View(reservacion);
        }

        // POST: Reservacions1/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,idpersona,tipoR,pcReservacion,fecha,horaEntrada,horaSalida,observacion")] Reservacion reservacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.pcReservacion = new SelectList(db.Computadora, "id", "nombre", reservacion.pcReservacion);
            ViewBag.idpersona = new SelectList(db.Usuario, "idUser", "username", reservacion.idpersona);
            ViewBag.tipoR = new SelectList(db.TipoReservacion, "id", "nombreTipoR", reservacion.tipoR);
            return View(reservacion);
        }

        // GET: Reservacions1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservacion reservacion = db.Reservacion.Find(id);
            if (reservacion == null)
            {
                return HttpNotFound();
            }
            return View(reservacion);
        }

        // POST: Reservacions1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservacion reservacion = db.Reservacion.Find(id);
            db.Reservacion.Remove(reservacion);
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
