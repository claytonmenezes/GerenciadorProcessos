namespace GerenciadorProcessos.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class atualizandoaprocdeinserçãodetipoevento : DbMigration
    {
        public override void Up()
        {
            Sql(@"if object_id('prInsereTiposEventosNovos') > 0
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
(@importacaoId int)as
begin
	select distinct case when ULT_EVENTO <> 'DADO NÃO CADASTRADO' then substring(ULT_EVENTO, 0, charindex(' - ', ULT_EVENTO)) else null end codEvento,
			case when ULT_EVENTO <> 'DADO NÃO CADASTRADO' then ltrim(rtrim(replace(replace(replace(substring(ULT_EVENTO, 2 + charindex('- ', ULT_EVENTO, 2), LEN(substring(ULT_EVENTO, 2 + charindex('- ', ULT_EVENTO, 2), 10000)) - 14), ' ', '<>'),'><', ''),'<>', ' ')))
				end evento
	into #tempTiposEventos
	from ImpBrasil
	where ImportacaoId = @importacaoId
	and ULT_EVENTO <> 'DADO NÃO CADASTRADO'

	insert into TipoEvento(CodEvento, Nome)
	select codEvento, evento
	from #tempTiposEventos tte
	where not exists(
		select 3
		from TipoEvento te
		where tte.evento = te.Nome
		and tte.codEvento = te.CodEvento
	)

	drop table #tempTiposEventos
end

GO

if object_id('prInsereTiposEventosNovos') > 0
begin
   print '<< CREATE prInsereTiposEventosNovos >>'
end
GO");
        }
        
        public override void Down()
        {
        }
    }
}
