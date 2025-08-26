using ControleDeContatos.Models;
using ControleDeContatos.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeContatos.Repositorio
{
    public class OrcamentoRepositorio : IOrcamentoRepositorio
    {
        private readonly BancoContext _bancoContext;

        public OrcamentoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public OrcamentoModel ListarPorId(int id)
        {
            return _bancoContext.Orcamentos
                .Include(o => o.Cliente)
                .Include(o => o.Veiculo)
                .Include(o => o.Itens)
                .FirstOrDefault(x => x.Id == id);
        }

        public List<OrcamentoModel> BuscarTodos()
        {
            return _bancoContext.Orcamentos
                .Include(o => o.Cliente)
                .Include(o => o.Veiculo)
                .OrderByDescending(o => o.DataOrcamento)
                .ToList();
        }

        public List<OrcamentoModel> BuscarAtivos()
        {
            return _bancoContext.Orcamentos
                .Include(o => o.Cliente)
                .Include(o => o.Veiculo)
                .Where(x => x.Ativo)
                .OrderByDescending(o => o.DataOrcamento)
                .ToList();
        }

        public List<OrcamentoModel> BuscarPorCliente(int clienteId)
        {
            return _bancoContext.Orcamentos
                .Include(o => o.Cliente)
                .Include(o => o.Veiculo)
                .Where(x => x.ClienteId == clienteId && x.Ativo)
                .OrderByDescending(o => o.DataOrcamento)
                .ToList();
        }

        public List<OrcamentoModel> BuscarPorVeiculo(int veiculoId)
        {
            return _bancoContext.Orcamentos
                .Include(o => o.Cliente)
                .Include(o => o.Veiculo)
                .Where(x => x.VeiculoId == veiculoId && x.Ativo)
                .OrderByDescending(o => o.DataOrcamento)
                .ToList();
        }

        public List<OrcamentoModel> BuscarPorStatus(string status)
        {
            return _bancoContext.Orcamentos
                .Include(o => o.Cliente)
                .Include(o => o.Veiculo)
                .Where(x => x.Status == status && x.Ativo)
                .OrderByDescending(o => o.DataOrcamento)
                .ToList();
        }

        public OrcamentoModel Adicionar(OrcamentoModel orcamento)
        {
            if (string.IsNullOrEmpty(orcamento.Numero))
            {
                orcamento.Numero = GerarNumeroOrcamento();
            }

            _bancoContext.Orcamentos.Add(orcamento);
            _bancoContext.SaveChanges();
            return orcamento;
        }

        public OrcamentoModel Atualizar(OrcamentoModel orcamento)
        {
            OrcamentoModel orcamentoDB = ListarPorId(orcamento.Id);

            if (orcamentoDB == null) throw new System.Exception("Houve um erro na atualização do orçamento!");

            orcamentoDB.Numero = orcamento.Numero;
            orcamentoDB.DataOrcamento = orcamento.DataOrcamento;
            orcamentoDB.Validade = orcamento.Validade;
            orcamentoDB.Status = orcamento.Status;
            orcamentoDB.Observacoes = orcamento.Observacoes;
            orcamentoDB.ValorTotal = orcamento.ValorTotal;
            orcamentoDB.ClienteId = orcamento.ClienteId;
            orcamentoDB.VeiculoId = orcamento.VeiculoId;
            orcamentoDB.Ativo = orcamento.Ativo;

            _bancoContext.Orcamentos.Update(orcamentoDB);
            _bancoContext.SaveChanges();

            return orcamentoDB;
        }

        public bool Apagar(int id)
        {
            OrcamentoModel orcamentoDB = ListarPorId(id);

            if (orcamentoDB == null) throw new System.Exception("Houve um erro na exclusão do orçamento!");

            _bancoContext.Orcamentos.Remove(orcamentoDB);
            _bancoContext.SaveChanges();

            return true;
        }

        public List<OrcamentoModel> BuscarPorNumeroOrcamento(string numeroOrcamento)
        {
            return _bancoContext.Orcamentos
                .Include(o => o.Cliente)
                .Include(o => o.Veiculo)
                .Where(x => x.Numero.Contains(numeroOrcamento) && x.Ativo)
                .OrderByDescending(o => o.DataOrcamento)
                .ToList();
        }

        public OrcamentoModel BuscarPorNumeroOrcamentoExato(string numeroOrcamento)
        {
            return _bancoContext.Orcamentos
                .Include(o => o.Cliente)
                .Include(o => o.Veiculo)
                .FirstOrDefault(x => x.Numero == numeroOrcamento);
        }

        public string GerarNumeroOrcamento()
        {
            var ultimoOrcamento = _bancoContext.Orcamentos
                .Where(x => x.Numero.StartsWith("ORC"))
                .OrderByDescending(x => x.Numero)
                .FirstOrDefault();

            if (ultimoOrcamento == null)
            {
                return "ORC0001";
            }

            var numero = ultimoOrcamento.Numero.Substring(3);
            if (int.TryParse(numero, out int num))
            {
                return $"ORC{(num + 1):D4}";
            }

            return "ORC0001";
        }

        public List<OrcamentoModel> BuscarPorPeriodo(DateTime dataInicio, DateTime dataFim)
        {
            return _bancoContext.Orcamentos
                .Include(o => o.Cliente)
                .Include(o => o.Veiculo)
                .Where(x => x.DataOrcamento >= dataInicio && x.DataOrcamento <= dataFim && x.Ativo)
                .OrderByDescending(o => o.DataOrcamento)
                .ToList();
        }
    }
}
