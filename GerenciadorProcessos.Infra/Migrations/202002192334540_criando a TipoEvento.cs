namespace GerenciadorProcessos.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class criandoaTipoEvento : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TipoEvento",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        CodEvento = c.Int(),
                        PBAS = c.Int(),
                        PCORR = c.Int(),
                        EVINC = c.Int(),
                        PEVINC = c.Int(),
                        PEVENTO = c.Int(),
                        PONTUACAO = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Evento", "TipoEventoId", c => c.Int(nullable: false));
            CreateIndex("dbo.Evento", "TipoEventoId");
            AddForeignKey("dbo.Evento", "TipoEventoId", "dbo.TipoEvento", "Id", cascadeDelete: true);
            DropColumn("dbo.Evento", "CodEvento");
            DropColumn("dbo.Evento", "Nome");
            DropColumn("dbo.Evento", "PBAS");
            DropColumn("dbo.Evento", "PCORR");
            DropColumn("dbo.Evento", "EVINC");
            DropColumn("dbo.Evento", "PEVINC");
            DropColumn("dbo.Evento", "PEVENTO");
            DropColumn("dbo.Evento", "PONTUACAO");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Evento", "PONTUACAO", c => c.Int());
            AddColumn("dbo.Evento", "PEVENTO", c => c.Int());
            AddColumn("dbo.Evento", "PEVINC", c => c.Int());
            AddColumn("dbo.Evento", "EVINC", c => c.Int());
            AddColumn("dbo.Evento", "PCORR", c => c.Int());
            AddColumn("dbo.Evento", "PBAS", c => c.Int());
            AddColumn("dbo.Evento", "Nome", c => c.String());
            AddColumn("dbo.Evento", "CodEvento", c => c.Int());
            DropForeignKey("dbo.Evento", "TipoEventoId", "dbo.TipoEvento");
            DropIndex("dbo.Evento", new[] { "TipoEventoId" });
            DropColumn("dbo.Evento", "TipoEventoId");
            DropTable("dbo.TipoEvento");
        }
    }
}
