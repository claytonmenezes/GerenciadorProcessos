namespace GerenciadorProcessos.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class incluindoointApiCaptchanoparametro : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ParametroUsuario", "ApiCaptcha", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ParametroUsuario", "ApiCaptcha");
        }
    }
}
