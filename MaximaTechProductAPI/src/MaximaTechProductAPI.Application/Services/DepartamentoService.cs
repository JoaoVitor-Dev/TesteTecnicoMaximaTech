using MaximaTechProductAPI.Application.Dtos;

namespace MaximaTechProductAPI.Application.Services;

public interface IDepartmentService
{
    IEnumerable<DepartamentoDto> GetAll();
}

public class DepartmentService : IDepartmentService
{
    public IEnumerable<DepartamentoDto> GetAll()
    {
        return new List<DepartamentoDto>
        {
            new DepartamentoDto { Codigo = "010", Descricao = "BEBIDAS" },
            new DepartamentoDto { Codigo = "020", Descricao = "CONGELADOS" },
            new DepartamentoDto { Codigo = "030", Descricao = "LATICINIOS" },
            new DepartamentoDto { Codigo = "040", Descricao = "VEGETAIS" }
        };
    }
}