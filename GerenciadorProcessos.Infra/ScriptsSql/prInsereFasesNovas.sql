if object_id('prInsereFasesNovas') > 0
begin
   drop procedure prInsereFasesNovas
   print '<< DROP prInsereFasesNovas >>'
end
GO
create procedure prInsereFasesNovas
/*----------------------------------------------------------------------------------------------------------------------
NOME: prInsereFasesNovas
OBJETIVO: Atualiza o banco de dados com as fases novas
DATA: 24/02/2020
----------------------------------------------------------------------------------------------------------------------*/
(@importacaoId int)as
begin
	select distinct case when FASE <> '' then FASE else 'DADO NÃO CADASTRADO' end Fase
	into #tempFases
	from ImpBrasil
	where ImportacaoId = @importacaoId

	insert into Fase
	select Fase
	from #tempFases tf
	where not exists(
		select 2
		from Fase f
		where tf.Fase = f.Nome
	)

	drop table #tempFases
end

GO

if object_id('prInsereFasesNovas') > 0
begin
   print '<< CREATE prInsereFasesNovas >>'
end
GO
