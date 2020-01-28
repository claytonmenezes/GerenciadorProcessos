using GerenciadorProcessos.Domain.Entidades.Geral;
using System;
using System.Runtime.Serialization;

namespace GerenciadorProcessos.Domain.Entidades
{
    public class ProcessoAssociado : EntidadeBase
    {
        [IgnoreDataMember]
        public Processo Processo { get; set; }
        public int ProcessoId { get; set; }
        public string NumeroProcesso { get; set; }
        public string Titular { get; set; }
        public string TipoAssociacao { get; set; }
        public DateTime? DataAssociacao { get; set; }
        public DateTime? DataDesassociacao { get; set; }
    }
}
