using MaximaTechProductAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximaTechProductAPI.Core.Interfaces
{
    public interface IDepartamentoRepository
    {
        Task<IEnumerable<Departamento>> obterTodos();
        Task<Departamento?> obter(Guid id);
        Task Adicionar(Departamento departamento);
        Task Atualizar(Departamento departamento);
        Task Inativar(Guid id);
    }
}
