using GerenciadorProcessos.Domain.Entidades.Geral;
using System.Runtime.Serialization;

namespace GerenciadorProcessos.Domain.Entidades
{
    public class CondicaoPropriedadeSolo : EntidadeBase
    {
        [IgnoreDataMember]
        public Processo Processo { get; set; }
        public int ProcessoId { get; set; }
        public string Tipo { get; set; }
    }
}
