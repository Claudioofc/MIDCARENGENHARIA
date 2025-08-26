using System;
using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class OrdemServicoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o número da OS")]
        [StringLength(20, ErrorMessage = "O número da OS deve ter no máximo 20 caracteres")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "Digite a data de entrada")]
        public DateTime DataEntrada { get; set; } = DateTime.Now;
        
        public DateTime? DataSaida { get; set; }

        [Required(ErrorMessage = "Selecione o status da OS")]
        public string Status { get; set; } = "Aberta"; // Aberta, Em Andamento, Finalizada, Cancelada

        [StringLength(500, ErrorMessage = "As observações devem ter no máximo 500 caracteres")]
        public string Observacoes { get; set; }

        [Required(ErrorMessage = "Digite o valor total do serviço")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
        public decimal ValorTotal { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public bool Ativo { get; set; } = true;

        // Relacionamentos
        [Required(ErrorMessage = "Selecione o cliente")]
        public int ClienteId { get; set; }
        public virtual ClienteModel Cliente { get; set; }

        [Required(ErrorMessage = "Selecione o veículo")]
        public int VeiculoId { get; set; }
        public virtual VeiculoModel Veiculo { get; set; }

        // Propriedades de navegação para relacionamentos futuros
        // public virtual ICollection<ItemOrdemServicoModel> Itens { get; set; }
    }
}
