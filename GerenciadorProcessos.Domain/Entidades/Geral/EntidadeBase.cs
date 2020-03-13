using System.ComponentModel.DataAnnotations.Schema;

namespace GerenciadorProcessos.Domain.Entidades.Geral
{
    public abstract class EntidadeBase
    {
        public int Id { get; set; }
        [NotMapped]
        public bool Excluir { get; set; }
    }
}
