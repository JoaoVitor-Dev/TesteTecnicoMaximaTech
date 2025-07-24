using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximaTechProductAPI.Core.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public string Codigo { get; set; }
        public bool Status { get; set; } = false;
    }
}
