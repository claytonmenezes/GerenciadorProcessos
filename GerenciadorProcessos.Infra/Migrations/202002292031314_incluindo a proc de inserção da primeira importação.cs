namespace GerenciadorProcessos.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class incluindoaprocdeinserçãodaprimeiraimportação : DbMigration
    {
        public override void Up()
        {
            Sql(@"
if object_id('prInsereProcessosPrimeiraImportacao') > 0
begin
   drop procedure prInsereProcessosPrimeiraImportacao
   print '<< DROP prInsereProcessosPrimeiraImportacao >>'
end
GO
create procedure prInsereProcessosPrimeiraImportacao
/*----------------------------------------------------------------------------------------------------------------------
NOME: prInsereProcessosPrimeiraImportacao
OBJETIVO: Insere os primeiros processos a serem atualizados
DATA: 29/02/2020
----------------------------------------------------------------------------------------------------------------------*/
(@importacaoId int)as
begin
	if @importacaoId = 1
	begin
		select distinct dbo.fnStrZero(dbo.fnSoNumeros(PROCESSO), 10) PROCESSO
		into #temp
		from ImpBrasil
		where ImportacaoId = @importacaoId

		insert into Processo(NumeroCadastroEmpresa, NUP, Area, TipoRequerimento, Ativo, Superintendencia, UF, UnidadeProtocolizadora, DataProtocolo, DataPrioridade, FaseId, NumeroProcesso, Atualizar)
		select null, null, null, null, 1, null, null, null, null, null, null, PROCESSO, 1
		from #temp tp

		drop table #temp
	end
end

GO

if object_id('prInsereProcessosPrimeiraImportacao') > 0
begin
   print '<< CREATE prInsereProcessosPrimeiraImportacao >>'
end
GO
            ");
        }
        
        public override void Down()
        {
        }
    }
}
