if object_id('prInsereProcessosNovos') > 0
begin
   drop procedure prInsereProcessosNovos
   print '<< DROP prInsereProcessosNovos >>'
end
GO
create procedure prInsereProcessosNovos
/*----------------------------------------------------------------------------------------------------------------------  
NOME: prInsereProcessosNovos  
OBJETIVO: Atualiza o banco de dados com os novos processos  
DATA: 16/02/2020
----------------------------------------------------------------------------------------------------------------------*/  
as
begin
	select 1
end

GO

if object_id('prInsereProcessosNovos') > 0
begin
   print '<< CREATE prInsereProcessosNovos >>'
end
GO
