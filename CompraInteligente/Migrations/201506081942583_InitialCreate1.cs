namespace CompraInteligente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Simuladors", "por_CI", c => c.Single(nullable: false));
            AddColumn("dbo.Simuladors", "por_CM", c => c.Single(nullable: false));
            AddColumn("dbo.Simuladors", "por_CF", c => c.Single(nullable: false));
            AddColumn("dbo.Simuladors", "CM", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Simuladors", "CM");
            DropColumn("dbo.Simuladors", "por_CF");
            DropColumn("dbo.Simuladors", "por_CM");
            DropColumn("dbo.Simuladors", "por_CI");
        }
    }
}
