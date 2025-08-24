using ControleDeContatos.Models;
using System.Collections.Generic;

namespace ControleDeContatos.Repositorio
{
    public interface IItemOrcamentoRepositorio
    {
        ItemOrcamentoModel ListarPorId(int id);
        List<ItemOrcamentoModel> BuscarPorOrcamento(int orcamentoId);
        ItemOrcamentoModel Adicionar(ItemOrcamentoModel item);
        ItemOrcamentoModel Atualizar(ItemOrcamentoModel item);
        bool Apagar(int id);
        bool ApagarPorOrcamento(int orcamentoId);
        decimal CalcularValorTotal(int orcamentoId);
    }
}
