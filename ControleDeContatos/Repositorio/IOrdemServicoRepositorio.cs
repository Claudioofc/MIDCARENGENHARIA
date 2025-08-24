using ControleDeContatos.Models;
using System;
using System.Collections.Generic;

namespace ControleDeContatos.Repositorio
{
    public interface IOrdemServicoRepositorio
    {
        OrdemServicoModel ListarPorId(int id);
        List<OrdemServicoModel> BuscarTodas();
        List<OrdemServicoModel> BuscarAtivas();
        List<OrdemServicoModel> BuscarPorCliente(int clienteId);
        List<OrdemServicoModel> BuscarPorVeiculo(int veiculoId);
        List<OrdemServicoModel> BuscarPorStatus(string status);
        OrdemServicoModel Adicionar(OrdemServicoModel ordemServico);
        OrdemServicoModel Atualizar(OrdemServicoModel ordemServico);
        bool Apagar(int id);
        List<OrdemServicoModel> BuscarPorNumeroOS(string numeroOS);
        OrdemServicoModel BuscarPorNumeroOSExato(string numeroOS);
        string GerarNumeroOS();
        List<OrdemServicoModel> BuscarPorPeriodo(DateTime dataInicio, DateTime dataFim);
    }
}
