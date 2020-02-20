namespace GerenciadorProcessos.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class arualizandooeventocomosdadosdepontuação : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Evento", "PBAS", c => c.Int());
            AddColumn("dbo.Evento", "PCORR", c => c.Int());
            AddColumn("dbo.Evento", "EVINC", c => c.Int());
            AddColumn("dbo.Evento", "PEVINC", c => c.Int());
            AddColumn("dbo.Evento", "PEVENTO", c => c.Int());
            AddColumn("dbo.Evento", "PONTUACAO", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Evento", "PONTUACAO");
            DropColumn("dbo.Evento", "PEVENTO");
            DropColumn("dbo.Evento", "PEVINC");
            DropColumn("dbo.Evento", "EVINC");
            DropColumn("dbo.Evento", "PCORR");
            DropColumn("dbo.Evento", "PBAS");
        }
    }
}
