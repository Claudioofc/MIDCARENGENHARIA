using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeContatos.Controllers
{
    [Authorize]
    public class OrdemServicoController : Controller
    {
        private readonly IOrdemServicoRepositorio _ordemServicoRepositorio;
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IVeiculoRepositorio _veiculoRepositorio;
        private readonly ILogger<OrdemServicoController> _logger;

        public OrdemServicoController(
            IOrdemServicoRepositorio ordemServicoRepositorio, 
            IClienteRepositorio clienteRepositorio,
            IVeiculoRepositorio veiculoRepositorio,
            ILogger<OrdemServicoController> logger)
        {
            _ordemServicoRepositorio = ordemServicoRepositorio;
            _clienteRepositorio = clienteRepositorio;
            _veiculoRepositorio = veiculoRepositorio;
            _logger = logger;
        }

        public IActionResult Index(int pagina = 1, int tamanhoPagina = 25)
        {
            var todasOrdens = _ordemServicoRepositorio.BuscarAtivas();
            var totalRegistros = todasOrdens.Count;
            var totalPaginas = (int)Math.Ceiling((double)totalRegistros / tamanhoPagina);
            
            // Ajustar página se estiver fora dos limites
            if (pagina < 1) pagina = 1;
            if (pagina > totalPaginas && totalPaginas > 0) pagina = totalPaginas;
            
            // Calcular registros para a página atual
            var registrosPulados = (pagina - 1) * tamanhoPagina;
            var ordens = todasOrdens.Skip(registrosPulados).Take(tamanhoPagina).ToList();
            
            // Informações de paginação para a view
            ViewBag.PaginaAtual = pagina;
            ViewBag.TotalPaginas = totalPaginas;
            ViewBag.TamanhoPagina = tamanhoPagina;
            ViewBag.TotalRegistros = totalRegistros;
            ViewBag.RegistroInicial = totalRegistros > 0 ? registrosPulados + 1 : 0;
            ViewBag.RegistroFinal = Math.Min(registrosPulados + tamanhoPagina, totalRegistros);
            
            return View(ordens);
        }

        public IActionResult Criar()
        {
            ViewBag.Clientes = _clienteRepositorio.BuscarAtivos();
            ViewBag.Veiculos = _veiculoRepositorio.BuscarAtivos();
            return View();
        }

        [HttpPost]
        public IActionResult Criar(OrdemServicoModel ordemServico)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Verificar se número da OS já existe
                    if (!string.IsNullOrEmpty(ordemServico.NumeroOS))
                    {
                        var ordemExistente = _ordemServicoRepositorio.BuscarPorNumeroOSExato(ordemServico.NumeroOS);
                        if (ordemExistente != null)
                        {
                            TempData["MensagemErro"] = "Número da OS já cadastrado!";
                            ViewBag.Clientes = _clienteRepositorio.BuscarAtivos();
                            ViewBag.Veiculos = _veiculoRepositorio.BuscarAtivos();
                            return View(ordemServico);
                        }
                    }

                    _ordemServicoRepositorio.Adicionar(ordemServico);
                    TempData["MensagemSucesso"] = "Ordem de Serviço cadastrada com sucesso!";
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.Clientes = _clienteRepositorio.BuscarAtivos();
                ViewBag.Veiculos = _veiculoRepositorio.BuscarAtivos();
                return View(ordemServico);
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar a OS, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Editar(int id)
        {
            OrdemServicoModel ordem = _ordemServicoRepositorio.ListarPorId(id);
            
            // Log para debug
            _logger.LogInformation($"Editando OS {id} - Valor: {ordem?.Valor}");
            
            ViewBag.Clientes = _clienteRepositorio.BuscarAtivos();
            ViewBag.Veiculos = _veiculoRepositorio.BuscarAtivos();
            return View(ordem);
        }

        [HttpPost]
        public IActionResult Editar(OrdemServicoModel ordemServico)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Verificar se número da OS já existe em outra OS
                    if (!string.IsNullOrEmpty(ordemServico.NumeroOS))
                    {
                        var ordemExistente = _ordemServicoRepositorio.BuscarPorNumeroOSExato(ordemServico.NumeroOS);
                        if (ordemExistente != null && ordemExistente.Id != ordemServico.Id)
                        {
                            TempData["MensagemErro"] = "Número da OS já cadastrado para outra ordem!";
                            ViewBag.Clientes = _clienteRepositorio.BuscarAtivos();
                            ViewBag.Veiculos = _veiculoRepositorio.BuscarAtivos();
                            return View(ordemServico);
                        }
                    }

                    _ordemServicoRepositorio.Atualizar(ordemServico);
                    TempData["MensagemSucesso"] = "Ordem de Serviço atualizada com sucesso!";
                    return RedirectToAction("Index");
                }

                ViewBag.Clientes = _clienteRepositorio.BuscarAtivos();
                ViewBag.Veiculos = _veiculoRepositorio.BuscarAtivos();
                return View(ordemServico);
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos atualizar a OS, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            OrdemServicoModel ordem = _ordemServicoRepositorio.ListarPorId(id);
            return View(ordem);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _ordemServicoRepositorio.Apagar(id);

                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Ordem de Serviço apagada com sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = "Ops, não conseguimos apagar a OS!";
                }

                return RedirectToAction("Index");
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos apagar a OS, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Buscar(string termo)
        {
            if (string.IsNullOrEmpty(termo))
            {
                return Json(_ordemServicoRepositorio.BuscarAtivas());
            }

            var ordens = _ordemServicoRepositorio.BuscarPorNumeroOS(termo);
            return Json(ordens);
        }

        [HttpGet]
        public IActionResult BuscarPorCliente(int clienteId)
        {
            var ordens = _ordemServicoRepositorio.BuscarPorCliente(clienteId);
            return Json(ordens);
        }

        [HttpGet]
        public IActionResult BuscarPorVeiculo(int veiculoId)
        {
            var ordens = _ordemServicoRepositorio.BuscarPorVeiculo(veiculoId);
            return Json(ordens);
        }

        [HttpGet]
        public IActionResult BuscarPorStatus(string status)
        {
            var ordens = _ordemServicoRepositorio.BuscarPorStatus(status);
            return Json(ordens);
        }

        [HttpGet]
        public IActionResult BuscarVeiculosPorCliente(int clienteId)
        {
            var veiculos = _veiculoRepositorio.BuscarPorCliente(clienteId);
            return Json(veiculos);
        }

        public IActionResult Visualizar(int id)
        {
            OrdemServicoModel ordem = _ordemServicoRepositorio.ListarPorId(id);
            if (ordem == null)
            {
                TempData["MensagemErro"] = "Ordem de Serviço não encontrada!";
                return RedirectToAction("Index", "Home");
            }
            return View(ordem);
        }

        // Action para debug de uma OS específica
        public IActionResult DebugOS(int id)
        {
            var ordem = _ordemServicoRepositorio.ListarPorId(id);
            
            var debugInfo = new
            {
                Id = ordem?.Id,
                NumeroOS = ordem?.NumeroOS,
                Status = ordem?.Status,
                Valor = ordem?.Valor,
                ValorFormatado = ordem?.Valor.ToString("F2"),
                Descricao = ordem?.Descricao,
                Cliente = ordem?.Cliente?.Nome,
                Veiculo = ordem?.Veiculo?.Placa
            };
            
            return Json(debugInfo);
        }

        // Action para criar dados de teste (remover em produção)
        public IActionResult CriarDadosTeste()
        {
            try
            {
                var clientes = _clienteRepositorio.BuscarAtivos();
                var veiculos = _veiculoRepositorio.BuscarAtivos();

                if (!clientes.Any() || !veiculos.Any())
                {
                    return Json(new { success = false, message = "É necessário ter clientes e veículos cadastrados" });
                }

                var random = new Random();
                var statuses = new[] { "Aberta", "Em Andamento", "Finalizada", "Cancelada" };

                for (int i = 2; i <= 30; i++) // Criar 29 ordens adicionais
                {
                    var ordem = new OrdemServicoModel
                    {
                        NumeroOS = $"OS{i:D4}",
                        Descricao = $"Serviço de teste {i}",
                        Valor = random.Next(100, 5000),
                        Status = statuses[random.Next(statuses.Length)],
                        DataAbertura = DateTime.Now.AddDays(-random.Next(1, 30)),
                        ClienteId = clientes[random.Next(clientes.Count)].Id,
                        VeiculoId = veiculos[random.Next(veiculos.Count)].Id,
                        Mecanico = "Milton Diego",
                        Ativo = true
                    };

                    _ordemServicoRepositorio.Adicionar(ordem);
                }

                return Json(new { success = true, message = "30 ordens de serviço criadas com sucesso!" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar dados de teste");
                return Json(new { success = false, message = "Erro ao criar dados de teste" });
            }
        }
    }
}
