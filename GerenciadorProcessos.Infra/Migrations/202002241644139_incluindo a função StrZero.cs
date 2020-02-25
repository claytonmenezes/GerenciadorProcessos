namespace GerenciadorProcessos.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class incluindoafunçãoStrZero : DbMigration
    {
        public override void Up()
        {
            Sql(@"
if object_id('fnStrZero') > 0
begin
   drop function fnStrZero
   print '<< DROP fnStrZero >>'
end

GO
create function fnStrZero
/*----------------------------------------------------------------------------------------------------------------------
NOME: fnStrZero
OBJETIVO: Colocar zeros a esquerda
DATA: 24/02/2020
------------------------------------------------------------------------------------------------------------------------
select dbo.fnStrZero(@texto, @tamanho)
----------------------------------------------------------------------------------------------------------------------*/
(@texto varchar(8000), @tamanho int)
returns varchar(8000)
as
begin
    return REPLICATE('0', @tamanho - LEN(@texto)) + RTrim(@texto)
end                 
GO

if object_id('fnStrZero') > 0
begin
   print '<< CREATE fnStrZero >>'
end
GO
            ");
        }
        
        public override void Down()
        {
        }
    }
}
