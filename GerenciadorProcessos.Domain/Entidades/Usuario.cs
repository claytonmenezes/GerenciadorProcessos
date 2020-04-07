using GerenciadorProcessos.Domain.Entidades.Geral;
using System.Runtime.Serialization;

namespace GerenciadorProcessos.Domain.Entidades
{
    public class Usuario : EntidadeBase
    {
        public bool Ativo { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        [IgnoreDataMember]
        public string Senha { get; set; }
        public bool ExpiraSenha { get; set; }

    }
}
