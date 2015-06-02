using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompraInteligente.Models
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
        Ligero , Comercial //disponible para agregar más
    }
    #endregion
    class Simulacion
    {
        #region declaracion_De_Variables
        public int plazo { get; set; }
        public Moneda tipoMoneda { get; set; } 
        public DateTime fechaDesenbolso { get; set; }
        public int diaDePago { get; set; }
        public Credito tipoDeCredito { get; set; }
        public float tea { get; set; }
        public bool informacionPeriodica { get; set; } //1=si, 0 = no
        public SeguroDesgravamen tipoDeSeguroDesgravamen { get; set; }
        public float portes { get; set; }
        public int cuotasPorAnho { get; set; }
        public double valorDelBien { get; set; }
        public Geografia geografia { get; set; }
        public SeguroVehicular tipoSeguroVehicular { get; set; }
        public float tasaSeguroVehicular { get; set; }
        public float tipoDeCambio { get; set; }
        public double coutaInicial_num { get; set; }
        public float coutaInicial_por { get; set; }
        public double montoDelPrestamo_num { get; set; }
        public double ultimaCuota_num { get; set; }
        public float montoDelPrestamo_por { get; set; }
        public float ultimaCuota_por { get; set; }
        public float tcea { get; set; }
        #endregion

        
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
            coutaInicial_num = 0;
            coutaInicial_por = 0;
            montoDelPrestamo_num = 0;
            ultimaCuota_num = 0;
            montoDelPrestamo_por = 0;
            ultimaCuota_por = 0;
            tcea = 0;
        }



    }
}
