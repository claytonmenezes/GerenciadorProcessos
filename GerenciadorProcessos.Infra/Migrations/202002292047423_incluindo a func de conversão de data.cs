namespace GerenciadorProcessos.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class incluindoafuncdeconversãodedata : DbMigration
    {
        public override void Up()
        {
            Sql(@"
if object_id('fnFormataData') > 0
begin
   drop function fnFormataData
   print '<< DROP fnFormataData >>'
end

GO
create function fnFormataData
/*----------------------------------------------------------------------------------------------------------------------
NOME: fnFormataData
OBJETIVO: converte varios tipos de data para o formato correto
DATA: 23/04/2018
----------------------------------------------------------------------------------------------------------------------
select dbo.fnFormataData(@data)
----------------------------------------------------------------------------------------------------------------------*/
(@data varchar (20)) returns smalldatetime
as
begin
	declare @data_formatada smalldatetime

	set @data_formatada = case when @data like '[0-9][0-9]/[0-9][0-9]/%' then case when convert(int,substring(@data, charindex('/', @data, 1)+1, 2)) > 12 then dbo.fnConverteData(@data, 'mm/dd/yyyy') else dbo.fnConverteData(@data, 'dd/mm/yyyy') end 
							    when @data like '[0-9]/[0-9][0-9]/%' then case when convert(int,substring(@data, charindex('/', @data, 1)+1, 2)) > 12 then dbo.fnConverteData('0' + @data, 'mm/dd/yyyy') else dbo.fnConverteData('0' + @data, 'dd/mm/yyyy') end 
							    when @data like '[0-9][0-9]/[0-9]/%' then dbo.fnConverteData(left(@data,3) + '0' + right(@data,6), 'dd/mm/yyyy') 
							    when @data like '[0-9]/[0-9]/%' then dbo.fnConverteData('0' + left(@data,2) + '0' + right(@data,6), 'dd/mm/yyyy')
							    when @data like '[0-9][0-9][0-9][0-9]-%' then case when convert(int,substring(@data, charindex('-', @data, 1)+1, 2)) > 12 then dbo.fnConverteData(@data, 'yyyy-dd-mm') else dbo.fnConverteData(@data, 'yyyy-mm-dd') end 
							    when @data like '[0-9][0-9]-[0-9][0-9]-%' then case when convert(int,substring(@data, charindex('-', @data, 1)+1, 2)) > 12 then dbo.fnConverteData(@data, 'mm-dd-yyyy') else dbo.fnConverteData(@data, 'dd-mm-yyyy') end 
							    when @data like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]' then case when convert(int,substring(@data, 5, 2)) > 12 then dbo.fnConverteData(@data, 'yyyyddmm') else dbo.fnConverteData(@data, 'yyyymmdd') end
							    when @data like '[0-9][0-9][0-9][0-9][0-9][0-9]' then dbo.fnConverteData(@data + '01', 'yyyymmdd') end
	return @data_formatada
end
GO

if object_id('fnFormataData') > 0
begin
   print '<< CREATE fnFormataData >>'
end
GO
            ");
        }
        
        public override void Down()
        {
        }
    }
}
