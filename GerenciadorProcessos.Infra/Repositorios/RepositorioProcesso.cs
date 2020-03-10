using GerenciadorProcessos.Domain.Entidades;
using GerenciadorProcessos.Infra.Repositorios.Geral;
using System.Linq;

namespace GerenciadorProcessos.Infra.Repositorios
{
    public class RepositorioProcesso : RepositorioPadrao<Processo>
    {
        public virtual IQueryable ListarAtualizar()
        {
            return Listar(p => p.Atualizar);
        }
    }
}
