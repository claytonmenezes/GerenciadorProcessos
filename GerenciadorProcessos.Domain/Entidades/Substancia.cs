using GerenciadorProcessos.Domain.Entidades.Geral;
using System;
using System.Runtime.Serialization;

namespace GerenciadorProcessos.Domain.Entidades
{
    public class Substancia : EntidadeBase
    {
        [IgnoreDataMember]
        public Processo Processo { get; set; }
        public int ProcessoId { get; set; }
        public string Nome { get; set; }
        public string TipoUso { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFinal { get; set; }
        public string MotivoEncerramento { get; set; }
    }
}
