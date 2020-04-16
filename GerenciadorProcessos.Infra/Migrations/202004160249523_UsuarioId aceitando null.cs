namespace GerenciadorProcessos.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsuarioIdaceitandonull : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParametroUsuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumeroColunaAtualizar = c.Int(nullable: false),
                        UsuarioId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.ParametroSistema", "NumeroColunaAtualizar");
            Sql("insert into ParametroUsuario values(1, null)");
        }

        public override void Down()
        {
            Sql("delete ParametroUsuario");
            AddColumn("dbo.ParametroSistema", "NumeroColunaAtualizar", c => c.Int(nullable: false));
            DropTable("dbo.ParametroUsuario");
        }
    }
}
