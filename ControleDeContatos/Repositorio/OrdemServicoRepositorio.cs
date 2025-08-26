using ControleDeContatos.Models;
using ControleDeContatos.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeContatos.Repositorio
{
    public class OrdemServicoRepositorio : IOrdemServicoRepositorio
    {
        private readonly BancoContext _bancoContext;

        public OrdemServicoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public OrdemServicoModel ListarPorId(int id)
        {
            return _bancoContext.OrdensServico
                .Include(os => os.Cliente)
                .Include(os => os.Veiculo)
                .FirstOrDefault(x => x.Id == id);
        }

        public List<OrdemServicoModel> BuscarTodas()
        {
            return _bancoContext.OrdensServico
                .Include(os => os.Cliente)
                .Include(os => os.Veiculo)
                .Where(x => x.Ativo)
                .OrderByDescending(os => os.DataEntrada)
                .ToList();
        }

        public List<OrdemServicoModel> BuscarAtivas()
        {
            return _bancoContext.OrdensServico
                .Include(os => os.Cliente)
                .Include(os => os.Veiculo)
                .Where(x => x.Ativo)
                .OrderByDescending(os => os.DataEntrada)
                .ToList();
        }

        public List<OrdemServicoModel> BuscarPorCliente(int clienteId)
        {
            return _bancoContext.OrdensServico
                .Include(os => os.Cliente)
                .Include(os => os.Veiculo)
                .Where(x => x.ClienteId == clienteId && x.Ativo)
                .OrderByDescending(os => os.DataEntrada)
                .ToList();
        }

        public List<OrdemServicoModel> BuscarPorVeiculo(int veiculoId)
        {
            return _bancoContext.OrdensServico
                .Include(os => os.Cliente)
                .Include(os => os.Veiculo)
                .Where(x => x.VeiculoId == veiculoId && x.Ativo)
                .OrderByDescending(os => os.DataEntrada)
                .ToList();
        }

        public List<OrdemServicoModel> BuscarPorStatus(string status)
        {
            return _bancoContext.OrdensServico
                .Include(os => os.Cliente)
                .Include(os => os.Veiculo)
                .Where(x => x.Status == status && x.Ativo)
                .OrderByDescending(os => os.DataEntrada)
                .ToList();
        }

        public OrdemServicoModel Adicionar(OrdemServicoModel ordemServico)
        {
            if (string.IsNullOrEmpty(ordemServico.Numero))
            {
                ordemServico.Numero = GerarNumeroOS();
            }

            _bancoContext.OrdensServico.Add(ordemServico);
            _bancoContext.SaveChanges();
            return ordemServico;
        }

        public OrdemServicoModel Atualizar(OrdemServicoModel ordemServico)
        {
            OrdemServicoModel ordemDB = ListarPorId(ordemServico.Id);

            if (ordemDB == null) throw new System.Exception("Houve um erro na atualização da OS!");

            ordemDB.Numero = ordemServico.Numero;
            ordemDB.DataEntrada = ordemServico.DataEntrada;
            ordemDB.DataSaida = ordemServico.DataSaida;
            ordemDB.Status = ordemServico.Status;
            ordemDB.Observacoes = ordemServico.Observacoes;
            ordemDB.ValorTotal = ordemServico.ValorTotal;
            ordemDB.Mecanico = ordemServico.Mecanico;
            ordemDB.ClienteId = ordemServico.ClienteId;
            ordemDB.VeiculoId = ordemServico.VeiculoId;
            ordemDB.Ativo = ordemServico.Ativo;

            _bancoContext.OrdensServico.Update(ordemDB);
            _bancoContext.SaveChanges();

            return ordemDB;
        }

        public bool Apagar(int id)
        {
            OrdemServicoModel ordemDB = ListarPorId(id);

            if (ordemDB == null) throw new System.Exception("Houve um erro na exclusão da OS!");

            _bancoContext.OrdensServico.Remove(ordemDB);
            _bancoContext.SaveChanges();

            return true;
        }

        public List<OrdemServicoModel> BuscarPorNumeroOS(string numeroOS)
        {
            return _bancoContext.OrdensServico
                .Include(os => os.Cliente)
                .Include(os => os.Veiculo)
                .Where(x => x.Numero.Contains(numeroOS) && x.Ativo)
                .OrderByDescending(os => os.DataEntrada)
                .ToList();
        }

        public OrdemServicoModel BuscarPorNumeroOSExato(string numeroOS)
        {
            return _bancoContext.OrdensServico
                .Include(os => os.Cliente)
                .Include(os => os.Veiculo)
                .FirstOrDefault(x => x.Numero == numeroOS);
        }

        public string GerarNumeroOS()
        {
            var ultimaOS = _bancoContext.OrdensServico
                .Where(x => x.Numero.StartsWith("OS"))
                .OrderByDescending(x => x.Numero)
                .FirstOrDefault();

            if (ultimaOS == null)
            {
                return "OS0001";
            }

            var numero = ultimaOS.Numero.Substring(2);
            if (int.TryParse(numero, out int num))
            {
                return $"OS{(num + 1):D4}";
            }

            return "OS0001";
        }

        public List<OrdemServicoModel> BuscarPorPeriodo(DateTime dataInicio, DateTime dataFim)
        {
            return _bancoContext.OrdensServico
                .Include(os => os.Cliente)
                .Include(os => os.Veiculo)
                .Where(x => x.DataEntrada >= dataInicio && x.DataEntrada <= dataFim && x.Ativo)
                .OrderByDescending(os => os.DataEntrada)
                .ToList();
        }
    }
}
