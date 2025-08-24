using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class OrcamentoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o número do orçamento")]
        [StringLength(20, ErrorMessage = "O número do orçamento deve ter no máximo 20 caracteres")]
        public string NumeroOrcamento { get; set; }

        [Required(ErrorMessage = "Digite a descrição do orçamento")]
        [StringLength(1000, ErrorMessage = "A descrição deve ter no máximo 1000 caracteres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Digite o valor total")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
        public decimal ValorTotal { get; set; }

        [Required(ErrorMessage = "Selecione o status do orçamento")]
        public string Status { get; set; } // Pendente, Aprovado, Rejeitado, Convertido em OS

        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime? DataAprovacao { get; set; }
        public DateTime? DataValidade { get; set; }

        [StringLength(500, ErrorMessage = "As observações devem ter no máximo 500 caracteres")]
        public string Observacoes { get; set; }

        public bool Ativo { get; set; } = true;

        // Relacionamentos
        [Required(ErrorMessage = "Selecione o cliente")]
        public int ClienteId { get; set; }
        public virtual ClienteModel Cliente { get; set; }

        [Required(ErrorMessage = "Selecione o veículo")]
        public int VeiculoId { get; set; }
        public virtual VeiculoModel Veiculo { get; set; }

        // Relacionamento com OS (quando convertido)
        public int? OrdemServicoId { get; set; }
        public virtual OrdemServicoModel OrdemServico { get; set; }

        // Itens do orçamento
        public virtual ICollection<ItemOrcamentoModel> Itens { get; set; } = new List<ItemOrcamentoModel>();
    }
}
