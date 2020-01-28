namespace GerenciadorProcessos.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class incluindoasClasses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CondicaoPropriedadeSolo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProcessoId = c.Int(nullable: false),
                        Tipo = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Processo", t => t.ProcessoId, cascadeDelete: true)
                .Index(t => t.ProcessoId);
            
            CreateTable(
                "dbo.DocumentoProcesso",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProcessoId = c.Int(nullable: false),
                        Descricao = c.String(),
                        DataProtocolo = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Processo", t => t.ProcessoId, cascadeDelete: true)
                .Index(t => t.ProcessoId);
            
            CreateTable(
                "dbo.Fase",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Municipio",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProcessoId = c.Int(nullable: false),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Processo", t => t.ProcessoId, cascadeDelete: true)
                .Index(t => t.ProcessoId);
            
            CreateTable(
                "dbo.PessoaRelacionada",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProcessoId = c.Int(nullable: false),
                        TipoRelacao = c.String(),
                        CpfCnpj = c.String(),
                        Nome = c.String(),
                        ResponsabilidadeRepresentação = c.String(),
                        PrazoArrendamento = c.DateTime(),
                        DataInicio = c.DateTime(),
                        DataFinal = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Processo", t => t.ProcessoId, cascadeDelete: true)
                .Index(t => t.ProcessoId);
            
            CreateTable(
                "dbo.ProcessoAssociado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProcessoId = c.Int(nullable: false),
                        NumeroProcesso = c.String(),
                        Titular = c.String(),
                        TipoAssociacao = c.String(),
                        DataAssociacao = c.DateTime(),
                        DataDesassociacao = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Processo", t => t.ProcessoId, cascadeDelete: true)
                .Index(t => t.ProcessoId);
            
            CreateTable(
                "dbo.Substancia",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProcessoId = c.Int(nullable: false),
                        Nome = c.String(),
                        TipoUso = c.String(),
                        DataInicio = c.DateTime(),
                        DataFinal = c.DateTime(),
                        MotivoEncerramento = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Processo", t => t.ProcessoId, cascadeDelete: true)
                .Index(t => t.ProcessoId);
            
            CreateTable(
                "dbo.Titulo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProcessoId = c.Int(nullable: false),
                        Numero = c.Int(),
                        Descricao = c.String(),
                        TipoTitulo = c.String(),
                        SituacaoTitulo = c.String(),
                        DataPublicacao = c.DateTime(),
                        DataVencimento = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Processo", t => t.ProcessoId, cascadeDelete: true)
                .Index(t => t.ProcessoId);
            
            AddColumn("dbo.Processo", "NumeroCadastroEmpresa", c => c.String());
            AddColumn("dbo.Processo", "NUP", c => c.String());
            AddColumn("dbo.Processo", "Area", c => c.Single());
            AddColumn("dbo.Processo", "TipoRequerimento", c => c.String());
            AddColumn("dbo.Processo", "Ativo", c => c.Boolean());
            AddColumn("dbo.Processo", "Superintendencia", c => c.String());
            AddColumn("dbo.Processo", "UF", c => c.String());
            AddColumn("dbo.Processo", "UnidadeProtocolizadora", c => c.String());
            AddColumn("dbo.Processo", "DataProtocolo", c => c.DateTime());
            AddColumn("dbo.Processo", "DataPrioridade", c => c.DateTime());
            AddColumn("dbo.Processo", "FaseId", c => c.Int(nullable: false));
            CreateIndex("dbo.Processo", "FaseId");
            AddForeignKey("dbo.Processo", "FaseId", "dbo.Fase", "Id", cascadeDelete: true);
            DropColumn("dbo.Processo", "Nome");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Processo", "Nome", c => c.Int(nullable: false));
            DropForeignKey("dbo.Titulo", "ProcessoId", "dbo.Processo");
            DropForeignKey("dbo.Substancia", "ProcessoId", "dbo.Processo");
            DropForeignKey("dbo.ProcessoAssociado", "ProcessoId", "dbo.Processo");
            DropForeignKey("dbo.PessoaRelacionada", "ProcessoId", "dbo.Processo");
            DropForeignKey("dbo.Municipio", "ProcessoId", "dbo.Processo");
            DropForeignKey("dbo.Processo", "FaseId", "dbo.Fase");
            DropForeignKey("dbo.DocumentoProcesso", "ProcessoId", "dbo.Processo");
            DropForeignKey("dbo.CondicaoPropriedadeSolo", "ProcessoId", "dbo.Processo");
            DropIndex("dbo.Titulo", new[] { "ProcessoId" });
            DropIndex("dbo.Substancia", new[] { "ProcessoId" });
            DropIndex("dbo.ProcessoAssociado", new[] { "ProcessoId" });
            DropIndex("dbo.PessoaRelacionada", new[] { "ProcessoId" });
            DropIndex("dbo.Municipio", new[] { "ProcessoId" });
            DropIndex("dbo.DocumentoProcesso", new[] { "ProcessoId" });
            DropIndex("dbo.Processo", new[] { "FaseId" });
            DropIndex("dbo.CondicaoPropriedadeSolo", new[] { "ProcessoId" });
            DropColumn("dbo.Processo", "FaseId");
            DropColumn("dbo.Processo", "DataPrioridade");
            DropColumn("dbo.Processo", "DataProtocolo");
            DropColumn("dbo.Processo", "UnidadeProtocolizadora");
            DropColumn("dbo.Processo", "UF");
            DropColumn("dbo.Processo", "Superintendencia");
            DropColumn("dbo.Processo", "Ativo");
            DropColumn("dbo.Processo", "TipoRequerimento");
            DropColumn("dbo.Processo", "Area");
            DropColumn("dbo.Processo", "NUP");
            DropColumn("dbo.Processo", "NumeroCadastroEmpresa");
            DropTable("dbo.Titulo");
            DropTable("dbo.Substancia");
            DropTable("dbo.ProcessoAssociado");
            DropTable("dbo.PessoaRelacionada");
            DropTable("dbo.Municipio");
            DropTable("dbo.Fase");
            DropTable("dbo.DocumentoProcesso");
            DropTable("dbo.CondicaoPropriedadeSolo");
        }
    }
}
