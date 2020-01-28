using GerenciadorProcessos.Domain.Entidades.Geral;
using System.Runtime.Serialization;

namespace GerenciadorProcessos.Domain.Entidades
{
    public class Municipio : EntidadeBase
    {
        [IgnoreDataMember]
        public Processo Processo { get; set; }
        public int ProcessoId { get; set; }
        public string Nome { get; set; }
    }
}
