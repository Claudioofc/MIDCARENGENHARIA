using System;
using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class VeiculoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite a placa do veículo")]
        [StringLength(10, ErrorMessage = "A placa deve ter no máximo 10 caracteres")]
        public string Placa { get; set; }

        [Required(ErrorMessage = "Digite o modelo do veículo")]
        [StringLength(50, ErrorMessage = "O modelo deve ter no máximo 50 caracteres")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "Digite a marca do veículo")]
        [StringLength(50, ErrorMessage = "A marca deve ter no máximo 50 caracteres")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "Digite o ano do veículo")]
        [Range(1900, 2030, ErrorMessage = "O ano deve estar entre 1900 e 2030")]
        public int Ano { get; set; }

        [StringLength(20, ErrorMessage = "A cor deve ter no máximo 20 caracteres")]
        public string Cor { get; set; }

        [StringLength(20, ErrorMessage = "O chassi deve ter no máximo 20 caracteres")]
        public string Chassi { get; set; }

        [StringLength(20, ErrorMessage = "O motor deve ter no máximo 20 caracteres")]
        public string Motor { get; set; }

        [StringLength(20, ErrorMessage = "O combustível deve ter no máximo 20 caracteres")]
        public string Combustivel { get; set; }

        [StringLength(20, ErrorMessage = "O RENAVAM deve ter no máximo 20 caracteres")]
        public string Renavam { get; set; }

        [StringLength(10, ErrorMessage = "A quilometragem deve ter no máximo 10 caracteres")]
        public string Quilometragem { get; set; }

        [StringLength(500, ErrorMessage = "As observações devem ter no máximo 500 caracteres")]
        public string Observacoes { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.Now;

        public bool Ativo { get; set; } = true;

        // Relacionamento com Cliente
        [Required(ErrorMessage = "Selecione o cliente")]
        public int ClienteId { get; set; }
        public virtual ClienteModel Cliente { get; set; }

        // Propriedades de navegação para relacionamentos futuros
        // public virtual ICollection<OrdemServicoModel> OrdensServico { get; set; }
    }
}
