namespace GerenciadorProcessos.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class incluindooexpirasenha : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuario", "ExpiraSenha", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuario", "ExpiraSenha");
        }
    }
}
