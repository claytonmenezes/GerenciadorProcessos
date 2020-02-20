namespace GerenciadorProcessos.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fazendoligaçãoentreeventoeprocesso : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Evento", "ProcessoId", c => c.Int(nullable: false));
            CreateIndex("dbo.Evento", "ProcessoId");
            AddForeignKey("dbo.Evento", "ProcessoId", "dbo.Processo", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Evento", "ProcessoId", "dbo.Processo");
            DropIndex("dbo.Evento", new[] { "ProcessoId" });
            DropColumn("dbo.Evento", "ProcessoId");
        }
    }
}
