using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Caso_de_Estudio.Models;
using Microsoft.Reporting.WebForms;

namespace Caso_de_Estudio.Controllers
{
    public class ComputadorasController : Controller
    {
        private BDLabTICEntities4 db = new BDLabTICEntities4();

        // GET: Computadoras
        public ActionResult Index(string buscar = "")
        {
            var compu = from c in db.Computadora select c;

            compu = compu.Where(c => c.estado.Equals(1));

            if (!string.IsNullOrEmpty(buscar))
            {
                compu = compu.Where(c => c.nombre.Contains(buscar));
            }

            return View(compu.ToList());
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
            ViewBag.idLab = new SelectList(db.Laboratorio.Where(l=> l.estado == 1), "id", "nombreLab");
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
                computadora.estado = 1;
                db.Computadora.Add(computadora);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.estado = new SelectList(db.Estado, "id", "nombreEst", computadora.estado);
            ViewBag.idLab = new SelectList(db.Laboratorio.Where(l => l.estado == 1 ), "id", "nombreLab", computadora.idLab);
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
            ViewBag.idLab = new SelectList(db.Laboratorio.Where(l => l.estado == 1 || l.estado == 2), "id", "nombreLab", computadora.idLab);
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

                computadora.estado =1;
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
            computadora.estado = 2;  
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult VerReporte(string tipo, string buscar = "")
        {
            LocalReport rpt = new LocalReport();
            string mt, enc, f;
            string[] s;
            Warning[] w;


            string ruta = Path.Combine(Server.MapPath("~/Reportes"), "RComputadora.rdlc");
            string deviceInfo = @"<DeviceInfo>
                      <MarginTop>0cm</MarginTop>
                      <MarginLeft>0cm</MarginLeft>
                      <MarginRight>0cm</MarginRight>
                      <MarginBottom>0cm</MarginBottom>
                        <EmbedFonts>None</EmbedFonts>
                    </DeviceInfo>";

            rpt.ReportPath = ruta;

            BDLabTICEntities4 modelo = new BDLabTICEntities4();

            List<vw_reservacion> lista = new List<vw_reservacion>();
            var Compu = from p in db.vw_reservacion select p;

            Compu = Compu.Where(tc => tc.estado == 1 || tc.estado == 2);

            if (!string.IsNullOrEmpty(buscar))
            {
                Compu = Compu.Where(c => c.nombre.Equals(1));
            }
            lista = Compu.ToList();

            ReportDataSource rds = new ReportDataSource("DsComputadora", lista);
            rpt.DataSources.Add(rds);

            var b = rpt.Render(tipo, deviceInfo, out mt, out enc, out f, out s, out w);

            return File(b, mt);
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
