using GerenciadorProcessos.Domain.Entidades;
using GerenciadorProcessos.Infra.Repositorios.Geral;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace GerenciadorProcessos.Infra.Repositorios
{
    public class RepositorioProcesso : RepositorioPadrao<Processo>
    {
        public override IQueryable<Processo> Listar(Expression<Func<Processo, bool>> predicate)
        {
            return base.Listar(predicate)
                .Include("Fase")
                .Include("Eventos")
                .Include("Eventos.TipoEvento")
                .Include("PessoasRelacionadas")
                .Include("Titulos")
                .Include("Substancias")
                .Include("Municipios")
                .Include("CondicoesPropriedadeSolo")
                .Include("ProcessosAssociados")
                .Include("DocumentosProcesso");
        }
        public override IQueryable<Processo> Listar()
        {
            return base.Listar().Include("Fase");
        }
        protected override void PreAlterar(Processo obj)
        {
            base.PreAlterar(obj);
            var eventosBanco = db.Eventos.Where(e => e.ProcessoId == obj.Id);
            var pessoasRelacionadasBanco = db.PessoasRelacionadas.Where(e => e.ProcessoId == obj.Id);
            var titulosBanco = db.Titulos.Where(e => e.ProcessoId == obj.Id);
            var substanciasBanco = db.Substancias.Where(e => e.ProcessoId == obj.Id);
            var municipiosBanco = db.Municipios.Where(e => e.ProcessoId == obj.Id);
            var condicoesPropriedadeSoloBanco = db.CondicoesPropriedadeSolo.Where(e => e.ProcessoId == obj.Id);
            var processosAssociadosBanco = db.ProcessosAssociados.Where(e => e.ProcessoId == obj.Id);
            var documentosProcessoBanco = db.DocumentosProcesso.Where(e => e.ProcessoId == obj.Id);

            obj.Eventos.RemoveAll(e => eventosBanco.Any(eb => e.Data == eb.Data && e.TipoEventoId == eb.TipoEventoId));
            obj.PessoasRelacionadas.RemoveAll(pr => pessoasRelacionadasBanco.Any(prb => pr.CpfCnpj == prb.CpfCnpj && pr.TipoRelacao == prb.TipoRelacao && pr.DataInicio == prb.DataInicio));
            obj.Titulos.RemoveAll(t => titulosBanco.Any(tb => t.Numero == tb.Numero && t.Descricao == tb.Descricao && t.DataPublicacao == tb.DataPublicacao));
            obj.Substancias.RemoveAll(s => substanciasBanco.Any(sb => s.Nome ==sb.Nome && s.TipoUso == sb.TipoUso && s.DataInicio == sb.DataInicio));
            obj.Municipios.RemoveAll(m => municipiosBanco.Any(mb => m.Nome == mb.Nome));
            obj.CondicoesPropriedadeSolo.RemoveAll(cps => condicoesPropriedadeSoloBanco.Any(cpsb => cps.Tipo == cpsb.Tipo));
            obj.ProcessosAssociados.RemoveAll(pa => processosAssociadosBanco.Any(pab => pa.NumeroProcesso == pab.NumeroProcesso));
            obj.DocumentosProcesso.RemoveAll(dp => documentosProcessoBanco.Any(dpb => dp.Descricao == dpb.Descricao && dp.DataProtocolo == dpb.DataProtocolo));
        }
    }
}
