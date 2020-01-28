using System.Data.Entity;

namespace GerenciadorProcessos.Infra.Contexto
{
    public class Contexto : DbContext
    {
        public Contexto() : base("GerenciadorProcessos") { }
    }
}
