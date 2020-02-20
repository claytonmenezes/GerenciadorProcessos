using GerenciadorProcessos.Domain.Entidades.Geral;

namespace GerenciadorProcessos.Domain.Entidades
{
    public class TipoEvento : EntidadeBase
    {
        public string Nome { get; set; }
        public int? CodEvento { get; set; }
        public int? PBAS { get; set; }
        public int? PCORR { get; set; }
        public int? EVINC { get; set; }
        public int? PEVINC { get; set; }
        public int? PEVENTO { get; set; }
        public int? PONTUACAO { get; set; }
    }
}
