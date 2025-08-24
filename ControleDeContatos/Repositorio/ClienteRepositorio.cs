using ControleDeContatos.Models;
using ControleDeContatos.Models.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeContatos.Repositorio
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly BancoContext _bancoContext;

        public ClienteRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public ClienteModel ListarPorId(int id)
        {
            return _bancoContext.Clientes.FirstOrDefault(x => x.Id == id);
        }

        public List<ClienteModel> BuscarTodos()
        {
            return _bancoContext.Clientes.ToList();
        }

        public List<ClienteModel> BuscarAtivos()
        {
            return _bancoContext.Clientes.Where(x => x.Ativo).ToList();
        }

        public ClienteModel Adicionar(ClienteModel cliente)
        {
            _bancoContext.Clientes.Add(cliente);
            _bancoContext.SaveChanges();
            return cliente;
        }

        public ClienteModel Atualizar(ClienteModel cliente)
        {
            ClienteModel clienteDB = ListarPorId(cliente.Id);

            if (clienteDB == null) throw new System.Exception("Houve um erro na atualização do cliente!");

            clienteDB.Nome = cliente.Nome;
            clienteDB.CpfCnpj = cliente.CpfCnpj;
            clienteDB.Telefone = cliente.Telefone;
            clienteDB.Email = cliente.Email;
            clienteDB.Endereco = cliente.Endereco;
            clienteDB.Cidade = cliente.Cidade;
            clienteDB.Estado = cliente.Estado;
            clienteDB.Cep = cliente.Cep;
            clienteDB.Observacoes = cliente.Observacoes;
            clienteDB.Ativo = cliente.Ativo;

            _bancoContext.Clientes.Update(clienteDB);
            _bancoContext.SaveChanges();

            return clienteDB;
        }

        public bool Apagar(int id)
        {
            ClienteModel clienteDB = ListarPorId(id);

            if (clienteDB == null) throw new System.Exception("Houve um erro na exclusão do cliente!");

            _bancoContext.Clientes.Remove(clienteDB);
            _bancoContext.SaveChanges();

            return true;
        }

        public List<ClienteModel> BuscarPorNome(string nome)
        {
            return _bancoContext.Clientes
                .Where(x => x.Nome.Contains(nome) && x.Ativo)
                .ToList();
        }

        public ClienteModel BuscarPorCpfCnpj(string cpfCnpj)
        {
            return _bancoContext.Clientes
                .FirstOrDefault(x => x.CpfCnpj == cpfCnpj);
        }
    }
}
