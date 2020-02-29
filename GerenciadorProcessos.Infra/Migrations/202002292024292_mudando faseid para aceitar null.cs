namespace GerenciadorProcessos.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mudandofaseidparaaceitarnull : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Processo", "FaseId", "dbo.Fase");
            DropIndex("dbo.Processo", new[] { "FaseId" });
            AlterColumn("dbo.Processo", "FaseId", c => c.Int());
            CreateIndex("dbo.Processo", "FaseId");
            AddForeignKey("dbo.Processo", "FaseId", "dbo.Fase", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Processo", "FaseId", "dbo.Fase");
            DropIndex("dbo.Processo", new[] { "FaseId" });
            AlterColumn("dbo.Processo", "FaseId", c => c.Int(nullable: false));
            CreateIndex("dbo.Processo", "FaseId");
            AddForeignKey("dbo.Processo", "FaseId", "dbo.Fase", "Id", cascadeDelete: true);
        }
    }
}
