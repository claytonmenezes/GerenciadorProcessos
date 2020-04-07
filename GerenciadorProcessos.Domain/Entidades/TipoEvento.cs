using GerenciadorProcessos.Domain.Entidades.Geral;

namespace GerenciadorProcessos.Domain.Entidades
{
    public class TipoEvento : EntidadeBase
    {
        public string Nome { get; set; }
        public int? CodEvento { get; set; }
        public int? Pontuacao { get; set; }
    }
}
