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
	select distinct case when FASE <> '' then FASE else 'DADO NÃO CADASTRADO' end Fase
	into #fases
	from ImpBrasil

	select distinct case
						when ULT_EVENTO <> 'DADO NÃO CADASTRADO' then substring(ULT_EVENTO, 0, charindex(' - ', ULT_EVENTO))
						else null
					end codEvento,
					case
						when ULT_EVENTO <> 'DADO NÃO CADASTRADO' then ltrim(rtrim(replace(replace(replace(substring(ULT_EVENTO, 2 + charindex('- ', ULT_EVENTO, 2), LEN(substring(ULT_EVENTO, 2 + charindex('- ', ULT_EVENTO, 2), 10000)) - 14), ' ', '<>'),'><', ''),'<>', ' ')))
						else 'DADO NÃO CADASTRADO'
					end evento
				--, case when ULT_EVENTO <> 'DADO NÃO CADASTRADO' then substring(ULT_EVENTO, len(ULT_EVENTO) - 9, 10000) else null end Data
	into #eventos
	from ImpBrasil
	order by 1

	select PROCESSO, AREA_HA, UF, case when FASE <> '' then FASE else 'DADO NÃO CADASTRADO' end Fase
	into #processos
	from ImpBrasil

	insert into Fase
	select Fase
	from #fases a
	where not exists(
		select 2
		from Fase b
		where a.Fase = b.Nome
	)

	insert into TipoEvento(CodEvento, Nome)
	select codEvento, evento
	from #eventos a
	where not exists(
		select 3
		from TipoEvento b
		where a.evento = b.Nome
	)

	insert into Processo(NumeroCadastroEmpresa, NUP, Area, TipoRequerimento, Ativo, Superintendencia, UF, UnidadeProtocolizadora, DataProtocolo, DataPrioridade, FaseId, NumeroProcesso)
	select null, null, dbo.fnConverteMoeda(AREA_HA), null, 0, null, UF, null, null, null, f.Id, REPLICATE('0', 11 - LEN(PROCESSO)) + RTrim(PROCESSO)
	from #processos tp
	join Fase f on tp.FASE = f.Nome
	where not exists(
		select 8
		from Processo p
		where p.NumeroProcesso = REPLICATE('0', 11 - LEN(tp.PROCESSO)) + RTrim(tp.PROCESSO)
	)

	drop table #fases, #eventos, #processos
end

GO

if object_id('prInsereProcessosNovos') > 0
begin
   print '<< CREATE prInsereProcessosNovos >>'
end
GO
