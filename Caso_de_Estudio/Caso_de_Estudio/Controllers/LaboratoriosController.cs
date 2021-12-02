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
    public class LaboratoriosController : Controller
    {
        private BDLabTICEntities3 db = new BDLabTICEntities3();

        // GET: Laboratorios
        public ActionResult Index(String buscar)
        {
            var laboratorio = db.Laboratorio.Include(l => l.Estado1);

            if (!string.IsNullOrEmpty(buscar))
            {
                laboratorio = laboratorio.Where(l=> l.nombreLab.Contains(buscar));
            }

            return View(laboratorio.ToList());
        }

        // GET: Laboratorios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laboratorio laboratorio = db.Laboratorio.Find(id);
            if (laboratorio == null)
            {
                return HttpNotFound();
            }
            return View(laboratorio);
        }

        // GET: Laboratorios/Create
        public ActionResult Create()
        {
            ViewBag.estado = new SelectList(db.Estado, "id", "nombreEst");
            return View();
        }

        // POST: Laboratorios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombreLab,estado")] Laboratorio laboratorio)
        {
            if (ModelState.IsValid)
            {
                laboratorio.estado = 1;
                db.Laboratorio.Add(laboratorio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.estado = new SelectList(db.Estado, "id", "nombreEst", laboratorio.estado);
            return View(laboratorio);
        }

        // GET: Laboratorios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laboratorio laboratorio = db.Laboratorio.Find(id);
            if (laboratorio == null)
            {
                return HttpNotFound();
            }
            ViewBag.estado = new SelectList(db.Estado, "id", "nombreEst", laboratorio.estado);
            return View(laboratorio);
        }

        // POST: Laboratorios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombreLab,estado")] Laboratorio laboratorio)
        {
            if (ModelState.IsValid)
            {
                laboratorio.estado = 2;
                db.Entry(laboratorio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.estado = new SelectList(db.Estado, "id", "nombreEst", laboratorio.estado);
            return View(laboratorio);
        }

        // GET: Laboratorios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laboratorio laboratorio = db.Laboratorio.Find(id);
            if (laboratorio == null)
            {
                return HttpNotFound();
            }
            return View(laboratorio);
        }

        // POST: Laboratorios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Laboratorio laboratorio = db.Laboratorio.Find(id);
            laboratorio.estado = 2;
            db.Entry(laboratorio).State = EntityState.Modified;
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
