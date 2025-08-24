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
                .Include(o => o.OrdemServico)
                .Include(o => o.Itens)
                .FirstOrDefault(x => x.Id == id);
        }

        public List<OrcamentoModel> BuscarTodos()
        {
            return _bancoContext.Orcamentos
                .Include(o => o.Cliente)
                .Include(o => o.Veiculo)
                .Include(o => o.OrdemServico)
                .OrderByDescending(o => o.DataCriacao)
                .ToList();
        }

        public List<OrcamentoModel> BuscarAtivos()
        {
            return _bancoContext.Orcamentos
                .Include(o => o.Cliente)
                .Include(o => o.Veiculo)
                .Include(o => o.OrdemServico)
                .Where(x => x.Ativo)
                .OrderByDescending(o => o.DataCriacao)
                .ToList();
        }

        public List<OrcamentoModel> BuscarPorCliente(int clienteId)
        {
            return _bancoContext.Orcamentos
                .Include(o => o.Cliente)
                .Include(o => o.Veiculo)
                .Include(o => o.OrdemServico)
                .Where(x => x.ClienteId == clienteId && x.Ativo)
                .OrderByDescending(o => o.DataCriacao)
                .ToList();
        }

        public List<OrcamentoModel> BuscarPorVeiculo(int veiculoId)
        {
            return _bancoContext.Orcamentos
                .Include(o => o.Cliente)
                .Include(o => o.Veiculo)
                .Include(o => o.OrdemServico)
                .Where(x => x.VeiculoId == veiculoId && x.Ativo)
                .OrderByDescending(o => o.DataCriacao)
                .ToList();
        }

        public List<OrcamentoModel> BuscarPorStatus(string status)
        {
            return _bancoContext.Orcamentos
                .Include(o => o.Cliente)
                .Include(o => o.Veiculo)
                .Include(o => o.OrdemServico)
                .Where(x => x.Status == status && x.Ativo)
                .OrderByDescending(o => o.DataCriacao)
                .ToList();
        }

        public OrcamentoModel Adicionar(OrcamentoModel orcamento)
        {
            if (string.IsNullOrEmpty(orcamento.NumeroOrcamento))
            {
                orcamento.NumeroOrcamento = GerarNumeroOrcamento();
            }

            _bancoContext.Orcamentos.Add(orcamento);
            _bancoContext.SaveChanges();
            return orcamento;
        }

        public OrcamentoModel Atualizar(OrcamentoModel orcamento)
        {
            OrcamentoModel orcamentoDB = ListarPorId(orcamento.Id);

            if (orcamentoDB == null) throw new System.Exception("Houve um erro na atualização do orçamento!");

            orcamentoDB.NumeroOrcamento = orcamento.NumeroOrcamento;
            orcamentoDB.Descricao = orcamento.Descricao;
            orcamentoDB.ValorTotal = orcamento.ValorTotal;
            orcamentoDB.Status = orcamento.Status;
            orcamentoDB.DataAprovacao = orcamento.DataAprovacao;
            orcamentoDB.DataValidade = orcamento.DataValidade;
            orcamentoDB.Observacoes = orcamento.Observacoes;
            orcamentoDB.ClienteId = orcamento.ClienteId;
            orcamentoDB.VeiculoId = orcamento.VeiculoId;
            orcamentoDB.OrdemServicoId = orcamento.OrdemServicoId;
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
                .Include(o => o.OrdemServico)
                .Where(x => x.NumeroOrcamento.Contains(numeroOrcamento) && x.Ativo)
                .OrderByDescending(o => o.DataCriacao)
                .ToList();
        }

        public OrcamentoModel BuscarPorNumeroOrcamentoExato(string numeroOrcamento)
        {
            return _bancoContext.Orcamentos
                .Include(o => o.Cliente)
                .Include(o => o.Veiculo)
                .Include(o => o.OrdemServico)
                .FirstOrDefault(x => x.NumeroOrcamento == numeroOrcamento);
        }

        public string GerarNumeroOrcamento()
        {
            var ultimoOrcamento = _bancoContext.Orcamentos
                .Where(x => x.NumeroOrcamento.StartsWith("ORC"))
                .OrderByDescending(x => x.NumeroOrcamento)
                .FirstOrDefault();

            if (ultimoOrcamento == null)
            {
                return "ORC0001";
            }

            var numero = ultimoOrcamento.NumeroOrcamento.Substring(3);
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
                .Include(o => o.OrdemServico)
                .Where(x => x.DataCriacao >= dataInicio && x.DataCriacao <= dataFim && x.Ativo)
                .OrderByDescending(o => o.DataCriacao)
                .ToList();
        }

        public OrcamentoModel AprovarOrcamento(int id)
        {
            var orcamento = ListarPorId(id);
            if (orcamento == null) throw new System.Exception("Orçamento não encontrado!");

            orcamento.Status = "Aprovado";
            orcamento.DataAprovacao = DateTime.Now;
            orcamento.DataValidade = DateTime.Now.AddDays(30); // Válido por 30 dias

            _bancoContext.Orcamentos.Update(orcamento);
            _bancoContext.SaveChanges();

            return orcamento;
        }

        public OrcamentoModel RejeitarOrcamento(int id)
        {
            var orcamento = ListarPorId(id);
            if (orcamento == null) throw new System.Exception("Orçamento não encontrado!");

            orcamento.Status = "Rejeitado";

            _bancoContext.Orcamentos.Update(orcamento);
            _bancoContext.SaveChanges();

            return orcamento;
        }

        public OrcamentoModel ConverterEmOS(int id, int ordemServicoId)
        {
            var orcamento = ListarPorId(id);
            if (orcamento == null) throw new System.Exception("Orçamento não encontrado!");

            orcamento.Status = "Convertido em OS";
            orcamento.OrdemServicoId = ordemServicoId;

            _bancoContext.Orcamentos.Update(orcamento);
            _bancoContext.SaveChanges();

            return orcamento;
        }

        public decimal CalcularValorTotal(int orcamentoId)
        {
            return _bancoContext.ItensOrcamento
                .Where(i => i.OrcamentoId == orcamentoId && i.Ativo)
                .Sum(i => i.ValorTotal);
        }
    }
}
