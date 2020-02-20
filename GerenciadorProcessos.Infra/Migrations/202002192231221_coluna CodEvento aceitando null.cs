namespace GerenciadorProcessos.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class colunaCodEventoaceitandonull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Evento", "CodEvento", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Evento", "CodEvento", c => c.Int(nullable: false));
        }
    }
}
