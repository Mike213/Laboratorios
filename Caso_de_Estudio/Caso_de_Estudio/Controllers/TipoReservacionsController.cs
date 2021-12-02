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
    public class TipoReservacionsController : Controller
    {
        private BDLabTICEntities3 db = new BDLabTICEntities3();

        // GET: TipoReservacions
        public ActionResult Index(String buscar)
        {
            var tipoReservacion = db.TipoReservacion.Include(t => t.Estado1);

            if (!string.IsNullOrEmpty(buscar))
            {
                tipoReservacion = tipoReservacion.Where(t => t.nombreTipoR.Contains(buscar));
            }
            return View(tipoReservacion.ToList());
        }

        // GET: TipoReservacions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoReservacion tipoReservacion = db.TipoReservacion.Find(id);
            if (tipoReservacion == null)
            {
                return HttpNotFound();
            }
            return View(tipoReservacion);
        }

        // GET: TipoReservacions/Create
        public ActionResult Create()
        {
            ViewBag.estado = new SelectList(db.Estado, "id", "nombreEst");
            return View();
        }

        // POST: TipoReservacions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombreTipoR,descripcion,estado")] TipoReservacion tipoReservacion)
        {
            if (ModelState.IsValid)
            {
                var existe = (from d in db.TipoReservacion where d.nombreTipoR == tipoReservacion.nombreTipoR select d).FirstOrDefault();

                if (existe == null)

                {

                    tipoReservacion.estado = 1;
                db.TipoReservacion.Add(tipoReservacion);
                db.SaveChanges();
                return RedirectToAction("Index");

                }
            }

            ViewBag.estado = new SelectList(db.Estado, "id", "nombreEst", tipoReservacion.estado);
            return View(tipoReservacion);
        }

        // GET: TipoReservacions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoReservacion tipoReservacion = db.TipoReservacion.Find(id);
            if (tipoReservacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.estado = new SelectList(db.Estado, "id", "nombreEst", tipoReservacion.estado);
            return View(tipoReservacion);
        }

        // POST: TipoReservacions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombreTipoR,descripcion,estado")] TipoReservacion tipoReservacion)
        {
            if (ModelState.IsValid)
            {
              
                    tipoReservacion.estado = 2;
                db.Entry(tipoReservacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
             
            }
            ViewBag.estado = new SelectList(db.Estado, "id", "nombreEst", tipoReservacion.estado);
            return View(tipoReservacion);
        }

        // GET: TipoReservacions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoReservacion tipoReservacion = db.TipoReservacion.Find(id);
            if (tipoReservacion == null)
            {
                return HttpNotFound();
            }
            return View(tipoReservacion);
        }

        // POST: TipoReservacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoReservacion tipoReservacion = db.TipoReservacion.Find(id);
            db.TipoReservacion.Remove(tipoReservacion);
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
