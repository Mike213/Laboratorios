﻿using System;
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
    public class ComputadorasController : Controller
    {
        private BDLabTICEntities3 db = new BDLabTICEntities3();

        // GET: Computadoras
        public ActionResult Index()
        {
            var computadora = db.Computadora.Include(c => c.Estado1).Include(c => c.Laboratorio);
            return View(computadora.ToList());
        }

        // GET: Computadoras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Computadora computadora = db.Computadora.Find(id);
            if (computadora == null)
            {
                return HttpNotFound();
            }
            return View(computadora);
        }

        // GET: Computadoras/Create
        public ActionResult Create()
        {
            ViewBag.estado = new SelectList(db.Estado, "id", "nombreEst");
            ViewBag.idLab = new SelectList(db.Laboratorio, "id", "nombreLab");
            return View();
        }

        // POST: Computadoras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,caracteristicas,estado,idLab")] Computadora computadora)
        {
            if (ModelState.IsValid)
            {
                db.Computadora.Add(computadora);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.estado = new SelectList(db.Estado, "id", "nombreEst", computadora.estado);
            ViewBag.idLab = new SelectList(db.Laboratorio, "id", "nombreLab", computadora.idLab);
            return View(computadora);
        }

        // GET: Computadoras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Computadora computadora = db.Computadora.Find(id);
            if (computadora == null)
            {
                return HttpNotFound();
            }
            ViewBag.estado = new SelectList(db.Estado, "id", "nombreEst", computadora.estado);
            ViewBag.idLab = new SelectList(db.Laboratorio, "id", "nombreLab", computadora.idLab);
            return View(computadora);
        }

        // POST: Computadoras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,caracteristicas,estado,idLab")] Computadora computadora)
        {
            if (ModelState.IsValid)
            {
                db.Entry(computadora).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.estado = new SelectList(db.Estado, "id", "nombreEst", computadora.estado);
            ViewBag.idLab = new SelectList(db.Laboratorio, "id", "nombreLab", computadora.idLab);
            return View(computadora);
        }

        // GET: Computadoras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Computadora computadora = db.Computadora.Find(id);
            if (computadora == null)
            {
                return HttpNotFound();
            }
            return View(computadora);
        }

        // POST: Computadoras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Computadora computadora = db.Computadora.Find(id);
            db.Computadora.Remove(computadora);
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