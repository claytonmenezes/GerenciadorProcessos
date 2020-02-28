using GerenciadorProcessos.Domain.Entidades.Geral;
using System;
using System.Collections.Generic;

namespace GerenciadorProcessos.Domain.Entidades
{
    public class Processo : EntidadeBase
    {
        public string NumeroProcesso { get; set; }
        public string NumeroCadastroEmpresa { get; set; }
        public string NUP { get; set; }
        public float? Area { get; set; }
        public string TipoRequerimento { get; set; }
        public bool? Ativo { get; set; }
        public string Superintendencia { get; set; }
        public string UF { get; set; }
        public string UnidadeProtocolizadora { get; set; }
        public DateTime? DataProtocolo { get; set; }
        public DateTime? DataPrioridade { get; set; }
        public Fase Fase { get; set; }
        public int FaseId { get; set; }
        public List<Evento> Eventos { get; set; }
        public List<PessoaRelacionada> PessoasRelacionadas { get; set; }
        public List<Titulo> Titulos { get; set; }
        public List<Substancia> Substancias { get; set; }
        public List<Municipio> Municipios { get; set; }
        public List<CondicaoPropriedadeSolo> CondicoesPropriedadeSolo { get; set; }
        public List<ProcessoAssociado> ProcessosAssociados { get; set; }
        public List<DocumentoProcesso> DocumentosProcesso { get; set; }
        public bool Atualizar { get; set; }
    }
}
