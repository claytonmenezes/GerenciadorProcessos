using GerenciadorProcessos.Domain.Entidades;
using GerenciadorProcessos.Infra.Repositorios.Geral;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Data.Entity;

namespace GerenciadorProcessos.Infra.Repositorios
{
    public class RepositorioProcesso : RepositorioPadrao<Processo>
    {
        public override IQueryable<Processo> Listar(Expression<Func<Processo, bool>> predicate)
        {
            return base.Listar(predicate)
                .Include("Fase")
                .Include("Eventos")
                .Include("PessoasRelacionadas")
                .Include("Titulos")
                .Include("Substancias")
                .Include("Municipios")
                .Include("CondicoesPropriedadeSolo")
                .Include("ProcessosAssociados")
                .Include("DocumentosProcesso");
        }
    }
}
