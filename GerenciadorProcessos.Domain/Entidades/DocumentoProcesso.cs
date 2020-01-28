using GerenciadorProcessos.Domain.Entidades.Geral;
using System;
using System.Runtime.Serialization;

namespace GerenciadorProcessos.Domain.Entidades
{
    public class DocumentoProcesso : EntidadeBase
    {
        [IgnoreDataMember]
        public Processo Processo { get; set; }
        public int ProcessoId { get; set; }
        public string Descricao { get; set; }
        public DateTime? DataProtocolo { get; set; }
    }
}
