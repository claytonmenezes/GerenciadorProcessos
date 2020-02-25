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
