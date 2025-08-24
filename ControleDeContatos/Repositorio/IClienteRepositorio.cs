using ControleDeContatos.Models;
using System.Collections.Generic;

namespace ControleDeContatos.Repositorio
{
    public interface IClienteRepositorio
    {
        ClienteModel ListarPorId(int id);
        List<ClienteModel> BuscarTodos();
        List<ClienteModel> BuscarAtivos();
        ClienteModel Adicionar(ClienteModel cliente);
        ClienteModel Atualizar(ClienteModel cliente);
        bool Apagar(int id);
        List<ClienteModel> BuscarPorNome(string nome);
        ClienteModel BuscarPorCpfCnpj(string cpfCnpj);
    }
}
