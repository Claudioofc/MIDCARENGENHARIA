using System;
using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class OrdemServicoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o número da OS")]
        [StringLength(20, ErrorMessage = "O número da OS deve ter no máximo 20 caracteres")]
        public string NumeroOS { get; set; }

        [Required(ErrorMessage = "Digite a descrição do serviço")]
        [StringLength(1000, ErrorMessage = "A descrição deve ter no máximo 1000 caracteres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Digite o valor do serviço")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "Selecione o status da OS")]
        public string Status { get; set; } = "Aberta"; // Aberta, Em Andamento, Finalizada, Cancelada

        public DateTime DataAbertura { get; set; } = DateTime.Now;
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFinalizacao { get; set; }

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

        [Required(ErrorMessage = "Digite o nome do mecânico")]
        [StringLength(100, ErrorMessage = "O nome do mecânico deve ter no máximo 100 caracteres")]
        public string Mecanico { get; set; } = "Milton Diego";

        // Propriedades de navegação para relacionamentos futuros
        // public virtual ICollection<ItemOrdemServicoModel> Itens { get; set; }
    }
}
