using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CompraInteligente.Models;
using System.Diagnostics;

namespace CompraInteligente.Controllers
{
    public class SimuladorsController : Controller
    {
        private const int DiasAnho = 365;
        private SimuladorContext db = new SimuladorContext();

        // GET: Simuladors
        public ActionResult Index()
        {
            return View(db.Simuladores.ToList());
        }

        // GET: Simuladors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Simulador simulador = db.Simuladores.Find(id);
            if (simulador == null)
            {
                return HttpNotFound();
            }
            return View(simulador);
        }

        public ActionResult Cronograma(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Simulador simulador = db.Simuladores.Find(id);
            if (simulador == null)
            {
                return HttpNotFound();
            }
            return View(simulador);
        }

        // GET: Simuladors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Simuladors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SimuladorID,plazo,tipoMoneda,fechaDesenbolso,diaDePago,tipoDeCredito,tea,informacionPeriodica,tipoDeSeguroDesgravamen,tasaMensualSeguroDesgravamen,portes,cuotasPorAnho,valorDelBien,geografia,tipoSeguroVehicular,tasaSeguroVehicular,tipoDeCambio,cuotaInicial_num,montoDelPrestamo_num,ultimaCuota_num,tcea,tna,tasaAnualSeguroDesgravamen")] Simulador simulador)
        {

            calcularValoresImplicitos(simulador);
            if (ModelState.IsValid)
            {
                db.Simuladores.Add(simulador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(simulador);
        }

        // GET: Simuladors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Simulador simulador = db.Simuladores.Find(id);
            if (simulador == null)
            {
                return HttpNotFound();
            }
            return View(simulador);
        }

        // POST: Simuladors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SimuladorID,plazo,tipoMoneda,fechaDesenbolso,diaDePago,tipoDeCredito,tea,informacionPeriodica,tipoDeSeguroDesgravamen,tasaMensualSeguroDesgravamen,portes,cuotasPorAnho,valorDelBien,geografia,tipoSeguroVehicular,tasaSeguroVehicular,tipoDeCambio,cuotaInicial_num,montoDelPrestamo_num,ultimaCuota_num,tcea,tna,tasaAnualSeguroDesgravamen")] Simulador simulador)
        {
            calcularValoresImplicitos(simulador);
            if (ModelState.IsValid)
            {
                db.Entry(simulador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(simulador);
        }

        // GET: Simuladors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Simulador simulador = db.Simuladores.Find(id);
            if (simulador == null)
            {
                return HttpNotFound();
            }
            return View(simulador);
        }

        // POST: Simuladors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Simulador simulador = db.Simuladores.Find(id);
            db.Simuladores.Remove(simulador);
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

        public void calcularValoresImplicitos(Simulador s)
        {
            if (s.tipoMoneda == Moneda.Soles)
            {
                if (s.informacionPeriodica)
                {
                    s.portes = 10;
                }
                if (s.tipoDeCredito == Credito.Ligero)
                {
                    s.tea = 0.1749f;
                }
                else
                {
                    s.tea = 0.2149f;
                }

            }
            else
            {
                if (s.tipoDeCredito == Credito.Ligero)
                {
                    s.tea = 0.1649f;
                }
                else
                {
                    s.tea = 0.2149f;
                }
                if (s.informacionPeriodica)
                {
                    s.portes = 3.8f;
                }
            }


            if (s.tipoDeSeguroDesgravamen == SeguroDesgravamen.Individual)
            {
                s.tasaMensualSeguroDesgravamen = 0.0005f;

            }
            else
            {
                s.tasaMensualSeguroDesgravamen = 0;
            }
            if (s.tipoSeguroVehicular == SeguroVehicular.Endosado)
            {
                s.tasaSeguroVehicular = 0;
            }
            //listo para agregar más
            s.montoDelPrestamo_num = s.valorDelBien - s.cuotaInicial_num;
            s.ultimaCuota_num = s.valorDelBien * 0.35f;//según el BCP la última cuota recomendada es 35%
            s.tna = (float)(Math.Pow(1 + s.tea, 1.0f / 12) - 1) * 12 * DiasAnho / 360;

            s.tasaAnualSeguroDesgravamen = s.tasaMensualSeguroDesgravamen * 12;
        }
        public float calcularTasaAjustadaSeguroDesgravamen(Simulador s,int DiasMes)
        {
            return (float)Math.Round(s.tasaAnualSeguroDesgravamen * DiasMes / DiasAnho,5);
        }

        public float calcularTasaAjustadaSeguroVehicular(Simulador s,int DiasMes)
        {
            return s.tasaSeguroVehicular * DiasMes / DiasAnho;
        }
        public float calcularTasaAjustadaAlPlazo(Simulador s,int DiasMes)
        {
            return s.tna * DiasMes / DiasAnho;
        }
        public double calcularInteres(Simulador s,double Monto, int DiasMes )
        {
            return Monto * calcularTasaAjustadaAlPlazo(s,DiasMes);
        }
        public double calcularCuotaMensual(Simulador s,double Monto, int DiaMes)
        {
            float tasa = calcularTasaAjustadaAlPlazo(s, DiaMes);
            return Monto * (tasa / (1 - Math.Pow(1 + tasa, -s.plazo)));
        }
        public double calcularAmortizacion(double cuotaMensual, double interes)
        {
            return cuotaMensual - interes;
        }
    }
}
