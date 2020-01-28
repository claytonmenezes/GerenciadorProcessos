using GerenciadorProcessos.Domain.Entidades;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace GerenciadorProcessos.Infra.Contexto
{
    public class Contexto : DbContext
    {
        public Contexto() : base("GerenciadorProcessos") { }
        public virtual DbSet<Processo> Processos { get; set; }
        public virtual DbSet<Fase> Fases { get; set; }
        public virtual DbSet<PessoaRelacionada> PessoasRelacionadas { get; set; }
        public virtual DbSet<Titulo> Titulos { get; set; }
        public virtual DbSet<Substancia> Substancias { get; set; }
        public virtual DbSet<Municipio> Municipios { get; set; }
        public virtual DbSet<CondicaoPropriedadeSolo> CondicoesPropriedadeSolo { get; set; }
        public virtual DbSet<ProcessoAssociado> ProcessosAssociado { get; set; }
        public virtual DbSet<DocumentoProcesso> DocumentosProcesso { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
