using ControleDeContatos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ControleDeContatos.Repositorio;

namespace ControleDeContatos.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOrdemServicoRepositorio _ordemServicoRepositorio;

        public HomeController(ILogger<HomeController> logger, IOrdemServicoRepositorio ordemServicoRepositorio)
        {
            _logger = logger;
            _ordemServicoRepositorio = ordemServicoRepositorio;
        }

        public IActionResult Index()
        {
            try
            {
                var todasOrdens = _ordemServicoRepositorio.BuscarTodas();
                var dataAtual = DateTime.Now;

                // Mostrar todas as ordens na aba "OS Abertas"
                var ordensAbertas = todasOrdens?.ToList() ?? new List<OrdemServicoModel>();

                // Log detalhado para debug
                _logger.LogInformation($"Total de ordens: {todasOrdens?.Count ?? 0}");
                _logger.LogInformation($"Ordens abertas: {ordensAbertas.Count}");

                // Calcular percentuais baseados no status das ordens
                var totalOrdens = todasOrdens?.Count() ?? 0;
                var countOrdensAbertas = todasOrdens?.Where(os => string.IsNullOrEmpty(os.Status) || os.Status == "Aberta").Count() ?? 0;
                var countOrdensEmAndamento = todasOrdens?.Where(os => os.Status == "Em Andamento").Count() ?? 0;
                var countOrdensFinalizadas = todasOrdens?.Where(os => os.Status == "Finalizada").Count() ?? 0;
                var countOrdensCanceladas = todasOrdens?.Where(os => os.Status == "Cancelada").Count() ?? 0;

                var percentuais = new DashboardPercentuaisModel
                {
                    DentroPrazo = totalOrdens > 0 ? Math.Round((double)countOrdensAbertas / totalOrdens * 100, 0) : 0,
                    EmAlerta = totalOrdens > 0 ? Math.Round((double)countOrdensEmAndamento / totalOrdens * 100, 0) : 0,
                    NoLimite = totalOrdens > 0 ? Math.Round((double)countOrdensFinalizadas / totalOrdens * 100, 0) : 0,
                    PrazoExpirado = totalOrdens > 0 ? Math.Round((double)countOrdensCanceladas / totalOrdens * 100, 0) : 0,
                    Total = totalOrdens
                };

                ViewBag.Percentuais = percentuais;
                ViewBag.OrdensAbertas = ordensAbertas;
            }
            catch (Exception ex)
            {
                // Em caso de erro, definir valores padrão
                ViewBag.Percentuais = new DashboardPercentuaisModel
                {
                    DentroPrazo = 0,
                    EmAlerta = 0,
                    NoLimite = 0,
                    PrazoExpirado = 0,
                    Total = 0
                };
                ViewBag.OrdensAbertas = new List<OrdemServicoModel>();
                
                _logger.LogError(ex, "Erro ao calcular percentuais do dashboard");
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Action para debug - remover depois
        public IActionResult Debug()
        {
            var todasOrdens = _ordemServicoRepositorio.BuscarTodas();
            var ordensAbertas = todasOrdens?.Where(os => string.IsNullOrEmpty(os.Status) || os.Status == "Aberta" || os.Status == "Em Andamento").ToList() ?? new List<OrdemServicoModel>();
            
            var debugInfo = new
            {
                TotalOrdens = todasOrdens?.Count ?? 0,
                OrdensAbertas = ordensAbertas.Count,
                Ordens = todasOrdens?.Select(os => new { 
                    Id = os.Id, 
                    NumeroOS = os.NumeroOS, 
                    Status = os.Status, 
                    Ativo = os.Ativo,
                    DataAbertura = os.DataAbertura 
                }).ToList()
            };
            
            return Json(debugInfo);
        }


    }
}
