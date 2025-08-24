using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class ClienteModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do cliente")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o CPF/CNPJ")]
        [StringLength(18, ErrorMessage = "CPF/CNPJ deve ter no máximo 18 caracteres")]
        public string CpfCnpj { get; set; }

        [Required(ErrorMessage = "Digite o telefone")]
        [StringLength(15, ErrorMessage = "Telefone deve ter no máximo 15 caracteres")]
        public string Telefone { get; set; }

        [EmailAddress(ErrorMessage = "Digite um email válido")]
        [StringLength(100, ErrorMessage = "Email deve ter no máximo 100 caracteres")]
        public string Email { get; set; }

        [StringLength(200, ErrorMessage = "Endereço deve ter no máximo 200 caracteres")]
        public string Endereco { get; set; }

        [StringLength(100, ErrorMessage = "Cidade deve ter no máximo 100 caracteres")]
        public string Cidade { get; set; }

        [StringLength(2, ErrorMessage = "Estado deve ter 2 caracteres")]
        public string Estado { get; set; }

        [StringLength(10, ErrorMessage = "CEP deve ter no máximo 10 caracteres")]
        public string Cep { get; set; }

        [StringLength(500, ErrorMessage = "Observações deve ter no máximo 500 caracteres")]
        public string Observacoes { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.Now;

        public bool Ativo { get; set; } = true;

        // Propriedades de navegação para relacionamentos futuros
        // public virtual ICollection<VeiculoModel> Veiculos { get; set; }
        // public virtual ICollection<OrdemServicoModel> OrdensServico { get; set; }
    }
}
