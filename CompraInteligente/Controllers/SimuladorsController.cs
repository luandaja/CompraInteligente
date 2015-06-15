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
            s.por_CF = 0.50f;
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
            s.ultimaCuota_num = s.valorDelBien * s.por_CF;//según el BCP la última cuota recomendada es 35%
            s.tna = (float)(Math.Pow(1 + s.tea, 1.0f / 12) - 1) * 12 * DiasAnho / 360;

            s.tasaAnualSeguroDesgravamen = s.tasaMensualSeguroDesgravamen * 12;

            s.por_CI = (float)(s.cuotaInicial_num / s.valorDelBien);
            s.por_CM = 1 - s.por_CI - s.por_CF;
            s.CM = Math.Round(s.por_CM * s.valorDelBien,2);

        }

        public double calcularInteres(Simulador s, int DiasMes, double monto)
        {
            float tasa = s.tna * DiasMes / DiasAnho;
            return monto * tasa;

        }
        public double calcularSeguroDesg(Simulador s, int DiasMes, double monto)
        {
            float tasa = s.tasaAnualSeguroDesgravamen * DiasMes / DiasAnho;
            return monto * tasa;
        }
        public double calcularSeguroVehicular(Simulador s, int DiasMes)
        {
            float tasa = s.tasaSeguroVehicular * DiasMes / DiasAnho;
            return s.valorDelBien * tasa;

        }
        public double calcularCuota(Simulador s, int DiasMes)
        {
            float tasa = (float)calcularInteres(s, DiasMes, 1);
            //double interes = calcularInteres(s, DiasMes, s.CM);
            double cuota = s.CM * (tasa / (1 - Math.Pow(1 + tasa, -36)));
            return cuota;
        }
        public double calcularCuota2(Simulador s, int DiasMes, double CM)
        {
            float tasa = (float)calcularInteres(s, DiasMes, 1);
            //double interes = calcularInteres(s, DiasMes, CM);
            double cuota = CM * (tasa / (1 - Math.Pow(1 + tasa, -36)));
            return cuota;
        }
        public double calcularAmortizacion(Simulador s, int DiasMes, double cuota)
        {

            double interes = calcularInteres(s, DiasMes, s.CM);
            return cuota - interes;
        }
        public double calcularValorPresenteSegunValorFuturo(Simulador s, double valorFuturo)
        {
            /*Convierte un valor futuro a un valor presente*/
            double plazoEnDias = s.plazo * 30;
            double valorPresente = Math.Round(Math.Pow(1 + s.tea, -(plazoEnDias / DiasAnho)) * valorFuturo);
            return valorPresente;
        }
        public double calcularValorFuturo(Simulador s)
        {
            /*Convierte un valor futuro a un valor presente*/
            double plazoEnDias = s.plazo * 30;
            double valorFuturo = Math.Round(Math.Pow(1 + s.tea, (plazoEnDias / DiasAnho))*s.CM);
            return valorFuturo;
        }
    }

}
