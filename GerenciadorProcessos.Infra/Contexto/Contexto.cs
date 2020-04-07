using GerenciadorProcessos.Domain.Entidades;
using GerenciadorProcessos.Infra.Migrations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace GerenciadorProcessos.Infra.Contexto
{
    public class Contexto : DbContext
    {
        public Contexto() : base("GerenciadorProcessos")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Contexto, Configuration>());
        }
        public virtual DbSet<Processo> Processos { get; set; }
        public virtual DbSet<Fase> Fases { get; set; }
        public virtual DbSet<PessoaRelacionada> PessoasRelacionadas { get; set; }
        public virtual DbSet<Titulo> Titulos { get; set; }
        public virtual DbSet<Substancia> Substancias { get; set; }
        public virtual DbSet<Municipio> Municipios { get; set; }
        public virtual DbSet<CondicaoPropriedadeSolo> CondicoesPropriedadeSolo { get; set; }
        public virtual DbSet<ProcessoAssociado> ProcessosAssociados { get; set; }
        public virtual DbSet<DocumentoProcesso> DocumentosProcesso { get; set; }
        public virtual DbSet<Evento> Eventos { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
