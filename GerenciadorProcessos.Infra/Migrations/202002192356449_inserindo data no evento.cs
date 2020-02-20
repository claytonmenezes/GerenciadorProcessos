namespace GerenciadorProcessos.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inserindodatanoevento : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Evento", "Data", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Evento", "Data");
        }
    }
}
