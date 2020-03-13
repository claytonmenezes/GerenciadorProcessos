using GerenciadorProcessos.Domain.Entidades.Geral;
using System;
using System.Runtime.Serialization;

namespace GerenciadorProcessos.Domain.Entidades
{
    public class Evento : EntidadeBase
    {
        [IgnoreDataMember]
        public Processo Processo { get; set; }
        public int ProcessoId { get; set; }
        [IgnoreDataMember]
        public TipoEvento TipoEvento { get; set; }
        public int? TipoEventoId { get; set; }
        public DateTime Data { get; set; }
    }
}
