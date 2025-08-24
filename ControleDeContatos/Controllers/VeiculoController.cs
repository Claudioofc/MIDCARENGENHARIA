using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using ControleDeContatos.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeContatos.Controllers
{
    [Authorize]
    public class VeiculoController : Controller
    {
        private readonly IVeiculoRepositorio _veiculoRepositorio;
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IDetranService _detranService;
        private readonly ILogger<VeiculoController> _logger;

        public VeiculoController(IVeiculoRepositorio veiculoRepositorio, IClienteRepositorio clienteRepositorio, IDetranService detranService, ILogger<VeiculoController> logger)
        {
            _veiculoRepositorio = veiculoRepositorio;
            _clienteRepositorio = clienteRepositorio;
            _detranService = detranService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<VeiculoModel> veiculos = _veiculoRepositorio.BuscarAtivos();
            return View(veiculos);
        }

        public IActionResult Criar()
        {
            ViewBag.Clientes = _clienteRepositorio.BuscarAtivos();
            return View();
        }

        [HttpPost]
        public IActionResult Criar(VeiculoModel veiculo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Verificar se placa já existe
                    var veiculoExistente = _veiculoRepositorio.BuscarPorPlacaExata(veiculo.Placa);
                    if (veiculoExistente != null)
                    {
                        TempData["MensagemErro"] = "Placa já cadastrada!";
                        ViewBag.Clientes = _clienteRepositorio.BuscarAtivos();
                        return View(veiculo);
                    }

                    _veiculoRepositorio.Adicionar(veiculo);
                    TempData["MensagemSucesso"] = "Veículo cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }

                ViewBag.Clientes = _clienteRepositorio.BuscarAtivos();
                return View(veiculo);
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar o veículo, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Editar(int id)
        {
            VeiculoModel veiculo = _veiculoRepositorio.ListarPorId(id);
            ViewBag.Clientes = _clienteRepositorio.BuscarAtivos();
            return View(veiculo);
        }

        [HttpPost]
        public IActionResult Editar(VeiculoModel veiculo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Verificar se placa já existe em outro veículo
                    var veiculoExistente = _veiculoRepositorio.BuscarPorPlacaExata(veiculo.Placa);
                    if (veiculoExistente != null && veiculoExistente.Id != veiculo.Id)
                    {
                        TempData["MensagemErro"] = "Placa já cadastrada para outro veículo!";
                        ViewBag.Clientes = _clienteRepositorio.BuscarAtivos();
                        return View(veiculo);
                    }

                    _veiculoRepositorio.Atualizar(veiculo);
                    TempData["MensagemSucesso"] = "Veículo atualizado com sucesso!";
                    return RedirectToAction("Index");
                }

                ViewBag.Clientes = _clienteRepositorio.BuscarAtivos();
                return View(veiculo);
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos atualizar o veículo, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            VeiculoModel veiculo = _veiculoRepositorio.ListarPorId(id);
            return View(veiculo);
        }

        [HttpPost]
        public async Task<IActionResult> ConsultarPlaca(string placa)
        {
            try
            {
                _logger.LogInformation("Consultando placa: {Placa}", placa);
                
                if (string.IsNullOrEmpty(placa))
                {
                    return Json(new VeiculoInfo
                    {
                        Sucesso = false,
                        Mensagem = "Placa não informada"
                    });
                }
                
                var veiculoInfo = await _detranService.ConsultarVeiculoPorPlacaAsync(placa);
                return Json(veiculoInfo);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar placa: {Placa}", placa);
                return Json(new VeiculoInfo
                {
                    Sucesso = false,
                    Mensagem = "Erro ao consultar placa: " + ex.Message
                });
            }
        }



        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _veiculoRepositorio.Apagar(id);

                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Veículo apagado com sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = "Ops, não conseguimos apagar o veículo!";
                }

                return RedirectToAction("Index");
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos apagar o veículo, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Buscar(string termo)
        {
            if (string.IsNullOrEmpty(termo))
            {
                return Json(_veiculoRepositorio.BuscarAtivos());
            }

            var veiculos = _veiculoRepositorio.BuscarPorMarcaModelo(termo);
            return Json(veiculos);
        }

        [HttpGet]
        public IActionResult BuscarPorCliente(int clienteId)
        {
            var veiculos = _veiculoRepositorio.BuscarPorCliente(clienteId);
            return Json(veiculos);
        }

        [HttpGet]
        public IActionResult BuscarPorPlaca(string placa)
        {
            var veiculos = _veiculoRepositorio.BuscarPorPlaca(placa);
            return Json(veiculos);
        }
    }
}
