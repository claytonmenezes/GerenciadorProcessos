namespace GerenciadorProcessos.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TipoEventoIdaceitandonull : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Evento", "TipoEventoId", "dbo.TipoEvento");
            DropIndex("dbo.Evento", new[] { "TipoEventoId" });
            AlterColumn("dbo.Evento", "TipoEventoId", c => c.Int());
            CreateIndex("dbo.Evento", "TipoEventoId");
            AddForeignKey("dbo.Evento", "TipoEventoId", "dbo.TipoEvento", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Evento", "TipoEventoId", "dbo.TipoEvento");
            DropIndex("dbo.Evento", new[] { "TipoEventoId" });
            AlterColumn("dbo.Evento", "TipoEventoId", c => c.Int(nullable: false));
            CreateIndex("dbo.Evento", "TipoEventoId");
            AddForeignKey("dbo.Evento", "TipoEventoId", "dbo.TipoEvento", "Id", cascadeDelete: true);
        }
    }
}
