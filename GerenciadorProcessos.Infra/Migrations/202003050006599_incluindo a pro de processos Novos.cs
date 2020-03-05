namespace GerenciadorProcessos.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class incluindoaprodeprocessosNovos : DbMigration
    {
        public override void Up()
        {
            Sql(@"
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
	SET DATEFORMAT ymd

    set nocount on

	select *
	into #temp
	from ImpBrasil a
	where ImportacaoId = @importacaoId
	and not exists (
		select 2
		from ImpBrasil b
		where ImportacaoId = (@importacaoId - 1)
		and a.processo = b.PROCESSO
		and a.ULT_EVENTO = b.ULT_EVENTO
		and a.FASE = b.FASE
	);

	with cte as (
	select dbo.fnStrZero(dbo.fnSoNumeros(PROCESSO), 10) PROCESSO,
			case when FASE <> '' then FASE else 'DADO NÃO CADASTRADO' end FASE,
			case when ULT_EVENTO <> 'DADO NÃO CADASTRADO' then substring(ULT_EVENTO, 0, charindex(' - ', ULT_EVENTO)) else null end COD_EVENTO,
			case when ULT_EVENTO <> 'DADO NÃO CADASTRADO' then ltrim(rtrim(replace(replace(replace(substring(ULT_EVENTO, 2 + charindex('- ', ULT_EVENTO, 2), LEN(substring(ULT_EVENTO, 2 + charindex('- ', ULT_EVENTO, 2), 10000)) - 14), ' ', '<>'),'><', ''),'<>', ' '))) else 'DADO NÃO CADASTRADO' end EVENTO,
			case when ULT_EVENTO <> 'DADO NÃO CADASTRADO' then substring(ULT_EVENTO, len(ULT_EVENTO) - 9, 10000) else null end DATA_EVENTO,
			row_number() over(partition by PROCESSO, Fase, ULT_EVENTO order by ULT_EVENTO desc) linhas
		from #temp
	)
	select *
	into #tempProcessos
	from cte
	where linhas = 1

	select PROCESSO, COD_EVENTO, DATA_EVENTO
	into #tempEventos
	from #tempProcessos

	update Processo
	set FaseId = f.Id
	from Processo p
	join #tempProcessos tp on p.NumeroProcesso = tp.PROCESSO
	join Fase f on tp.FASE = f.Nome
	where FaseId <> f.Id

	insert into Processo(NumeroCadastroEmpresa, NUP, Area, TipoRequerimento, Ativo, Superintendencia, UF, UnidadeProtocolizadora, DataProtocolo, DataPrioridade, FaseId, NumeroProcesso, Atualizar)
	select null, null, null, null, 1, null, null, null, null, null, null, processo, 1
	from #tempProcessos tp
	join Fase f on tp.FASE = f.Nome
	where not exists(
		select 4
		from Processo p
		where tp.processo = p.NumeroProcesso
	)
	
	insert into Evento
	select p.Id, te.Id, dbo.fnFormataData(DATA_EVENTO)
	from #tempEventos tevt
	join TipoEvento te on tevt.COD_EVENTO = te.CodEvento
	join Processo p on tevt.PROCESSO = p.NumeroProcesso
	where not exists (
		select *
		from Evento e
		where e.ProcessoId = p.Id
		and te.Id = e.TipoEventoId
		and dbo.fnFormataData(tevt.DATA_EVENTO) = e.Data
	)

	drop table #tempProcessos, #tempEventos, #temp
end

GO

if object_id('prInsereProcessosNovos') > 0
begin
   print '<< CREATE prInsereProcessosNovos >>'
end
GO
            ");
        }
        
        public override void Down()
        {
        }
    }
}
