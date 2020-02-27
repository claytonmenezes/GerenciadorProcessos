namespace GerenciadorProcessos.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class incluindofunçõessónumeroseconvertemoedas : DbMigration
    {
        public override void Up()
        {
            Sql(@"
if object_id('fnSoNumeros') > 0
begin
   drop function fnSoNumeros
   print '<< DROP fnSoNumeros >>'
end

GO
create function fnSoNumeros
/*----------------------------------------------------------------------------------------------------------------------
NOME: fnSoNumeros
OBJETIVO: faz a string retornar somente os números contidos dentro dela
DATA: 10/05/2010
------------------------------------------------------------------------------------------------------------------------
select dbo.fnSoNumeros('421.585.671-04')
select dbo.fnSoNumeros(null)
----------------------------------------------------------------------------------------------------------------------*/
(@texto varchar(8000))
returns varchar(8000)
as
begin
    if @texto is null
        return null

    declare @retorno varchar(8000)

    declare 
        @i int,
        @letra char(1)

    set @i = 1
    set @retorno = ''

    while @i <= len(@texto)
    begin
        set @letra = substring(@texto,@i,1)

        if @letra in ('0','1','2','3','4','5','6','7','8','9')
            set @retorno = @retorno + @letra

        set @i = @i + 1
    end

    return @retorno
end                 
GO

if object_id('fnSoNumeros') > 0
begin
   print '<< CREATE fnSoNumeros >>'
end
GO
if object_id('fnConverteMoeda') > 0
begin
   drop function fnConverteMoeda
   print '<< DROP fnConverteMoeda >>'
end

GO
create function fnConverteMoeda
/*----------------------------------------------------------------------------------------------------------------------
NOME: fn_converte_data
OBJETIVO: 
DATA: 02/07/2013
----------------------------------------------------------------------------------------------------------------------
select dbo.fnConverteMoeda('-2,84E-14')
select dbo.fnConverteMoeda('-2,84E-14')
----------------------------------------------------------------------------------------------------------------------*/

(@valor varchar(50)) 
RETURNS money
begin

    if charindex('E',@valor) > 0
        set @valor = '0'

    declare @pos_ponto int = charindex('.',@valor)
    declare @pos_virgula int = charindex(',',@valor)

    if @pos_virgula > 0 and @pos_ponto = 0 -- só tem virgula
        set @valor = replace(@valor,',','.')
    else
    begin
        if @pos_virgula > 0 and @pos_ponto > 0 -- tem virgula e ponto
            if @pos_ponto < @pos_virgula
            set @valor = replace(replace(@valor,'.',''), ',','.')
            else
            set @valor = replace(@valor,',','')
    end
   
    set @valor = replace(@valor,'R','')
    set @valor = replace(@valor,'$','')
    set @valor = replace(@valor,' ','')
    set @valor = replace(@valor,char(255),'')
   
    return convert(money,@valor)

end
GO

if object_id('fnConverteMoeda') > 0
begin
   print '<< CREATE fnConverteMoeda >>'
end
GO
");
        }
        
        public override void Down()
        {
        }
    }
}
