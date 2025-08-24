using System.Threading.Tasks;

namespace ControleDeContatos.Services
{
    public interface IDetranService
    {
        Task<VeiculoInfo> ConsultarVeiculoPorPlacaAsync(string placa);
    }

    public class VeiculoInfo
    {
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public int Ano { get; set; }
        public string Cor { get; set; }
        public string Chassi { get; set; }
        public string Motor { get; set; }
        public string Combustivel { get; set; }
        public string Renavam { get; set; }
        public string Quilometragem { get; set; }
        public string Proprietario { get; set; }
        public string Situacao { get; set; }
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
    }
}
