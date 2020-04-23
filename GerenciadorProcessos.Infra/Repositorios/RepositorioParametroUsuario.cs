using System;
using System.Linq;
using System.Linq.Expressions;
using GerenciadorProcessos.Domain.Entidades;
using GerenciadorProcessos.Infra.Repositorios.Geral;

namespace GerenciadorProcessos.Infra.Repositorios
{
    public class RepositorioParametroUsuario : RepositorioPadrao<ParametroUsuario>
    {
        public override IQueryable<ParametroUsuario> Listar()
        {
            return base.Listar().Where(pu => pu.UsuarioId == usuarioId);
        }
        public override IQueryable<ParametroUsuario> Listar(Expression<Func<ParametroUsuario, bool>> predicate)
        {
            return base.Listar(predicate).Where(pu => pu.UsuarioId == usuarioId);
        }
        public ParametroUsuario BuscarParametro ()
        {
            return Listar().FirstOrDefault();
        }
    }
}
