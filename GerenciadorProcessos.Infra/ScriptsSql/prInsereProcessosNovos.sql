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
	create table #temp(
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

	if @importacaoId = 1
	begin
		insert into #temp
		select *
		from ImpBrasil
		where ImportacaoId = @importacaoId
	end
	else
	begin
		insert into #temp
		select *
		from ImpBrasil a
		where ImportacaoId = @importacaoId
		and not exists (
			select 2
			from ImpBrasil b
			where ImportacaoId = (@importacaoId - 1)
			and a.processo = b.PROCESSO
			and a.ULT_EVENTO = b.ULT_EVENTO
			and a.FASE = b.FASE
		)
	end

	with cte as (
	select dbo.fnStrZero(dbo.fnSoNumeros(PROCESSO), 10) PROCESSO, ID, NUMERO, ANO, dbo.fnConverteMoeda(AREA_HA) AREA_HA,
		   case when FASE <> '' then FASE else 'DADO NÃO CADASTRADO' end FASE,
		   case when ULT_EVENTO <> 'DADO NÃO CADASTRADO' then substring(ULT_EVENTO, 0, charindex(' - ', ULT_EVENTO)) else null end COD_EVENTO,
		   case when ULT_EVENTO <> 'DADO NÃO CADASTRADO' then ltrim(rtrim(replace(replace(replace(substring(ULT_EVENTO, 2 + charindex('- ', ULT_EVENTO, 2), LEN(substring(ULT_EVENTO, 2 + charindex('- ', ULT_EVENTO, 2), 10000)) - 14), ' ', '<>'),'><', ''),'<>', ' '))) else 'DADO NÃO CADASTRADO' end EVENTO,
		   case when ULT_EVENTO <> 'DADO NÃO CADASTRADO' then substring(ULT_EVENTO, len(ULT_EVENTO) - 9, 10000) else null end DATA_EVENTO,
		   NOME, SUBS, USO, UF, row_number() over(partition by PROCESSO, Fase, ULT_EVENTO order by ULT_EVENTO desc) linhas
		from #temp
	)
	select *
	into #tempProcessos
	from cte
	where linhas = 1

	select PROCESSO, COD_EVENTO, DATA_EVENTO
	into #tempEventos
	from #tempProcessos

	--update Processo
	--set FaseId = f.Id
	--from Processo p
	--join #tempProcessos tp on p.NumeroProcesso = tp.PROCESSO
	--join Fase f on tp.FASE = f.Nome
	--join Fase f2 on p.FaseId = f2.Id
	--where f.Id <> f2.Id

	insert into Processo(NumeroCadastroEmpresa, NUP, Area, TipoRequerimento, Ativo, Superintendencia, UF, UnidadeProtocolizadora, DataProtocolo, DataPrioridade, FaseId, NumeroProcesso, Atualizar)
	select null, null, null, null, 1, null, null, null, null, null, f.Id, processo, 1
	from #tempProcessos tp
	join Fase f on tp.FASE = f.Nome
	where not exists(
		select 4
		from Processo p
		where tp.processo = p.NumeroProcesso
	)

	--insert into Evento
	--select 
	--from #tempEventos tevt
	--join TipoEvento te on tevt.codEvento = te.CodEvento
	--join proc

	drop table #tempProcessos, #tempEventos, #temp
end

GO

if object_id('prInsereProcessosNovos') > 0
begin
   print '<< CREATE prInsereProcessosNovos >>'
end
GO
