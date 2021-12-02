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
    public class ReservacionsController : Controller
    {
        private BDLabTICEntities4 db = new BDLabTICEntities4();

        // GET: Reservacions
        public ActionResult Index(String buscar)
        {
            var reservacion = db.Reservacion.Include(r => r.Computadora).Include(r => r.Usuario).Include(r => r.TipoReservacion);
            if (!string.IsNullOrEmpty(buscar))
            {
                reservacion = reservacion.Where(r => r.Computadora.nombre.Contains(buscar));




            }


            return View(reservacion.ToList());
        }

        // GET: Reservacions/Details/5
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

        // GET: Reservacions/Create
        public ActionResult Create()
        {
            ViewBag.pcReservacion = new SelectList(db.Computadora.Where(c => c.estado == 1 ), "id", "nombre");
            ViewBag.idpersona = new SelectList(db.Usuario.Where(u => u.estado == 1), "idUser", "username");
            ViewBag.tipoR = new SelectList(db.TipoReservacion.Where(t => t.estado ==1), "id", "nombreTipoR");
            return View();
        }

        // POST: Reservacions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Reservacion reservacion)
        {
            if (ModelState.IsValid)


            {

                var r = new Reservacion();
                String temp = Convert.ToString(Session["id"]);
                int idP = int.Parse(temp);
                r.id = 0;
                r.idpersona = idP;
                r.horaEntrada = reservacion.horaEntrada;
                r.horaSalida = reservacion.horaSalida;
                r.fecha = reservacion.fecha;
                r.pcReservacion = reservacion.pcReservacion;
                r.observacion = reservacion.observacion;
                r.tipoR = reservacion.tipoR;

                
                db.Reservacion.Add(r);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.pcReservacion = new SelectList(db.Computadora.Where(c => c.estado == 1 ), "id", "nombre", reservacion.pcReservacion);
            ViewBag.idpersona = new SelectList(db.Usuario.Where(c => c.estado == 1 ), "idUser", "username", reservacion.idpersona);
            ViewBag.tipoR = new SelectList(db.TipoReservacion.Where(t => t.estado == 1 ), "id", "nombreTipoR", reservacion.tipoR);
            return View(reservacion);
        }

        // GET: Reservacions/Edit/5
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
            ViewBag.pcReservacion = new SelectList(db.Computadora.Where(c => c.estado == 1), "id", "nombre", reservacion.pcReservacion);
            ViewBag.idpersona = new SelectList(db.Usuario.Where(u => u.estado == 1), "idUser", "username", reservacion.idpersona);
            ViewBag.tipoR = new SelectList(db.TipoReservacion.Where(t => t.estado == 1 ), "id", "nombreTipoR", reservacion.tipoR);
            return View(reservacion);
        }

        // POST: Reservacions/Edit/5
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

        // GET: Reservacions/Delete/5
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

        // POST: Reservacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservacion reservacion = db.Reservacion.Find(id);
            db.Reservacion.Remove(reservacion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult VerReporteReservacion(string tipo, string buscar = "")
        {
            LocalReport rpt = new LocalReport();
            string mt, enc, f;
            string[] s;
            Warning[] w;


            string ruta = Path.Combine(Server.MapPath("~/Reportes"), "RReservacion.rdlc");
            string deviceInfo = @"<DeviceInfo>
                      <MarginTop>0cm</MarginTop>
                      <MarginLeft>0cm</MarginLeft>
                      <MarginRight>0cm</MarginRight>
                      <MarginBottom>0cm</MarginBottom>
                        <EmbedFonts>None</EmbedFonts>
                    </DeviceInfo>";

            rpt.ReportPath = ruta;

            BDLabTICEntities4 modelo = new BDLabTICEntities4();

            List<VW_reservacion> lista = new List<VW_reservacion>();
            var Reserv = from p in db.VW_reservacion select p;

            if (!string.IsNullOrEmpty(buscar))
            {
                Reserv = Reserv.Where(p => p.nombre.Equals(buscar));
            }
            lista = Reserv.ToList();

            ReportDataSource rds = new ReportDataSource("DsReservacion", lista);
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
