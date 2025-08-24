using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace ControleDeContatos.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
        private readonly IClienteRepositorio _clienteRepositorio;

        public ClienteController(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }

        public IActionResult Index()
        {
            List<ClienteModel> clientes = _clienteRepositorio.BuscarAtivos();
            return View(clientes);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(ClienteModel cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Verificar se CPF/CNPJ já existe
                    var clienteExistente = _clienteRepositorio.BuscarPorCpfCnpj(cliente.CpfCnpj);
                    if (clienteExistente != null)
                    {
                        TempData["MensagemErro"] = "CPF/CNPJ já cadastrado!";
                        return View(cliente);
                    }

                    _clienteRepositorio.Adicionar(cliente);
                    TempData["MensagemSucesso"] = "Cliente cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(cliente);
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar o cliente, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Editar(int id)
        {
            ClienteModel cliente = _clienteRepositorio.ListarPorId(id);
            return View(cliente);
        }

        [HttpPost]
        public IActionResult Editar(ClienteModel cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Verificar se CPF/CNPJ já existe em outro cliente
                    var clienteExistente = _clienteRepositorio.BuscarPorCpfCnpj(cliente.CpfCnpj);
                    if (clienteExistente != null && clienteExistente.Id != cliente.Id)
                    {
                        TempData["MensagemErro"] = "CPF/CNPJ já cadastrado para outro cliente!";
                        return View(cliente);
                    }

                    _clienteRepositorio.Atualizar(cliente);
                    TempData["MensagemSucesso"] = "Cliente atualizado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(cliente);
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos atualizar o cliente, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            ClienteModel cliente = _clienteRepositorio.ListarPorId(id);
            return View(cliente);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _clienteRepositorio.Apagar(id);

                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Cliente apagado com sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = "Ops, não conseguimos apagar o cliente!";
                }

                return RedirectToAction("Index");
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos apagar o cliente, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Buscar(string termo)
        {
            if (string.IsNullOrEmpty(termo))
            {
                return Json(_clienteRepositorio.BuscarAtivos());
            }

            var clientes = _clienteRepositorio.BuscarPorNome(termo);
            return Json(clientes);
        }
    }
}
