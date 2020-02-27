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
(@importacaoId int)as
begin
	select ImportacaoId, dbo.fnStrZero(dbo.fnSoNumeros(PROCESSO), 10) PROCESSO, ID, NUMERO, ANO, dbo.fnConverteMoeda(AREA_HA) AREA_HA,
		   case when FASE <> '' then FASE else 'DADO NÃO CADASTRADO' end FASE,
		   case when ULT_EVENTO <> 'DADO NÃO CADASTRADO' then substring(ULT_EVENTO, 0, charindex(' - ', ULT_EVENTO)) else null end COD_EVENTO,
		   case when ULT_EVENTO <> 'DADO NÃO CADASTRADO' then ltrim(rtrim(replace(replace(replace(substring(ULT_EVENTO, 2 + charindex('- ', ULT_EVENTO, 2), LEN(substring(ULT_EVENTO, 2 + charindex('- ', ULT_EVENTO, 2), 10000)) - 14), ' ', '<>'),'><', ''),'<>', ' '))) else 'DADO NÃO CADASTRADO' end EVENTO,
		   case when ULT_EVENTO <> 'DADO NÃO CADASTRADO' then substring(ULT_EVENTO, len(ULT_EVENTO) - 9, 10000) else null end DATA_EVENTO,
		   NOME, SUBS, USO, UF
	into #temp
	from ImpBrasil
	where ImportacaoId = @importacaoId

	with cte as (
		select *, row_number() over(partition by PROCESSO order by DATA_EVENTO desc) linhas
		from #temp
	)
	select *
	into #tempProcessos
	from cte
	where linhas = 1

	select *--dbo.fnStrZero(dbo.fnSoNumeros(PROCESSO), 10) processo, dbo.fnConverteMoeda(AREA_HA) area, UF, case when FASE <> '' then FASE else 'DADO NÃO CADASTRADO' end Fase
	--into #tempProcessos
	from #tempProcessos

	select Processo, case when ULT_EVENTO <> 'DADO NÃO CADASTRADO' then substring(ULT_EVENTO, 0, charindex(' - ', ULT_EVENTO)) else null end codEvento,
			case when ULT_EVENTO <> 'DADO NÃO CADASTRADO' then substring(ULT_EVENTO, len(ULT_EVENTO) - 9, 10000) else null end Data
	into #tempEventos
	from ImpBrasil

	insert into Processo(NumeroCadastroEmpresa, NUP, Area, TipoRequerimento, Ativo, Superintendencia, UF, UnidadeProtocolizadora, DataProtocolo, DataPrioridade, FaseId, NumeroProcesso)
	select null, null, area, null, 0, null, UF, null, null, null, f.Id, processo
	from #tempProcessos tp
	join Fase f on tp.FASE = f.Nome
	where not exists(
		select 4
		from Processo p
		where tp.processo = p.NumeroProcesso
	)

	insert into Evento
	select 
	from #tempEventos tevt
	join TipoEvento te on tevt.codEvento = te.CodEvento
	join proc

	drop table #tempProcessos, #tempEventos
end

GO

if object_id('prInsereProcessosNovos') > 0
begin
   print '<< CREATE prInsereProcessosNovos >>'
end
GO
