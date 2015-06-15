namespace CompraInteligente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Simuladors",
                c => new
                    {
                        SimuladorID = c.Int(nullable: false, identity: true),
                        plazo = c.Int(nullable: false),
                        tipoMoneda = c.Int(nullable: false),
                        fechaDesenbolso = c.DateTime(nullable: false),
                        diaDePago = c.Int(nullable: false),
                        tipoDeCredito = c.Int(nullable: false),
                        tea = c.Single(nullable: false),
                        informacionPeriodica = c.Boolean(nullable: false),
                        tipoDeSeguroDesgravamen = c.Int(nullable: false),
                        tasaMensualSeguroDesgravamen = c.Single(nullable: false),
                        portes = c.Single(nullable: false),
                        cuotasPorAnho = c.Int(nullable: false),
                        valorDelBien = c.Double(nullable: false),
                        geografia = c.Int(nullable: false),
                        tipoSeguroVehicular = c.Int(nullable: false),
                        tasaSeguroVehicular = c.Single(nullable: false),
                        tipoDeCambio = c.Single(nullable: false),
                        cuotaInicial_num = c.Double(nullable: false),
                        montoDelPrestamo_num = c.Double(nullable: false),
                        ultimaCuota_num = c.Double(nullable: false),
                        tcea = c.Single(nullable: false),
                        tna = c.Single(nullable: false),
                        tasaAnualSeguroDesgravamen = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.SimuladorID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Simuladors");
        }
    }
}
