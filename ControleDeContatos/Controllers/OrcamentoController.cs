using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace ControleDeContatos.Controllers
{
    [Authorize]
    public class OrcamentoController : Controller
    {
        private readonly IOrcamentoRepositorio _orcamentoRepositorio;
        private readonly IItemOrcamentoRepositorio _itemOrcamentoRepositorio;
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IVeiculoRepositorio _veiculoRepositorio;
        private readonly IOrdemServicoRepositorio _ordemServicoRepositorio;

        public OrcamentoController(
            IOrcamentoRepositorio orcamentoRepositorio,
            IItemOrcamentoRepositorio itemOrcamentoRepositorio,
            IClienteRepositorio clienteRepositorio,
            IVeiculoRepositorio veiculoRepositorio,
            IOrdemServicoRepositorio ordemServicoRepositorio)
        {
            _orcamentoRepositorio = orcamentoRepositorio;
            _itemOrcamentoRepositorio = itemOrcamentoRepositorio;
            _clienteRepositorio = clienteRepositorio;
            _veiculoRepositorio = veiculoRepositorio;
            _ordemServicoRepositorio = ordemServicoRepositorio;
        }

        public IActionResult Index()
        {
            List<OrcamentoModel> orcamentos = _orcamentoRepositorio.BuscarAtivos();
            return View(orcamentos);
        }

        public IActionResult Criar()
        {
            ViewBag.Clientes = _clienteRepositorio.BuscarAtivos();
            ViewBag.Veiculos = _veiculoRepositorio.BuscarAtivos();
            return View();
        }

        [HttpPost]
        public IActionResult Criar(OrcamentoModel orcamento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Verificar se número do orçamento já existe
                    if (!string.IsNullOrEmpty(orcamento.NumeroOrcamento))
                    {
                        var orcamentoExistente = _orcamentoRepositorio.BuscarPorNumeroOrcamentoExato(orcamento.NumeroOrcamento);
                        if (orcamentoExistente != null)
                        {
                            TempData["MensagemErro"] = "Número do orçamento já cadastrado!";
                            ViewBag.Clientes = _clienteRepositorio.BuscarAtivos();
                            ViewBag.Veiculos = _veiculoRepositorio.BuscarAtivos();
                            return View(orcamento);
                        }
                    }

                    _orcamentoRepositorio.Adicionar(orcamento);
                    TempData["MensagemSucesso"] = "Orçamento cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }

                ViewBag.Clientes = _clienteRepositorio.BuscarAtivos();
                ViewBag.Veiculos = _veiculoRepositorio.BuscarAtivos();
                return View(orcamento);
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar o orçamento, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Editar(int id)
        {
            OrcamentoModel orcamento = _orcamentoRepositorio.ListarPorId(id);
            ViewBag.Clientes = _clienteRepositorio.BuscarAtivos();
            ViewBag.Veiculos = _veiculoRepositorio.BuscarAtivos();
            return View(orcamento);
        }

        [HttpPost]
        public IActionResult Editar(OrcamentoModel orcamento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Verificar se número do orçamento já existe em outro orçamento
                    if (!string.IsNullOrEmpty(orcamento.NumeroOrcamento))
                    {
                        var orcamentoExistente = _orcamentoRepositorio.BuscarPorNumeroOrcamentoExato(orcamento.NumeroOrcamento);
                        if (orcamentoExistente != null && orcamentoExistente.Id != orcamento.Id)
                        {
                            TempData["MensagemErro"] = "Número do orçamento já cadastrado para outro orçamento!";
                            ViewBag.Clientes = _clienteRepositorio.BuscarAtivos();
                            ViewBag.Veiculos = _veiculoRepositorio.BuscarAtivos();
                            return View(orcamento);
                        }
                    }

                    _orcamentoRepositorio.Atualizar(orcamento);
                    TempData["MensagemSucesso"] = "Orçamento atualizado com sucesso!";
                    return RedirectToAction("Index");
                }

                ViewBag.Clientes = _clienteRepositorio.BuscarAtivos();
                ViewBag.Veiculos = _veiculoRepositorio.BuscarAtivos();
                return View(orcamento);
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos atualizar o orçamento, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            OrcamentoModel orcamento = _orcamentoRepositorio.ListarPorId(id);
            return View(orcamento);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _orcamentoRepositorio.Apagar(id);

                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Orçamento apagado com sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = "Ops, não conseguimos apagar o orçamento!";
                }

                return RedirectToAction("Index");
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos apagar o orçamento, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Buscar(string termo)
        {
            if (string.IsNullOrEmpty(termo))
            {
                return Json(_orcamentoRepositorio.BuscarAtivos());
            }

            var orcamentos = _orcamentoRepositorio.BuscarPorNumeroOrcamento(termo);
            return Json(orcamentos);
        }

        [HttpGet]
        public IActionResult BuscarPorCliente(int clienteId)
        {
            var orcamentos = _orcamentoRepositorio.BuscarPorCliente(clienteId);
            return Json(orcamentos);
        }

        [HttpGet]
        public IActionResult BuscarPorVeiculo(int veiculoId)
        {
            var orcamentos = _orcamentoRepositorio.BuscarPorVeiculo(veiculoId);
            return Json(orcamentos);
        }

        [HttpGet]
        public IActionResult BuscarPorStatus(string status)
        {
            var orcamentos = _orcamentoRepositorio.BuscarPorStatus(status);
            return Json(orcamentos);
        }

        [HttpGet]
        public IActionResult BuscarVeiculosPorCliente(int clienteId)
        {
            var veiculos = _veiculoRepositorio.BuscarPorCliente(clienteId);
            return Json(veiculos);
        }

        [HttpPost]
        public IActionResult Aprovar(int id)
        {
            try
            {
                var orcamento = _orcamentoRepositorio.AprovarOrcamento(id);
                TempData["MensagemSucesso"] = "Orçamento aprovado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Erro ao aprovar orçamento: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Rejeitar(int id)
        {
            try
            {
                var orcamento = _orcamentoRepositorio.RejeitarOrcamento(id);
                TempData["MensagemSucesso"] = "Orçamento rejeitado!";
                return RedirectToAction("Index");
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Erro ao rejeitar orçamento: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult ConverterEmOS(int id)
        {
            try
            {
                var orcamento = _orcamentoRepositorio.ListarPorId(id);
                if (orcamento == null)
                {
                    TempData["MensagemErro"] = "Orçamento não encontrado!";
                    return RedirectToAction("Index");
                }

                // Criar uma nova OS baseada no orçamento
                var novaOS = new OrdemServicoModel
                {
                    Descricao = orcamento.Descricao,
                    Valor = orcamento.ValorTotal,
                    Status = "Aberta",
                    ClienteId = orcamento.ClienteId,
                    VeiculoId = orcamento.VeiculoId,
                    Observacoes = $"Convertido do orçamento {orcamento.NumeroOrcamento}"
                };

                var ordemServico = _ordemServicoRepositorio.Adicionar(novaOS);
                _orcamentoRepositorio.ConverterEmOS(id, ordemServico.Id);

                TempData["MensagemSucesso"] = $"Orçamento convertido em OS {ordemServico.NumeroOS} com sucesso!";
                return RedirectToAction("Index");
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Erro ao converter orçamento em OS: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Visualizar(int id)
        {
            OrcamentoModel orcamento = _orcamentoRepositorio.ListarPorId(id);
            if (orcamento == null)
            {
                TempData["MensagemErro"] = "Orçamento não encontrado!";
                return RedirectToAction("Index");
            }

            return View(orcamento);
        }
    }
}
