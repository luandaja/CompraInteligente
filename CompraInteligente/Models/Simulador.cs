using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
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
        Ligero, Comercial //disponible para agregar más
    }
    #endregion

    public class Simulador
    {

        private int DiasAnho = 365;
        public int SimuladorID { get; set; }
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
        public float tea { get; set; }
        [Required]
        public bool informacionPeriodica { get; set; } //1=si, 0 = no
        [Required]
        public SeguroDesgravamen tipoDeSeguroDesgravamen { get; set; }
        public float tasaMensualSeguroDesgravamen { get; set; }
        public float portes { get; set; }
        [Required]
        public int cuotasPorAnho { get; set; }
        [Required]
        public double valorDelBien { get; set; }
        [Required]
        public Geografia geografia { get; set; }
        [Required]
        public SeguroVehicular tipoSeguroVehicular { get; set; }
        public float tasaSeguroVehicular { get; set; }
        [Required]
        public float tipoDeCambio { get; set; }
        [Required]
        public double cuotaInicial_num { get; set; }
        public double montoDelPrestamo_num { get; set; }
        public double ultimaCuota_num { get; set; }
        public float tcea { get; set; }
        #endregion
        #region Declaración_de_variables_implicias
        public float tna { get; set; }
        public float tasaAnualSeguroDesgravamen { get; set; }
        public float por_CI { get; set; }
        public float por_CM  {get;set;}
        public float por_CF {get;set;}
        public double CM { get; set; }
        #endregion

    }
    public class SimuladorContext : DbContext
    {
        public DbSet<Simulador> Simuladores { get; set; }
    }
}
