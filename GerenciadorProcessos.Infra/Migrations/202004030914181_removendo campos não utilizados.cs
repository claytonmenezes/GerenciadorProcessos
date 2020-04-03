namespace GerenciadorProcessos.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removendocamposnãoutilizados : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TipoEvento", "PBAS");
            DropColumn("dbo.TipoEvento", "PCORR");
            DropColumn("dbo.TipoEvento", "EVINC");
            DropColumn("dbo.TipoEvento", "PEVINC");
            DropColumn("dbo.TipoEvento", "PEVENTO");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TipoEvento", "PEVENTO", c => c.Int());
            AddColumn("dbo.TipoEvento", "PEVINC", c => c.Int());
            AddColumn("dbo.TipoEvento", "EVINC", c => c.Int());
            AddColumn("dbo.TipoEvento", "PCORR", c => c.Int());
            AddColumn("dbo.TipoEvento", "PBAS", c => c.Int());
        }
    }
}
