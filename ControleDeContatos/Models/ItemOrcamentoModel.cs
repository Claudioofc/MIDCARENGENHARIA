using System;
using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class ItemOrcamentoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite a descrição do item")]
        [StringLength(200, ErrorMessage = "A descrição deve ter no máximo 200 caracteres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Digite a quantidade")]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "Digite o valor unitário")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
        public decimal ValorUnitario { get; set; }

        [Required(ErrorMessage = "Digite o valor total")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
        public decimal ValorTotal { get; set; }

        [StringLength(100, ErrorMessage = "O tipo deve ter no máximo 100 caracteres")]
        public string Tipo { get; set; } // Serviço, Peça, Material

        [StringLength(200, ErrorMessage = "As observações devem ter no máximo 200 caracteres")]
        public string Observacoes { get; set; }

        public bool Ativo { get; set; } = true;

        // Relacionamento com Orçamento
        [Required]
        public int OrcamentoId { get; set; }
        public virtual OrcamentoModel Orcamento { get; set; }
    }
}
