using ControleDeContatos.Models;
using System;
using System.Collections.Generic;

namespace ControleDeContatos.Repositorio
{
    public interface IOrcamentoRepositorio
    {
        OrcamentoModel ListarPorId(int id);
        List<OrcamentoModel> BuscarTodos();
        List<OrcamentoModel> BuscarAtivos();
        List<OrcamentoModel> BuscarPorCliente(int clienteId);
        List<OrcamentoModel> BuscarPorVeiculo(int veiculoId);
        List<OrcamentoModel> BuscarPorStatus(string status);
        OrcamentoModel Adicionar(OrcamentoModel orcamento);
        OrcamentoModel Atualizar(OrcamentoModel orcamento);
        bool Apagar(int id);
        List<OrcamentoModel> BuscarPorNumeroOrcamento(string numeroOrcamento);
        OrcamentoModel BuscarPorNumeroOrcamentoExato(string numeroOrcamento);
        string GerarNumeroOrcamento();
        List<OrcamentoModel> BuscarPorPeriodo(DateTime dataInicio, DateTime dataFim);
        OrcamentoModel AprovarOrcamento(int id);
        OrcamentoModel RejeitarOrcamento(int id);
        OrcamentoModel ConverterEmOS(int id, int ordemServicoId);
        decimal CalcularValorTotal(int orcamentoId);
    }
}
