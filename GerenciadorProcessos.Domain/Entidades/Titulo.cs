using GerenciadorProcessos.Domain.Entidades.Geral;
using System;
using System.Runtime.Serialization;

namespace GerenciadorProcessos.Domain.Entidades
{
    public class Titulo : EntidadeBase
    {
        [IgnoreDataMember]
        public Processo Processo { get; set; }
        public int ProcessoId { get; set; }
        public int? Numero { get; set; }
        public string Descricao { get; set; }
        public string TipoTitulo { get; set; }
        public string SituacaoTitulo { get; set; }
        public DateTime? DataPublicacao { get; set; }
        public DateTime? DataVencimento { get; set; }
    }
}
