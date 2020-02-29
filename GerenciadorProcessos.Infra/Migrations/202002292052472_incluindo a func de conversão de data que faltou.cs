namespace GerenciadorProcessos.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class incluindoafuncdeconversãodedataquefaltou : DbMigration
    {
        public override void Up()
        {
            Sql(@"
if object_id('fnConverteData') > 0
begin
   drop function fnConverteData
   print '<< DROP fnConverteData >>'
end
GO

create function fnConverteData
/*----------------------------------------------------------------------------------------------------------------------
NOME: fnConverteData
OBJETIVO: 
DATA: 07/03/2008
----------------------------------------------------------------------------------------------------------------------
select dbo.fnConverteData( '2009-02-29 24:15', 'yyyy-mm-dd hh:nn')
select dbo.fnConverteData( '2009-Feb-29', 'yyyy-mmm-dd')
select dbo.fnConverteData( '21000229','yyyymmdd')
select dbo.fnConverteData( '00000000','yyyymmdd')


select charindex('Y', 'YYYY-MM-DD')

declare @data varchar(20)

set @data = '31-Fev-31'

select dbo.fnConverteData(left(@data,7)+'19'+right(@data,2),'dd-mmm-yyyy',3)
----------------------------------------------------------------------------------------------------------------------*/
(@data_string varchar(100),
 @formato varchar(100)
)  
returns smalldatetime
with encryption
as  
begin  
    declare @tamanho_mes smallint
    declare @pos_ano tinyint, @pos_mes tinyint, @pos_dia tinyint
    declare @pos_hora tinyint, @pos_minuto tinyint
    declare @ano smallint, @mes smallint, @dia smallint
    declare @hora smallint, @minuto smallint
    declare @nome_mes varchar(10)
    declare @posicao smallint
    declare @numero_data bigint
    set @formato = upper(@formato)
    set @tamanho_mes = 0
    set @posicao = 1
    
    set @data_string = ltrim(rtrim(@data_string))

    -- se a data for um numero de dias, ignoro o formato e converto para data diretamente
    /*if @data_string = dbo.fn_so_numeros(@data_string)
    begin
      set @numero_data = CONVERT(bigint,@data_string)
      if @numero_data < 100000
         return convert(smalldatetime,@numero_data)-2 -- subtraio 2 pq o Excel faz diferente
    end*/
    
    while @posicao <= LEN(@formato)
    begin
      if SUBSTRING(@formato,@posicao,1) = 'M'
         set @tamanho_mes= @tamanho_mes + 1
      set @posicao = @posicao+1
    end

    set @pos_ano = charindex('Y', @formato)
    set @pos_mes = charindex('M', @formato)
    set @pos_dia = charindex('D', @formato)
    set @pos_hora = charindex('H', @formato)
    set @pos_minuto = charindex('N', @formato)

    set @ano = convert(smallint,substring(@data_string,@pos_ano,4))
    if @tamanho_mes = 2
      set @mes = convert(smallint,substring(@data_string,@pos_mes,@tamanho_mes))
    else
    begin
      set @nome_mes = lower(substring(@data_string,@pos_mes,@tamanho_mes))
      set @mes = 
         case @nome_mes
            when 'jan' then 1
            when 'fev' then 2 
            when 'feb' then 2 
            when 'mar' then 3
            when 'abr' then 4
            when 'apr' then 4
            when 'mai' then 5
            when 'may' then 5
            when 'jun' then 6
            when 'jul' then 7
            when 'ago' then 8
            when 'aug' then 8
            when 'set' then 9
            when 'sep' then 9
            when 'out' then 10
            when 'oct' then 10
            when 'nov' then 11
            when 'dez' then 12
            when 'dec' then 12
         end
    end
    set @dia = convert(smallint,substring(@data_string,@pos_dia,2))

    if @pos_hora > 0
      set @hora = convert(smallint,substring(@data_string,@pos_hora,2))
    else
      set @hora = 0

    if @pos_minuto > 0
      set @minuto = convert(smallint,substring(@data_string,@pos_minuto,2))
    else
      set @minuto = 0

    if @ano < 1900
        return null
        
    if @ano > 2076
       set @ano = 2076

    if @dia = 0
        set @dia = 1

    if @mes = 0
        set @mes = 1
    if @mes > 12
        set @mes = 12

    if @mes = 2 and @dia > 28 and (@ano % 4 <> 0 or (@ano % 100 = 0 and @ano % 400 <> 0))
        set @dia = 28
    if @mes = 2 and @dia > 29 and (@ano % 4 = 0 and (@ano % 100 <> 0 or @ano % 400 = 0))
        set @dia = 29

    if @mes in (1,3,5,7,8,10,12) and @dia > 31
        set @dia = 31

    if @mes in (4,6,9,11) and @dia > 30
        set @dia = 30

    if @hora > 23
        set @hora = 23

    if @minuto > 59
        set @hora = 59

    return convert(smalldatetime, convert(varchar,@ano)+'-'+
                                  convert(varchar,@mes)+'-'+
                                  convert(varchar,@dia)+' '+
                                  convert(varchar,@hora)+':'+
                                  convert(varchar,@minuto)
                  )

end  
GO

if object_id('fnConverteData') > 0
begin
   print '<< CREATE fnConverteData >>'
end
GO
            ");
        }
        
        public override void Down()
        {
        }
    }
}
