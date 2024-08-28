using ControleDeContatos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeContatos.Repositorio
{
    public interface IContatoRepositorio
    {
        ContatoModel ListarPorId(int id);
        List<ContatoModel> BuscarTodos();
        ContatoModel Adiconar(ContatoModel contato);   
        ContatoModel Atualizar(ContatoModel contato);
        bool Apagar(int id);
        ContatoModel BuscarPorID(int id);
    }
}
