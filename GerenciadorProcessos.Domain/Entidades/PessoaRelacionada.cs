using GerenciadorProcessos.Domain.Entidades.Geral;
using System;
using System.Runtime.Serialization;

namespace GerenciadorProcessos.Domain.Entidades
{
    public class PessoaRelacionada : EntidadeBase
    {
        [IgnoreDataMember]
        public Processo Processo { get; set; }
        public int ProcessoId { get; set; }
        public string TipoRelacao { get; set; }
        public string CpfCnpj { get; set; }
        public string Nome { get; set; }
        public string ResponsabilidadeRepresentação { get; set; }
        public DateTime? PrazoArrendamento { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFinal { get; set; }
    }
}
