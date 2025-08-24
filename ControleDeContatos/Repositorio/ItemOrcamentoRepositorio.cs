using ControleDeContatos.Models;
using ControleDeContatos.Models.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeContatos.Repositorio
{
    public class ItemOrcamentoRepositorio : IItemOrcamentoRepositorio
    {
        private readonly BancoContext _bancoContext;

        public ItemOrcamentoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public ItemOrcamentoModel ListarPorId(int id)
        {
            return _bancoContext.ItensOrcamento
                .Include(i => i.Orcamento)
                .FirstOrDefault(x => x.Id == id);
        }

        public List<ItemOrcamentoModel> BuscarPorOrcamento(int orcamentoId)
        {
            return _bancoContext.ItensOrcamento
                .Include(i => i.Orcamento)
                .Where(x => x.OrcamentoId == orcamentoId && x.Ativo)
                .OrderBy(x => x.Id)
                .ToList();
        }

        public ItemOrcamentoModel Adicionar(ItemOrcamentoModel item)
        {
            _bancoContext.ItensOrcamento.Add(item);
            _bancoContext.SaveChanges();
            return item;
        }

        public ItemOrcamentoModel Atualizar(ItemOrcamentoModel item)
        {
            ItemOrcamentoModel itemDB = ListarPorId(item.Id);

            if (itemDB == null) throw new System.Exception("Houve um erro na atualização do item!");

            itemDB.Descricao = item.Descricao;
            itemDB.Quantidade = item.Quantidade;
            itemDB.ValorUnitario = item.ValorUnitario;
            itemDB.ValorTotal = item.ValorTotal;
            itemDB.Tipo = item.Tipo;
            itemDB.Observacoes = item.Observacoes;
            itemDB.Ativo = item.Ativo;

            _bancoContext.ItensOrcamento.Update(itemDB);
            _bancoContext.SaveChanges();

            return itemDB;
        }

        public bool Apagar(int id)
        {
            ItemOrcamentoModel itemDB = ListarPorId(id);

            if (itemDB == null) throw new System.Exception("Houve um erro na exclusão do item!");

            _bancoContext.ItensOrcamento.Remove(itemDB);
            _bancoContext.SaveChanges();

            return true;
        }

        public bool ApagarPorOrcamento(int orcamentoId)
        {
            var itens = _bancoContext.ItensOrcamento.Where(x => x.OrcamentoId == orcamentoId);
            _bancoContext.ItensOrcamento.RemoveRange(itens);
            _bancoContext.SaveChanges();

            return true;
        }

        public decimal CalcularValorTotal(int orcamentoId)
        {
            return _bancoContext.ItensOrcamento
                .Where(i => i.OrcamentoId == orcamentoId && i.Ativo)
                .Sum(i => i.ValorTotal);
        }
    }
}
