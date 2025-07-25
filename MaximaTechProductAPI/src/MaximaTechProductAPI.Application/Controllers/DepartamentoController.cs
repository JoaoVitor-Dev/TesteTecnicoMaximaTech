using MaximaTechProductAPI.Application.Dtos;
using MaximaTechProductAPI.Core.Entities;
using MaximaTechProductAPI.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaximaTechProductAPI.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        private readonly IDepartamentoRepository _departamentoRepository;

        public DepartamentoController(IDepartamentoRepository departamentoRepository)
        {
            _departamentoRepository = departamentoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var departamentos = await _departamentoRepository.obterTodos();

            var lista = departamentos.Select(d => new DepartamentoDto
            {
                Codigo = d.Codigo,
                Descricao = d.Descricao
            });

            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> obter(Guid id)
        {
            var departamento = await _departamentoRepository.obter(id);
            if (departamento == null)
                return NotFound();

            var dto = new DepartamentoDto
            {
                Codigo = departamento.Codigo,
                Descricao = departamento.Descricao
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DepartamentoDto dto)
        {
            var departamento = new Departamento
            {
                Id = Guid.NewGuid(),
                Codigo = dto.Codigo,
                Descricao = dto.Descricao,
                Status = true
            };

            await _departamentoRepository.Adicionar(departamento);

            return CreatedAtAction(nameof(obter), new { id = departamento.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] DepartamentoDto dto)
        {
            var existing = await _departamentoRepository.obter(id);
            if (existing == null)
                return NotFound();

            existing.Codigo = dto.Codigo;
            existing.Descricao = dto.Descricao;

            await _departamentoRepository.Atualizar(existing);

            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Inativar(Guid id)
        {
            var existing = await _departamentoRepository.obter(id);
            if (existing == null)
                return NotFound();

            await _departamentoRepository.Inativar(id);
            return Ok("Departamento inativado com sucesso");
        }
    }
}
