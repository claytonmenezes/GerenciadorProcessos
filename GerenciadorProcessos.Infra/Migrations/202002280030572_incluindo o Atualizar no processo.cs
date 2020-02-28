namespace GerenciadorProcessos.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class incluindooAtualizarnoprocesso : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Processo", "Atualizar", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Processo", "Atualizar");
        }
    }
}
