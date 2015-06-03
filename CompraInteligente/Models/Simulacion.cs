using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompraInteligente.Models
{
    class Simulacion
    {
        #region Colecciones 
        public enum Moneda
        {
            Dolares, Soles
        }
        public enum SeguroDesgravamen
        {
            Endosado, Individual
        }
        public enum Geografia
        {
            Lima, Provincia
        }
        public enum SeguroVehicular
        {
            Endosado //disponible para agregar más
        }
        public enum Credito
        {
            Ligero, Comercial //disponible para agregar más
        }
        #endregion

        private int DiasAnho = 365;
        public int id { get; set; }
        #region declaracion_De_Variables_Explicitas
        [Required]
        public int plazo { get; set; } //valores 24 o 36
        [Required]
        public Moneda tipoMoneda { get; set; }
        [Required]
        public DateTime fechaDesenbolso { get; set; }
        [Required]
        public int diaDePago { get; set; }
        [Required]
        public Credito tipoDeCredito { get; set; }
        public float tea { get; private set; }
        [Required]
        public bool informacionPeriodica { get; set; } //1=si, 0 = no
        [Required]
        public SeguroDesgravamen tipoDeSeguroDesgravamen { get; set; }
        public float tasaMensualSeguroDesgravamen { get; private set; }
        public float portes { get; private set; }
        [Required]
        public int cuotasPorAnho { get; set; }
        [Required]
        public double valorDelBien { get; set; }
        [Required]
        public Geografia geografia { get; set; }
        [Required]
        public SeguroVehicular tipoSeguroVehicular { get; set; }
        public float tasaSeguroVehicular { get; private set; }
        public float tipoDeCambio { get; private set; }
        [Required]
        public double cuotaInicial_num { get; set; }
        public float cuotaInicial_por { get; private set; }
        public double montoDelPrestamo_num { get; private set; }
        public double ultimaCuota_num { get; private set; }
        public float montoDelPrestamo_por { get; private set; }
        public float ultimaCuota_por { get; private set; }
        public float tcea { get; private set; }
        #endregion
        #region Declaración_de_variables_implicias
        public float tna { get; private set; }
        public float tasaAnualSeguroDesgravamen { get; private set; }

        #endregion

        #region Metodos_Básicos
        public Simulacion()
        {
            plazo = 1;
            tipoMoneda = Moneda.Soles;
            fechaDesenbolso = DateTime.Now;
            diaDePago = 1;
            tipoDeCredito = Credito.Ligero;
            //if (tipoDeCredito == Credito.Ligero)
            //{
            //    if (tipoMoneda==Moneda.Soles)
            //    {
            tea = 0.1749f;
            //    }
            //    else
            //    {
            //        tea = 0.1649f;
            //    }

            //}
            //else
            //{
            //    tea = 0.2149f;
            //}
            informacionPeriodica = true;
            tipoDeSeguroDesgravamen = SeguroDesgravamen.Endosado;
            tasaMensualSeguroDesgravamen = 0;
            //if (informacionPeriodica)
            //{
            portes = 10;
            //}
            //else
            //{
            //    portes = 0;
            //}
            cuotasPorAnho = 12;
            valorDelBien = 0;
            geografia = Geografia.Lima;
            tipoSeguroVehicular = SeguroVehicular.Endosado;
            //if (tipoSeguroVehicular == SeguroVehicular.Endosado)
            //{
            tasaSeguroVehicular = 0;
            //}
            tipoDeCambio = 3.15f;
            cuotaInicial_num = 0;
            cuotaInicial_por = 0;
            montoDelPrestamo_num = 0;
            ultimaCuota_num = 0;
            montoDelPrestamo_por = 0;
            ultimaCuota_por = 0.35f;
            tcea = 0;
        }


        public float calcularTEA()
        {
            if (tipoDeCredito == Credito.Ligero)
            {
                if (tipoMoneda == Moneda.Soles)
                {
                    tea = 0.1749f;
                }
                else
                {
                    tea = 0.1649f;
                }

            }
            else
            {
                tea = 0.2149f;
            }
            return tea;
        }
        public void calcularPortes()
        {
            if (informacionPeriodica)
            {
                if (tipoMoneda == Moneda.Soles)
                {
                    portes = 10;
                }
                else
                {
                    portes = 3.80f;
                }
            }
            else
            {
                portes = 0;
            }
        }
        public float calcularTasaMensualSeguroDesgravamen()
        {
            if (tipoDeSeguroDesgravamen == SeguroDesgravamen.Individual)
            {
                tasaMensualSeguroDesgravamen = 0.0005f;

            }
            else
            {
                tasaMensualSeguroDesgravamen = 0;
            }
            return tasaMensualSeguroDesgravamen;
        }
        public float calcularTasaSeguroVehicular()
        {
            if (tipoSeguroVehicular == SeguroVehicular.Endosado)
            {
                tasaSeguroVehicular = 0;
            }
            return tasaSeguroVehicular;
        }
        public float calcularCuotaInicial_por()
        {
            cuotaInicial_por = (float)Math.Round((cuotaInicial_num / valorDelBien),2);

            return cuotaInicial_por;
        }
        public double calcularMontoDelPrestamo_num()
        {
            montoDelPrestamo_num = valorDelBien - cuotaInicial_num;
            return montoDelPrestamo_num;

        }
        public double calcularUltimaCuota_num()
        {
            ultimaCuota_num = valorDelBien * ultimaCuota_por;
            return ultimaCuota_num;
        }
        public float calcularMontoDelPrestamo_por()
        {
            montoDelPrestamo_por = (float)Math.Round(montoDelPrestamo_num / valorDelBien,2);
            return montoDelPrestamo_por;
        }
        public void calcularUltimaCuota_por()
        {
            ultimaCuota_por = 0.35f;
        }
        public void calcularTCEA()
        {

        }
        #endregion

        #region Metodos_Avanzados
        public float calcularTNA()
        {
            tna = (float)(Math.Pow(1 + tea, 1 / 12) - 1)*12*DiasAnho/360;
            return tna;
        }
        public float calcularTasaAjustadaAlPlazo(int DiasMes)
        {
            return tna * DiasMes/DiasAnho;
        }
        public float calcularTasaSeguroAnualDesgravamen()
        {
            tasaAnualSeguroDesgravamen = tasaMensualSeguroDesgravamen * 12;
            return tasaAnualSeguroDesgravamen;
        }
        public float calcularTasaAjustadaSeguroDesgravamen(int DiasMes)
        {
            return tasaAnualSeguroDesgravamen * DiasMes/ DiasAnho;
            
        }
        public double calcularInteres(double Monto, int DiasMes)
        {
            return Monto * calcularTasaAjustadaAlPlazo(DiasMes);
        }
        public double calcularMontoSeguoDesgravamen(double Monto, int DiasMes)
        {
            return Monto * calcularTasaAjustadaSeguroDesgravamen(DiasMes);
        }
        public float calcularTasaAjustadaSeguroVehicular(int DiasMes)
        {
            return tasaSeguroVehicular * DiasMes / DiasAnho;
        }
        public double calcularMontoSeguroVehicularMensual(double Monto, int DiasMes)
        {
            return Monto * calcularTasaAjustadaSeguroVehicular(DiasMes);
        }

        public double calcularCuotaMensual(int Monto,int DiaMes)
        {
            return Monto*(calcularTasaAjustadaAlPlazo(DiaMes)/Math.Pow(1-(1+calcularTasaAjustadaAlPlazo(DiaMes)),-plazo));
        }
        public double calcularAmortizacion(int Monto, int DiaMes)
        {
            return calcularCuotaMensual(Monto, DiaMes) - calcularInteres(Monto, DiaMes);
        }
        #endregion
    }
}
