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
