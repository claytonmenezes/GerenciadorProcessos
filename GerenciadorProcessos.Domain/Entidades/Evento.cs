using GerenciadorProcessos.Domain.Entidades.Geral;
using System;

namespace GerenciadorProcessos.Domain.Entidades
{
    public class Evento : EntidadeBase
    {
        public string Nome { get; set; }
        public DateTime Data { get; set; }
    }
}
