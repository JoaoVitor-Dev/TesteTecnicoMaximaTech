using MaximaTechProductAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximaTechProductAPI.Core.Interfaces
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<dynamic>> obterTodos();
        Task<Produto?> obter(Guid id);
        Task Adicionar(Produto produto);
        Task Atualizar(Produto produto);
        Task Inativar(Guid id);
    }
}
