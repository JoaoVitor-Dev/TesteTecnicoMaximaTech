using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximaTechProductAPI.Core.Entities
{
    public class Produto: BaseEntity
    {
        public string Descricao { get; set; }
        public Guid DepartamentoId { get; set; }
        public decimal Preco { get; set; }
    }
}
