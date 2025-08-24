using ControleDeContatos.Models;
using System.Collections.Generic;

namespace ControleDeContatos.Repositorio
{
    public interface IVeiculoRepositorio
    {
        VeiculoModel ListarPorId(int id);
        List<VeiculoModel> BuscarTodos();
        List<VeiculoModel> BuscarAtivos();
        List<VeiculoModel> BuscarPorCliente(int clienteId);
        VeiculoModel Adicionar(VeiculoModel veiculo);
        VeiculoModel Atualizar(VeiculoModel veiculo);
        bool Apagar(int id);
        List<VeiculoModel> BuscarPorPlaca(string placa);
        VeiculoModel BuscarPorPlacaExata(string placa);
        List<VeiculoModel> BuscarPorMarcaModelo(string termo);
    }
}
