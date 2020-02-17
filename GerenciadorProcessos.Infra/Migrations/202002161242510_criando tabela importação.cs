namespace GerenciadorProcessos.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class criandotabelaimportação : DbMigration
    {
        public override void Up()
        {
            Sql(@"
CREATE TABLE ImpBrasil(
    ImportacaoId int,
	PROCESSO varchar(max),
    ID varchar(max),
    NUMERO varchar(max),
    ANO varchar(max),
    AREA_HA varchar(max),
    FASE varchar(max),
    ULT_EVENTO varchar(max),
    NOME varchar(max),
    SUBS varchar(max),
    USO varchar(max),
    UF varchar(max)
)
                ");
        }
        
        public override void Down()
        {
            Sql(@"
if object_id('ImpBrasil') > 0
begin
   drop table ImpBrasil
end
                ");
        }
    }
}
