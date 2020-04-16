namespace GerenciadorProcessos.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class incluindooParametroSistema : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParametroSistema",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AtualizaArquivoBrasil = c.Boolean(nullable: false),
                        NumeroColunaAtualizar = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            Sql("insert into ParametroSistema values(1, 1)");
        }
        
        public override void Down()
        {
            Sql("delete ParametroSistema");
            DropTable("dbo.ParametroSistema");
        }
    }
}
