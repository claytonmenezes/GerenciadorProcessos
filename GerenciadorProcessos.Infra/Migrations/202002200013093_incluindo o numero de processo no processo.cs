namespace GerenciadorProcessos.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class incluindoonumerodeprocessonoprocesso : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Processo", "NumeroProcesso", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Processo", "NumeroProcesso");
        }
    }
}
