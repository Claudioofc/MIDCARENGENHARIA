using ControleDeContatos.Models;
using ControleDeContatos.Models.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeContatos.Repositorio
{
    public class VeiculoRepositorio : IVeiculoRepositorio
    {
        private readonly BancoContext _bancoContext;

        public VeiculoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public VeiculoModel ListarPorId(int id)
        {
            return _bancoContext.Veiculos
                .Include(v => v.Cliente)
                .FirstOrDefault(x => x.Id == id);
        }

        public List<VeiculoModel> BuscarTodos()
        {
            return _bancoContext.Veiculos
                .Include(v => v.Cliente)
                .ToList();
        }

        public List<VeiculoModel> BuscarAtivos()
        {
            return _bancoContext.Veiculos
                .Include(v => v.Cliente)
                .Where(x => x.Ativo)
                .ToList();
        }

        public List<VeiculoModel> BuscarPorCliente(int clienteId)
        {
            return _bancoContext.Veiculos
                .Include(v => v.Cliente)
                .Where(x => x.ClienteId == clienteId && x.Ativo)
                .ToList();
        }

        public VeiculoModel Adicionar(VeiculoModel veiculo)
        {
            _bancoContext.Veiculos.Add(veiculo);
            _bancoContext.SaveChanges();
            return veiculo;
        }

        public VeiculoModel Atualizar(VeiculoModel veiculo)
        {
            VeiculoModel veiculoDB = ListarPorId(veiculo.Id);

            if (veiculoDB == null) throw new System.Exception("Houve um erro na atualização do veículo!");

            veiculoDB.Placa = veiculo.Placa;
            veiculoDB.Modelo = veiculo.Modelo;
            veiculoDB.Marca = veiculo.Marca;
            veiculoDB.Ano = veiculo.Ano;
            veiculoDB.Cor = veiculo.Cor;
            veiculoDB.Chassi = veiculo.Chassi;
            veiculoDB.Motor = veiculo.Motor;
            veiculoDB.Quilometragem = veiculo.Quilometragem;
            veiculoDB.Observacoes = veiculo.Observacoes;
            veiculoDB.ClienteId = veiculo.ClienteId;
            veiculoDB.Ativo = veiculo.Ativo;

            _bancoContext.Veiculos.Update(veiculoDB);
            _bancoContext.SaveChanges();

            return veiculoDB;
        }

        public bool Apagar(int id)
        {
            VeiculoModel veiculoDB = ListarPorId(id);

            if (veiculoDB == null) throw new System.Exception("Houve um erro na exclusão do veículo!");

            _bancoContext.Veiculos.Remove(veiculoDB);
            _bancoContext.SaveChanges();

            return true;
        }

        public List<VeiculoModel> BuscarPorPlaca(string placa)
        {
            return _bancoContext.Veiculos
                .Include(v => v.Cliente)
                .Where(x => x.Placa.Contains(placa) && x.Ativo)
                .ToList();
        }

        public VeiculoModel BuscarPorPlacaExata(string placa)
        {
            return _bancoContext.Veiculos
                .Include(v => v.Cliente)
                .FirstOrDefault(x => x.Placa == placa);
        }

        public List<VeiculoModel> BuscarPorMarcaModelo(string termo)
        {
            return _bancoContext.Veiculos
                .Include(v => v.Cliente)
                .Where(x => (x.Marca.Contains(termo) || x.Modelo.Contains(termo)) && x.Ativo)
                .ToList();
        }
    }
}
