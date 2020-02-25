namespace GerenciadorProcessos.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inserindoasprocsparainserçãodosprocessos : DbMigration
    {
        public override void Up()
        {
            Sql(@"
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
as
begin
	select distinct case when FASE <> '' then FASE else 'DADO NÃO CADASTRADO' end Fase
	into #tempFases
	from ImpBrasil

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

if object_id('prInsereTiposEventosNovos') > 0
begin
   drop procedure prInsereTiposEventosNovos
   print '<< DROP prInsereTiposEventosNovos >>'
end
GO
create procedure prInsereTiposEventosNovos
/*----------------------------------------------------------------------------------------------------------------------
NOME: prInsereTiposEventosNovos
OBJETIVO: Atualiza o banco de dados com tipos de eventos novos
DATA: 24/02/2020
----------------------------------------------------------------------------------------------------------------------*/
as
begin
	select distinct case when ULT_EVENTO <> 'DADO NÃO CADASTRADO' then substring(ULT_EVENTO, 0, charindex(' - ', ULT_EVENTO)) else null end codEvento,
			case when ULT_EVENTO <> 'DADO NÃO CADASTRADO' then ltrim(rtrim(replace(replace(replace(substring(ULT_EVENTO, 2 + charindex('- ', ULT_EVENTO, 2), LEN(substring(ULT_EVENTO, 2 + charindex('- ', ULT_EVENTO, 2), 10000)) - 14), ' ', '<>'),'><', ''),'<>', ' ')))
				else 'DADO NÃO CADASTRADO' end evento
	into #tempTiposEventos
	from ImpBrasil

	insert into TipoEvento(CodEvento, Nome)
	select codEvento, evento
	from #tempTiposEventos tte
	where not exists(
		select 3
		from TipoEvento te
		where tte.evento = te.Nome
	)

	drop table #tempTiposEventos
end

GO

if object_id('prInsereTiposEventosNovos') > 0
begin
   print '<< CREATE prInsereTiposEventosNovos >>'
end
GO
");
        }
        
        public override void Down()
        {
        }
    }
}
