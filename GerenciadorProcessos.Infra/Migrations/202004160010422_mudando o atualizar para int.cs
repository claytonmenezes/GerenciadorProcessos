namespace GerenciadorProcessos.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mudandooatualizarparaint : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Processo", "Atualizar", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Processo", "Atualizar", c => c.Boolean(nullable: false));
        }
    }
}
