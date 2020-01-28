namespace GerenciadorProcessos.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class iniciandoobancodedados : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Processo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Processo");
        }
    }
}
